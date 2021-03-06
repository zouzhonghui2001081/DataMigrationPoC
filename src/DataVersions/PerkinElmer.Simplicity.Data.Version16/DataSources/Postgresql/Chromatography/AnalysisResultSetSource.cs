using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class AnalysisResultSetSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetAnalysisResultSets(Guid projectGuid, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
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

        public static IList<Version16DataBase> GetAnalysisResultSets(Guid projectGuid, IList<Guid> analysisResultSetGuids,
            PostgresqlSourceContext postgresqlSourceContextl)
        {
            var migrationEntities = new List<Version16DataBase>();
            var analysisResultSetDao = new AnalysisResultSetDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContextl.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var analysisResultSetId in analysisResultSetGuids)
                {
                    var analysisResultSet = analysisResultSetDao.Get(connection, projectGuid, analysisResultSetId);
                    var analysisResultSetData = CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                    if (postgresqlSourceContextl.IsIncludeAuditTrail)
                        analysisResultSetData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContextl.AuditTrailConnectionString,  analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
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

            var batchRunAnalysisResults = batchRunAnalysisResultDao.GetBatchRunAnalysisResults(connection, analysisResultSet.Id);
            var batchResultSetGuids = batchRunAnalysisResults.Select(b => b.OriginalBatchResultSetGuid).Distinct().ToList();
            if (batchResultSetGuids.Contains(analysisResultSet.BatchResultSetGuid) == false)
                batchResultSetGuids.Add(analysisResultSet.BatchResultSetGuid);

            foreach (var batchResultSetGuid in batchResultSetGuids)
            {
                var batchResultSet = batchResultSetDao.Get(connection, projectGuid, batchResultSetGuid);
                var batchResultSetData = BatchResultSetSource.CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                analysisResultSetData.BatchResultSetData.Add(batchResultSetData);
            }

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

            var batchRunAnalysisResultsData = new List<BatchRunAnalysisResultData>();
            foreach (var batchRunAnalysisResult in batchRunAnalysisResults)
            {
                var batchRunAnalysisResultData = new BatchRunAnalysisResultData
                {
                    BatchRunAnalysisResult = batchRunAnalysisResult,
                    SequenceSampleInfoModifiable = sequenceSampleInfoModifiableDao.GetSequenceSampleInfoModifiable(connection, batchRunAnalysisResult.SequenceSampleInfoModifiableId),
                    ModifiableProcessingMethod = processingMethodModifiableDao.GetProcessingMethod(connection, batchRunAnalysisResult.ProcessingMethodModifiableId)
                };

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
