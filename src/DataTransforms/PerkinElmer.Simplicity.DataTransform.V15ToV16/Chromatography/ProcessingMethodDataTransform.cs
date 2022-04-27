using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class ProcessingMethodDataTransform : TransformBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override ReleaseVersions FromReleaseVersion => ReleaseVersions.Version15;

        public override ReleaseVersions ToReleaseVersion => ReleaseVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var projectProcessingMethodTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(
                fromVersionData =>
                {
                    if (fromVersionData.ReleaseVersion != ReleaseVersions.Version15 ||
                        !(fromVersionData is Data.Version15.MigrationData.Chromatography.ProcessingMethodMigrationData processingMethodData))
                        throw new ArgumentException("From version data is incorrect!");
                    return Transform(processingMethodData);
                }, transformContext.BlockOption);
            projectProcessingMethodTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"project processing method transform complete with State{_.Status}");
            });
            return projectProcessingMethodTransformBlock;
        }

        internal static ProcessingMethodMigrationData Transform(Data.Version15.MigrationData.Chromatography.ProcessingMethodMigrationData processingMethodMigrationData)
        {
            if(processingMethodMigrationData == null) throw new ArgumentNullException(nameof(processingMethodMigrationData));
            var processingMethodData16 =  new ProcessingMethodMigrationData
            {
                ProjectGuid = processingMethodMigrationData.ProjectGuid,
                ProcessingMethod = ProcessingMethod.Transform(processingMethodMigrationData.ProcessingMethod)
            };
            if (processingMethodMigrationData.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in processingMethodMigrationData.AuditTrailLogs)
                    processingMethodData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            if (processingMethodMigrationData.ReviewApproveData != null)
                processingMethodData16.ReviewApproveData = ReviewApproveDataTransform.Transform(processingMethodMigrationData.ReviewApproveData);
            return processingMethodData16;
        }
    }
}
