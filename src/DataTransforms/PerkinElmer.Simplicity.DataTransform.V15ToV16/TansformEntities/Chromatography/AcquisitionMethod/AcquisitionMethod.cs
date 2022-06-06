using System.Collections.Generic;
using AcqusitionMethod15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod.AcquisitionMethod;
using AcqusitionMethod16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.AcquisitionMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod
{
    public class AcquisitionMethod
    {
        public static AcqusitionMethod16 Transform(AcqusitionMethod15 acqusitionMethod)
        {
            var acqusitionMethod16 = new AcqusitionMethod16
            {
                Id = acqusitionMethod.Id,
                MethodName = acqusitionMethod.MethodName,
                VersionNumber = acqusitionMethod.VersionNumber,
                ReconciledRunTime = acqusitionMethod.ReconciledRunTime,
                CreateDate = acqusitionMethod.CreateDate,
                ModifyDate = acqusitionMethod.ModifyDate,
                CreateUserId = acqusitionMethod.CreateUserId,
                CreateUserName = acqusitionMethod.CreateUserName,
                ModifyUserId = acqusitionMethod.ModifyUserId,
                ModifyUserName = acqusitionMethod.ModifyUserName,
                MethodDevices = acqusitionMethod.MethodDevices,
                Guid = acqusitionMethod.Guid,
                ReviewApproveState = acqusitionMethod.ReviewApproveState
            };
            var deviceMethods = new List<Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod.DeviceMethod>();
            foreach (var deviceMethod in acqusitionMethod.DeviceMethods)
                deviceMethods.Add(DeviceMethod.Transform(deviceMethod));

            acqusitionMethod16.DeviceMethods = deviceMethods.ToArray();
            return acqusitionMethod16;
        }
    }
}
