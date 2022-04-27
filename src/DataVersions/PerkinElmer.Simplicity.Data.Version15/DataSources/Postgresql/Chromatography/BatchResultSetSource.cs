using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
	public class BatchResultSetSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version15;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion15;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateProjectSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<BatchResultSetMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var batchResultSetDao = new BatchResultSetDao();
                    var batchResultSets = batchResultSetDao.GetAll(connection, projectGuid);

                    foreach (var batchResultSet in batchResultSets)
                    {
                        var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                        bufferBlock.Post(batchResultSetData);
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

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateEntitiesSource(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<BatchResultSetMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectGuid = parameters.Item1;
                    var batchResultSetIds = parameters.Item2;
                    var batchResultSetDao = new BatchResultSetDao();
                    foreach (var batchResultSetId in batchResultSetIds)
                    {
                        var batchResultSet = batchResultSetDao.Get(connection, projectGuid, batchResultSetId);
                        var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                        batchResultSetData.ProjectGuid = projectGuid;
                        bufferBlock.Post(batchResultSetData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get batch result set by project id and batch result set ids finished with status {_.Status}.");
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
                var batchRunData = new BatchRunData
                {
                    BatchRun = batchRun,
                    AcquisitionMethod = AcquisitionMethodSource.GetAcqusitionMethod(connection, batchRun.AcquisitionMethodBatchResultId),
                    SequenceSampleInfoBatchResult = sequenceSampleInfoDao.GetSequenceSampleInfoBatchResultById(connection, batchRun.SequenceSampleInfoBatchResultId),
                    ProcessingMethod = processingMethodBatchResultDao.GetProcessingMethodById(connection, batchRun.ProcessingMethodBatchResultId),
                    NamedContents = namedContentDao.Get(connection, batchRun.Id),
                    StreamDataBatchResults = streamDataBatchResultDao.GetStreamInfo(connection, batchRun.Guid)
                };
                using (var _ = connection.BeginTransaction())
                {
                    foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
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
