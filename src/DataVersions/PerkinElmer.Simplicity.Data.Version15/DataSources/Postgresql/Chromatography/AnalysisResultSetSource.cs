using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    internal class AnalysisResultSetSource
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Guid> GetAnalysisResultSetGuids(long projectId,
            PostgresqlSourceContext postgresqlSourceContext)
        {
            var analysisResultSetDao = new AnalysisResultSetDao();
            using var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString);

            if (connection.State != ConnectionState.Open) connection.Open();
            return analysisResultSetDao.GetAllAnalysisResultSetGuids(connection, projectId);
        }

        public static IList<Version15DataBase> GetAnalysisResultSets(Guid projectGuid, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version15DataBase>(); 
            var analysisResultSetDao = new AnalysisResultSetDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var analysisResultSets = analysisResultSetDao.GetAll(connection, projectGuid);
                foreach (var analysisResultSet in analysisResultSets)
                {
                    var analysisResultSetData = CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        analysisResultSetData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                    migrationEntities.Add(analysisResultSetData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version15DataBase> GetAnalysisResultSets(Guid projectGuid, IList<Guid> analysisResultSetGuids,
            PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version15DataBase>();
            var analysisResultSetDao = new AnalysisResultSetDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var analysisResultSetId in analysisResultSetGuids)
                {
                    var analysisResultSet = analysisResultSetDao.Get(connection, projectGuid, analysisResultSetId);
                    var analysisResultSetData = CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        analysisResultSetData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                    migrationEntities.Add(analysisResultSetData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        internal static AnalysisResultSetData CreateAnalysisResultSetData(IDbConnection connection, Guid projectGuid,
            AnalysisResultSet analysisResultSet)
        {
            var analysisResultSetData = new AnalysisResultSetData();

            var batchRunAnalysisResultDao = new BatchRunAnalysisResultDao();
            var batchResultSetDao = new BatchResultSetDao();
            var batchRunChannelMapDao = new BatchRunChannelMapDao();
            var manualOverrideMapDao = new ManualOverrideMapDao();
            var manualOverrideIntegrationEventDao = new ManualOverrideIntegrationEventDao();
            var brChannelsWithExceededNumberOfPeaksDao = new BrChannelsWithExceededNumberOfPeaksDao();
            var suitabilitySummaryResultDao = new CompoundSuitabilitySummaryResultDao();

            analysisResultSetData.ProjectGuid = projectGuid;
            analysisResultSetData.AnalysisResultSet = analysisResultSet;
            var batchRunAnalysisResults = batchRunAnalysisResultDao.GetBatchRunAnalysisResults(connection, analysisResultSet.Id);
            var batchResultSetGuids = batchRunAnalysisResults.Select(b => b.OriginalBatchResultSetGuid).Distinct().ToList();
            if(batchResultSetGuids.Contains(analysisResultSet.BatchResultSetGuid) == false)
                batchResultSetGuids.Add(analysisResultSet.BatchResultSetGuid);


            var stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach (var batchResultSetGuid in batchResultSetGuids)
            {
                var batchResultSet = batchResultSetDao.Get(connection, projectGuid, batchResultSetGuid);
                var batchResultSetData = BatchResultSetSource.CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                analysisResultSetData.BatchResultSetData.Add(batchResultSetData);
            }

            stopWatch.Stop();
            var cost = stopWatch.ElapsedMilliseconds;

            analysisResultSetData.BatchRunAnalysisResults = CreateBatchRunAnalysisResultsData(connection, batchRunAnalysisResults);
            analysisResultSetData.BatchRunChannelMaps = batchRunChannelMapDao.GetBatchRunChannelMapByAnalysisResultSetId(connection, analysisResultSet.Id);

            var manaualOverrideMaps = manualOverrideMapDao.GetManualOverrideMapByAnalysisResultSetId(connection, analysisResultSet.Id);
            foreach (var manualOverrideMap in manaualOverrideMaps)
            {
                var manualOverrideIntegrationEvents = manualOverrideIntegrationEventDao.GetIntegrationEventsByManualOverrideMapId(connection, manualOverrideMap.Id);
                manualOverrideMap.ManualOverrideIntegrationEvents.AddRange(manualOverrideIntegrationEvents);
            }
            analysisResultSetData.ManualOverrideMaps = manaualOverrideMaps;

            analysisResultSetData.BrChannelsWithExceededNumberOfPeaks = brChannelsWithExceededNumberOfPeaksDao.Get(connection, analysisResultSet.Id);

            analysisResultSetData.CompoundSuitabilitySummaryResults = suitabilitySummaryResultDao.GetCompoundSuitabilitySummaryResults(connection, analysisResultSet.Id);

            analysisResultSetData.CompoundLibraryData = CompoundLibrarySource.CreateCompoundLibraryData(connection, projectGuid, analysisResultSet.Guid);

            analysisResultSetData.ReviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid, analysisResultSet.Guid.ToString(), ReviewApproveEntityType.AnalysisResultSet);

            return analysisResultSetData;
        }

        internal static IList<BatchRunAnalysisResultData> CreateBatchRunAnalysisResultsData(IDbConnection connection,
            IList<BatchRunAnalysisResult> batchRunAnalysisResults)
        {
            if (batchRunAnalysisResults == null) return null;
            var sequenceSampleInfoModifiableDao = new SequenceSampleInfoModifiableDao();
            var processingMethodModifiableDao = new ProcessingMethodModifiableDao();
            var batchRunAnalysisResultDao = new CalculatedChannelDataDao();
            var runPeakResultDao = new RunPeakResultDao();
            var suitabilityResultDao = new SuitabilityResultDao();
            var processingMethodCache = new Dictionary<long, ProcessingMethod>();

            var batchRunAnalysisResultsData = new List<BatchRunAnalysisResultData>();

            foreach (var batchRunAnalysisResult in batchRunAnalysisResults)
            {
                var batchRunAnalysisResultData = new BatchRunAnalysisResultData
                {
                    BatchRunAnalysisResult = batchRunAnalysisResult,
                    SequenceSampleInfoModifiable = sequenceSampleInfoModifiableDao.GetSequenceSampleInfoModifiable(connection, batchRunAnalysisResult.SequenceSampleInfoModifiableId)
                };

                if (!processingMethodCache.ContainsKey(batchRunAnalysisResult.ProcessingMethodModifiableId))
                    processingMethodCache[batchRunAnalysisResult.ProcessingMethodModifiableId] = processingMethodModifiableDao.GetProcessingMethod(connection, batchRunAnalysisResult.ProcessingMethodModifiableId);
                batchRunAnalysisResultData.ModifiableProcessingMethod = processingMethodCache[batchRunAnalysisResult.ProcessingMethodModifiableId];

                var calculatedChannelsData = new List<CalculatedChannelCompositeData>();
                var calculatedChannelDataListEntities = batchRunAnalysisResultDao.GetChannelDataByBatchRunAnalysisResultId(connection, batchRunAnalysisResult.Id);
                foreach (var calculatedChannelDataListEntity in calculatedChannelDataListEntities)
                {
                    var calculatedChannelCompositeData = new CalculatedChannelCompositeData
                    {
                        CalculatedChannelData = calculatedChannelDataListEntity,
                        SuitabilityResults = suitabilityResultDao.GetSuitabilityResult(connection, calculatedChannelDataListEntity.Id),
                        RunPeakResults = runPeakResultDao.GetRunPeakByCalculatedChannelDataId(connection, calculatedChannelDataListEntity.Id)
                    };
                    calculatedChannelsData.Add(calculatedChannelCompositeData);
                }
                batchRunAnalysisResultData.CalculatedChannelData = calculatedChannelsData;
                batchRunAnalysisResultsData.Add(batchRunAnalysisResultData);
            }

            return batchRunAnalysisResultsData;
        }
    }
}
