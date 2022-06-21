using System;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    internal class AcquisitionMethodTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveAcquisitionMethod(AcqusitionMethodData acquisitionMethodData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            CreateProjectAcquisitionMethod(connection, acquisitionMethodData.ProjectGuid, acquisitionMethodData.AcquisitionMethod);
            EntityAssociatedReviewApproveTarget.CreateReviewApproveEntity(connection, acquisitionMethodData.ReviewApproveData);
            EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext.AuditTrailConnectionString, acquisitionMethodData.AuditTrailLogs);
            connection.Close();
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

        internal static long CreateAcquisitionMethod(IDbConnection connection, long batchResultSetId, AcquisitionMethod acquisitionMethod)
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
