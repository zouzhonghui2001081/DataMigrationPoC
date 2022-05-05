using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class AcquisitionMethodSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersions SourceVersion => MigrationVersions.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AcqusitionMethodMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var acquisitionMethodDao = new AcquisitionMethodDao();

                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var acquisitionMethods = acquisitionMethodDao.GetAll(connection, projectGuid);
                                foreach (var acquisitionMethod in acquisitionMethods)
                                {
                                    acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                                    var acqusitionMethodData = new AcqusitionMethodMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        AcquisitionMethod = acquisitionMethod,
                                        ReviewApproveData = reviewApproveData
                                    };
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        acqusitionMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod);
                                    bufferBlock.Post(acqusitionMethodData);
                                }
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectEntitiesSourceParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var acquisitionMethodIds = projectEntitiesParams.EntityGuids;
                                foreach (var acquisitionMethodId in acquisitionMethodIds)
                                {
                                    var acquisitionMethod = acquisitionMethodDao.Get(connection, projectGuid, acquisitionMethodId);
                                    acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                                        acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                                    var acqusitionMethodData = new AcqusitionMethodMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        AcquisitionMethod = acquisitionMethod,
                                        ReviewApproveData = reviewApproveData
                                    };
                                    if (posgresqlContext.IsMigrateAuditTrail)
                                        acqusitionMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(posgresqlContext, acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod);
                                    bufferBlock.Post(acqusitionMethodData);
                                }
                            }
                            break;
                    }
                    

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get acquisition method source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public static AcquisitionMethod GetAcqusitionMethod(IDbConnection connection, long acquisitionMethodId)
        {
            var acquisitionMethodDao = new AcquisitionMethodDao();
            var acquisitionMethod = acquisitionMethodDao.GetAcquisitionMethodInfoByAcquisitionMethodId(connection, acquisitionMethodId);
            if (acquisitionMethod != null)
            {
                acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethodId);
            }
            return acquisitionMethod;
        }

        private static DeviceMethod[] GetAcquisitionMethodChildren(IDbConnection connection, long acquisitionMethodId)
        {
            var deviceMethodDao = new DeviceMethodDao();

            // Select DeviceMethod
            var deviceMethods = deviceMethodDao.GetMethodDevices(connection, acquisitionMethodId);
            var deviceModuleDetailsDao = new DeviceModuleDetailsDao();
            var expectedDeviceChannelDescriptorDao = new ExpectedDeviceChannelDescriptorDao();

            // Select DeviceModuleDetails
            foreach (var deviceMethod in deviceMethods)
            {
                deviceMethod.DeviceModules = deviceModuleDetailsDao.GetDeviceModules(connection, deviceMethod.Id);
                deviceMethod.ExpectedDeviceChannelDescriptors = expectedDeviceChannelDescriptorDao.Get(connection, deviceMethod.Id);
            }

            return deviceMethods;
        }
    }
}
