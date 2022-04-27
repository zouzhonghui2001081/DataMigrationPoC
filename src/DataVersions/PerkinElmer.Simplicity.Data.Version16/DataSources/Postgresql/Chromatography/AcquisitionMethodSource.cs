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
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class AcquisitionMethodSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AcqusitionMethodMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var acquisitionMethodDao = new AcquisitionMethodDao();
                    var acquisitionMethods = acquisitionMethodDao.GetAll(connection, projectGuid);
                    foreach (var acquisitionMethod in acquisitionMethods)
                    {
                        acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                        var acqusitionMethodData = new AcqusitionMethodMigrationData
                        {
                            ProjectGuid = projectGuid,
                            AcquisitionMethod = acquisitionMethod,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod)
                        };
                        bufferBlock.Post(acqusitionMethodData);
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

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<AcqusitionMethodMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectGuid = parameters.Item1;
                    var acquisitionMethodIds = parameters.Item2;
                    var acquisitionMethodDao = new AcquisitionMethodDao();
                    foreach (var acquisitionMethodId in acquisitionMethodIds)
                    {
                        var acquisitionMethod = acquisitionMethodDao.Get(connection, projectGuid, acquisitionMethodId);
                        acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                        var reviewApproveData = ReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                            acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                        var acqusitionMethodData = new AcqusitionMethodMigrationData
                        {
                            ProjectGuid = projectGuid,
                            AcquisitionMethod = acquisitionMethod,
                            ReviewApproveData = reviewApproveData,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod)
                        };
                        bufferBlock.Post(acqusitionMethodData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get acquisition methods source by project id and acquisition method ids finished with status {_.Status}.");
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
