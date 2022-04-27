using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Common.Postgresql;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Targets;
using PerkinElmer.Simplicity.Data.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    public class AcquisitionMethodTarget : TargetBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetTypes TargetType => TargetTypes.Posgresql;

        public override ReleaseVersions TargetReleaseVersion => ReleaseVersions.Version15;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion15;

        public override ITargetBlock<MigrationDataBase> CreateTarget(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
                {
                    if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.AcqusitionMethod) return;
                    if (versionData.ReleaseVersion != ReleaseVersions.Version15) return;
                    if (!(versionData is AcqusitionMethodMigrationData acqusitionMethodData)) return;

                    using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                    {
                        if (connection.State != ConnectionState.Open) connection.Open();
                        CreateProjectAcquisitionMethod(connection, acqusitionMethodData.ProjectGuid, acqusitionMethodData.AcquisitionMethod);
                        ReviewApproveTarget.CreateReviewApproveEntity(connection, acqusitionMethodData.ReviewApproveData);
                        AuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext, acqusitionMethodData.AuditTrailLogs);
                        connection.Close();
                    }
                        
                }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create review approve targets finished with status {_.Status}.");
            });

            return actionBlock;
        }

        internal static long CreateProjectAcquisitionMethod(IDbConnection connection, Guid projectId,
            AcquisitionMethod acquisitionMethod)
        {
            var projectDao = new ProjectDao();
            var acquisitionMethodDao = new AcquisitionMethodDao();
            var deviceMethodDao = new DeviceMethodDao();
            var deviceModuleDao = new DeviceModuleDetailsDao();
            var expectedDeviceChannelDescriptorDao = new ExpectedDeviceChannelDescriptorDao();

            var project = projectDao.GetProject(connection, projectId);
            if (project == null) throw new ArgumentNullException(nameof(project));
            var acqusitionMethodInDb = acquisitionMethodDao.Get(connection, project.Guid, acquisitionMethod.Guid);
            //acqusition method with same Guid already in DB.
            if (acqusitionMethodInDb != null)
                return -1;

            acquisitionMethodDao.Create(connection, project.Guid, acquisitionMethod);
            var acquisitionMethodId = acquisitionMethodDao.GetAcquisitionMethodId(connection, project.Name, acquisitionMethod.MethodName);
            if (acquisitionMethodId == null) throw new ArgumentNullException(nameof(acquisitionMethodId));

            foreach (var deviceMethod in acquisitionMethod.DeviceMethods)
                deviceMethod.AcquisitionMethodId = acquisitionMethodId.Value;
            deviceMethodDao.Create(connection, acquisitionMethod.DeviceMethods);

            foreach (var deviceMethod in acquisitionMethod.DeviceMethods)
            {
                foreach (var deviceModule in deviceMethod.DeviceModules)
                    deviceModule.DeviceMethodId = deviceMethod.Id;

                foreach (var expectedDeviceChannelDescriptor in deviceMethod.ExpectedDeviceChannelDescriptors)
                    expectedDeviceChannelDescriptor.DeviceMethodId = deviceMethod.Id;

                deviceModuleDao.Create(connection, deviceMethod.DeviceModules);
                expectedDeviceChannelDescriptorDao.Create(connection, deviceMethod.ExpectedDeviceChannelDescriptors);
            }

            return acquisitionMethodId.Value;
        }

        public static long CreateAcquisitionMethod(IDbConnection connection, long batchResultSetId, AcquisitionMethod acquisitionMethod)
        {
            var acquisitionMethodDao = new AcquisitionMethodDao();
            var deviceMethodDao = new DeviceMethodDao();
            var deviceModuleDao = new DeviceModuleDetailsDao();
            var expectedDeviceChannelDescriptorDao = new ExpectedDeviceChannelDescriptorDao();

            acquisitionMethodDao.Create(connection, batchResultSetId, acquisitionMethod);
            var acquisitionMethodFromCreate = acquisitionMethodDao.GetAcquisitionMethodInfoForBatchResultSet(connection, batchResultSetId, acquisitionMethod.Guid);
            if (acquisitionMethodFromCreate == null) throw new ArgumentNullException(nameof(acquisitionMethodFromCreate));

            var acquisitionMethodId = acquisitionMethodFromCreate.Id;

            foreach (var deviceMethod in acquisitionMethod.DeviceMethods)
                deviceMethod.AcquisitionMethodId = acquisitionMethodId;
            deviceMethodDao.Create(connection, acquisitionMethod.DeviceMethods);

            foreach (var deviceMethod in acquisitionMethod.DeviceMethods)
            {
                foreach (var deviceModule in deviceMethod.DeviceModules)
                    deviceModule.DeviceMethodId = deviceMethod.Id;

                foreach (var expectedDeviceChannelDescriptor in deviceMethod.ExpectedDeviceChannelDescriptors)
                    expectedDeviceChannelDescriptor.DeviceMethodId = deviceMethod.Id;

                deviceModuleDao.Create(connection, deviceMethod.DeviceModules);
                expectedDeviceChannelDescriptorDao.Create(connection, deviceMethod.ExpectedDeviceChannelDescriptors);
            }

            return acquisitionMethodId;
        }
	}
}
