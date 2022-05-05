using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
	public class BatchResultSetTargetBlockCreator : TargetBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetTypes TargetType => TargetTypes.Posgresql;

        public override MigrationVersion TargetVersion => MigrationVersion.Version15;

        public override ITargetBlock<MigrationDataBase> CreateTargetBlock(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
                {
                    if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.BatchResultSet) return;
                    if (versionData.MigrationVersion != MigrationVersion.Version15) return;
                    if (!(versionData is BatchResultSetMigrationData batchResultSetData)) return;

                    using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        CreateBatchResultSet(connection, batchResultSetData);
                        connection.Close();
                    }
                        
                }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create batch result set targets finished with status {_.Status}.");
            });

            return actionBlock;
        }

        internal static void CreateBatchResultSet(IDbConnection connection,  BatchResultSetMigrationData batchResultSetMigrationData)
        {
            var projectDao = new ProjectDao();
            var batchResultSetDao = new BatchResultSetDao();
            var batchRunDao = new BatchRunDao();
            var sequenceSampleInfoBatchResultDao = new SequenceSampleInfoBatchResultDao();
            var namedContentDao = new NamedContentDao();
            var streamDataBatchResultDao = new StreamDataBatchResultDao();
            var deviceDriverItemDetailsDao = new DeviceDriverItemDetailsDao();
            var batchResultDeviceModuleDetailsDao = new BatchResultDeviceModuleDetailsDao();

            var project = projectDao.GetProject(connection, batchResultSetMigrationData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));

            if(batchResultSetDao.IsExists(connection, batchResultSetMigrationData.ProjectGuid, batchResultSetMigrationData.BatchResultSet.Guid))
                return;

            batchResultSetMigrationData.BatchResultSet.ProjectId = project.Id;

            var batchResultSet = batchResultSetDao.CreateBatchResultSet(connection, batchResultSetMigrationData.BatchResultSet);
            var batchResultSetId = batchResultSet.Id;

            foreach (var deviceModuleDetail in batchResultSetMigrationData.DeviceModuleDetails)
                deviceModuleDetail.BatchResultSetId = batchResultSetId;
            batchResultDeviceModuleDetailsDao.Create(connection, batchResultSetMigrationData.DeviceModuleDetails.ToArray());

            foreach (var deviceDriverItemDetail in batchResultSetMigrationData.DeviceDriverItemDetails)
                deviceDriverItemDetail.BatchResultSetId = batchResultSetId;
            deviceDriverItemDetailsDao.Create(connection, batchResultSetMigrationData.DeviceDriverItemDetails.ToArray());

            var acquisitionMethods = new List<(Guid acquisistionMethodGuid, long id)>();
            var processingMethods = new List<(Guid processingMethodGuid, long id)>();

            foreach (var batchRunData in batchResultSetMigrationData.BatchRuns)
            {
                batchRunData.BatchRun.BatchResultSetId = batchResultSetId;

                //Sequence Sample Info
                if (batchRunData.SequenceSampleInfoBatchResult != null)
                {
                    batchRunData.SequenceSampleInfoBatchResult.BatchResultSetId = batchResultSetId;
                    sequenceSampleInfoBatchResultDao.CreateSequenceSampleInfo(connection, batchRunData.SequenceSampleInfoBatchResult);
                    batchRunData.BatchRun.SequenceSampleInfoBatchResultId = batchRunData.SequenceSampleInfoBatchResult.Id;
                }

                //Acquisition Method
                var acquisitionMethod = batchRunData.AcquisitionMethod;
                if (acquisitionMethods.Exists(a => a.acquisistionMethodGuid == acquisitionMethod.Guid))
                {
                    var acquisitionMethodValues =
                        acquisitionMethods.Find(a => a.acquisistionMethodGuid == acquisitionMethod.Guid);
                    batchRunData.BatchRun.AcquisitionMethodBatchResultId = acquisitionMethodValues.id;
                }
                else
                {
                   var acquisitionMethodId = AcquisitionMethodTargetBlockCreator.CreateAcquisitionMethod(connection, batchResultSet.Id, acquisitionMethod);
                   batchRunData.BatchRun.AcquisitionMethodBatchResultId = acquisitionMethodId;
                   acquisitionMethods.Add((acquisitionMethod.Guid, acquisitionMethodId));
                }

                //Processing Method
                var processingMethod = batchRunData.ProcessingMethod;
                if (processingMethods.Exists(p => p.processingMethodGuid == processingMethod.Guid))
                {
                    var processingMethodIdPair = processingMethods.Find(p => p.processingMethodGuid == processingMethod.Guid);
                    batchRunData.BatchRun.ProcessingMethodBatchResultId = processingMethodIdPair.id;
                }
                else
                {
                    var processingMethodId = ProcessingMethodTargetBlockCreator.CreateProcessingMethod(connection, batchResultSetId, processingMethod);
                    batchRunData.BatchRun.ProcessingMethodBatchResultId = processingMethodId;
                    processingMethods.Add((processingMethod.Guid, processingMethodId));
                }


                batchRunDao.Create(connection, batchRunData.BatchRun);

                //Name Content
                foreach (var namedContent in batchRunData.NamedContents)
                    namedContent.BatchRunId = batchRunData.BatchRun.Id;
                namedContentDao.Create(connection, batchRunData.NamedContents.ToArray());

                //Stream Data Batch Result
                using (var _ = connection.BeginTransaction())
                {
                    var manager = new NpgsqlLargeObjectManager((NpgsqlConnection)connection);
                    foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
                    {
                        streamDataBatchResult.BatchRunId = batchRunData.BatchRun.Id;
                        if (streamDataBatchResult.UseLargeObjectStream)
                        {
                            uint oid = manager.Create();
                            streamDataBatchResult.LargeObjectOid = oid;
                            var largeObjectStream = manager.OpenReadWrite(oid);
                            largeObjectStream.Write(streamDataBatchResult.YData, 0, streamDataBatchResult.YData.Length);
                            largeObjectStream.Close();
                            streamDataBatchResult.YData = null;
                        }
                        streamDataBatchResultDao.InsertStreamData(connection, streamDataBatchResult);
                    }
                }
                
            }
        }
    }
}
