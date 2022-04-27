using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Common.Postgresql;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Source;
using PerkinElmer.Simplicity.Data.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class AnalysisResultSetSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AnalysisResultSetMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var analysisResultSetDao = new AnalysisResultSetDao();
                    var analysisResultSets = analysisResultSetDao.GetAll(connection, projectGuid);

                    foreach (var analysisResultSet in analysisResultSets)
                    {
                        var analysisResultSetData = CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                        analysisResultSetData.AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                        bufferBlock.Post(analysisResultSetData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get analysis result set source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });

            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AnalysisResultSetMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectGuid = parameters.Item1;
                    var analysisResultSetIds = parameters.Item2;
                    var analysisResultSetDao = new AnalysisResultSetDao();
                    foreach (var analysisResultSetId in analysisResultSetIds)
                    {
                        var analysisResultSet = analysisResultSetDao.Get(connection, projectGuid, analysisResultSetId);
                        var analysisResultSetData = CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                        analysisResultSetData.AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                        bufferBlock.Post(analysisResultSetData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get acquisition method source by project id and Analysis Result Set ids finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        internal static AnalysisResultSetMigrationData CreateAnalysisResultSetData(IDbConnection connection, Guid projectGuid,
            AnalysisResultSet analysisResultSet)
        {
            var analysisResultSetData = new AnalysisResultSetMigrationData();

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

            analysisResultSetData.ReviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid, analysisResultSet.Guid.ToString(), ReviewApproveEntityType.AnalysisResultSet);

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

                var calculatedChannelsData = new List<CalculatedChannelCompositeMigrationData>();
                var calculatedChannelDataListEntities = batchRunAnalysisResultDao.GetChannelDataByBatchRunAnalysisResultId(connection, batchRunAnalysisResult.Id);
                foreach (var calculatedChannelDataListEntity in calculatedChannelDataListEntities)
                {
                    var calculatedChannelCompositeData = new CalculatedChannelCompositeMigrationData
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
