using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod;
using AcqusitionMethodData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.AcqusitionMethodData;
using AcqusitionMethodData16 = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.AcqusitionMethodData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class AcquisitionMethodDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static AcqusitionMethodData16 Transform(AcqusitionMethodData15 acqusitionMethodData)
        {
            if (acqusitionMethodData == null) throw new ArgumentNullException(nameof(acqusitionMethodData));
            var acqusitionMethodData16 = new AcqusitionMethodData16
            {
                ProjectGuid = acqusitionMethodData.ProjectGuid,
                AcquisitionMethod = AcquisitionMethod.Transform(acqusitionMethodData.AcquisitionMethod)
            };
            if (acqusitionMethodData.ReviewApproveData != null)
                acqusitionMethodData16.ReviewApproveData = ReviewApproveDataTransform.Transform(acqusitionMethodData.ReviewApproveData);
            if (acqusitionMethodData.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in acqusitionMethodData.AuditTrailLogs)
                    acqusitionMethodData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            return acqusitionMethodData16;
        }

    }
}
