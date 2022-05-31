using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;
using ProcessingMethodData = PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography.ProcessingMethodData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class ProcessingMethodDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      
        public static ProcessingMethodData Transform(Data.Version15.Contract.Version.Chromatography.ProcessingMethodData processingMethodData)
        {
            if(processingMethodData == null) throw new ArgumentNullException(nameof(processingMethodData));
            var processingMethodData16 =  new ProcessingMethodData
            {
                ProjectGuid = processingMethodData.ProjectGuid,
                ProcessingMethod = ProcessingMethod.Transform(processingMethodData.ProcessingMethod)
            };
            if (processingMethodData.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in processingMethodData.AuditTrailLogs)
                    processingMethodData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            if (processingMethodData.ReviewApproveData != null)
                processingMethodData16.ReviewApproveData = ReviewApproveDataTransform.Transform(processingMethodData.ReviewApproveData);
            return processingMethodData16;
        }
    }
}
