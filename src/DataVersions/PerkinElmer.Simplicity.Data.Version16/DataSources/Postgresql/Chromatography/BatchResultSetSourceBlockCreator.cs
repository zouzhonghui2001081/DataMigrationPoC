﻿using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class BatchResultSetSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceType SourceType => SourceType.Posgresql;

        public override MigrationVersion SourceVersion => MigrationVersion.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<BatchResultSetMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var batchResultSetDao = new BatchResultSetDao();

                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var batchResultSets = batchResultSetDao.GetAll(connection, projectGuid);
                                foreach (var batchResultSet in batchResultSets)
                                {
                                    var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                                    bufferBlock.Post(batchResultSetData);
                                }
                            }
                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectEntitiesSourceParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var batchResultSetIds = projectEntitiesParams.EntityGuids;
                                foreach (var batchResultSetId in batchResultSetIds)
                                {
                                    var batchResultSet = batchResultSetDao.Get(connection, projectGuid, batchResultSetId);
                                    var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                                    batchResultSetData.ProjectGuid = projectGuid;
                                    bufferBlock.Post(batchResultSetData);
                                }
                            }
                            break;
                    }
                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get batch result set source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        internal static BatchResultSetMigrationData CreateBatchResultSetData(IDbConnection connection, Guid projectId, BatchResultSet batchResultSet)
        {
            if (batchResultSet == null) return null;

            var batchRunDao = new BatchRunDao();
            var sequenceSampleInfoDao = new SequenceSampleInfoBatchResultDao();
            var processingMethodBatchResultDao = new ProcessingMethodBatchResultDao();
            var namedContentDao = new NamedContentDao();
            var streamDataBatchResultDao = new StreamDataBatchResultDao();
            var batchResultDeviceModuleDetailsDao = new BatchResultDeviceModuleDetailsDao();
            var deviceDriverItemDetailsDao = new DeviceDriverItemDetailsDao();

            var batchResultSetData = new BatchResultSetMigrationData { ProjectGuid = projectId, BatchResultSet = batchResultSet };

            var batchRuns = batchRunDao.GetBatchRunsByBatchResultSetIdAndSequenceSampleInfoBatchResultId(connection, batchResultSet.Id);
            foreach (var batchRun in batchRuns)
            {
                var batchRunData = new BatchRunMigrationData
                {
                    BatchRun = batchRun,
                    AcquisitionMethod = AcquisitionMethodSourceBlockCreator.GetAcqusitionMethod(connection, batchRun.AcquisitionMethodBatchResultId),
                    SequenceSampleInfoBatchResult = sequenceSampleInfoDao.GetSequenceSampleInfoBatchResultById(connection, batchRun.SequenceSampleInfoBatchResultId),
                    ProcessingMethod = processingMethodBatchResultDao.GetProcessingMethodById(connection, batchRun.ProcessingMethodBatchResultId),
                    NamedContents = namedContentDao.Get(connection, batchRun.Id),
                    StreamDataBatchResults = streamDataBatchResultDao.GetStreamInfo(connection, batchRun.Guid)
                };
                foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
                {
                    using (var _ = connection.BeginTransaction())
                    {
                        var valuesStreamData = streamDataBatchResultDao.GetStreamData(connection, batchRun.Guid, streamDataBatchResult.DeviceId, streamDataBatchResult.StreamIndex);
                        streamDataBatchResult.YData = valuesStreamData.data;
                    }
                }

                batchResultSetData.BatchRuns.Add(batchRunData);
            }

            batchResultSetData.DeviceModuleDetails = batchResultDeviceModuleDetailsDao.GetDeviceModules(connection, batchResultSet.Id);
            batchResultSetData.DeviceDriverItemDetails = deviceDriverItemDetailsDao.Get(connection, batchResultSet.Id);

            return batchResultSetData;
        }
    }
}
