using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Version.Data;
using PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    internal class AcquisitionMethodSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version15DataBase> GetAcqusitionMethods(Guid projectGuid, bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var acquisitionMethodDao = new AcquisitionMethodDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var acquisitionMethods = acquisitionMethodDao.GetAll(connection, projectGuid);
                foreach (var acquisitionMethod in acquisitionMethods)
                {
                    acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                    var acqusitionMethodData = new AcqusitionMethodData
                    {
                        ProjectGuid = projectGuid,
                        AcquisitionMethod = acquisitionMethod,
                        ReviewApproveData = reviewApproveData,
                    };
                    if (isIncludeAuditTrail)
                        acqusitionMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod);
                    migrationEntities.Add(acqusitionMethodData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        public static IList<Version15DataBase> GetAcqusitionMethods(Guid projectGuid, IList<Guid> acquisitionMethodGuids, bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var acquisitionMethodDao = new AcquisitionMethodDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                foreach (var acquisitionMethodGuid in acquisitionMethodGuids)
                {
                    var acquisitionMethod = acquisitionMethodDao.Get(connection, projectGuid, acquisitionMethodGuid);
                    acquisitionMethod.DeviceMethods = GetAcquisitionMethodChildren(connection, acquisitionMethod.Id);
                    var reviewApproveData = EntityAssociatedReviewApproveSource.GetReviewApproveData(connection, projectGuid,
                        acquisitionMethod.Guid.ToString(), ReviewApproveEntityType.AcquisitionMethod);
                    var acqusitionMethodData = new AcqusitionMethodData
                    {
                        ProjectGuid = projectGuid,
                        AcquisitionMethod = acquisitionMethod,
                        ReviewApproveData = reviewApproveData,
                    };
                    if (isIncludeAuditTrail)
                        acqusitionMethodData.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(acquisitionMethod.Guid.ToString(), EntityTypeConstants.AcquisitionMethod);
                    migrationEntities.Add(acqusitionMethodData);
                }
                connection.Close();
            }

            return migrationEntities;
        }

        internal static AcquisitionMethod GetAcqusitionMethod(IDbConnection connection, long acquisitionMethodId)
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
                deviceMethod.ExpectedDeviceChannelDescriptors =
                    expectedDeviceChannelDescriptorDao.Get(connection, deviceMethod.Id);
            }

            return deviceMethods;
        }
    }
}
