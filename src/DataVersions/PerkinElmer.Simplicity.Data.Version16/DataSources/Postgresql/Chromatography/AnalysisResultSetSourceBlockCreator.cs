using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class AnalysisResultSetSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersions SourceVersion => MigrationVersions.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AnalysisResultSetMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var analysisResultSetDao = new AnalysisResultSetDao();
                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var analysisResultSets = analysisResultSetDao.GetAll(connection, projectGuid);
                                foreach (var analysisResultSet in analysisResultSets)
                                {
                                    var analysisResultSetData =
                                        CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        analysisResultSetData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                                    bufferBlock.Post(analysisResultSetData);
                                }
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectSourceEntitiesParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var analysisResultSetIds = projectEntitiesParams.EntityGuids;
                                foreach (var analysisResultSetId in analysisResultSetIds)
                                {
                                    var analysisResultSet =
                                        analysisResultSetDao.Get(connection, projectGuid, analysisResultSetId);
                                    var analysisResultSetData =
                                        CreateAnalysisResultSetData(connection, projectGuid, analysisResultSet);
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        analysisResultSetData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, analysisResultSet.Guid.ToString(), EntityTypeConstants.AnalysisResultSet);
                                    bufferBlock.Post(analysisResultSetData);
                                }
                            }

                            break;
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
                var batchResultSetData = BatchResultSetSourceBlockCreator.CreateBatchResultSetData(connection, projectGuid, batchResultSet);
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

            analysisResultSetData.CompoundLibraryData = CompoundLibrarySourceBlockCreator.CreateCompoundLibraryData(connection, projectGuid, analysisResultSet.Guid);

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
