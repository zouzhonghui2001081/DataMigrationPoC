﻿using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Contracts.Transform;
using PerkinElmer.Simplicity.Data.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class AcquisitionMethodDataTransform : TransformBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override ReleaseVersions FromReleaseVersion => ReleaseVersions.Version15;

        public override ReleaseVersions ToReleaseVersion => ReleaseVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var acqusitionMethodTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(fromVersionData =>
            {
                if (fromVersionData.ReleaseVersion != ReleaseVersions.Version15 ||
                    !(fromVersionData is Data.Version15.MigrationData.Chromatography.AcqusitionMethodMigrationData acqusitionMethodData))
                    throw new ArgumentException("From version data is incorrect!");
                return Transform(acqusitionMethodData);
            }, transformContext.BlockOption);
            acqusitionMethodTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"acqusition method transform complete with State{_.Status}");
            });
            return acqusitionMethodTransformBlock;
        }

        internal static AcqusitionMethodMigrationData Transform(Data.Version15.MigrationData.Chromatography.AcqusitionMethodMigrationData acqusitionMethodMigrationData)
        {
            if (acqusitionMethodMigrationData == null) throw new ArgumentNullException(nameof(acqusitionMethodMigrationData));
            var acqusitionMethodData16 = new AcqusitionMethodMigrationData
            {
                ProjectGuid = acqusitionMethodMigrationData.ProjectGuid,
                AcquisitionMethod = AcquisitionMethod.Transform(acqusitionMethodMigrationData.AcquisitionMethod)
            };
            if (acqusitionMethodMigrationData.ReviewApproveData != null)
                acqusitionMethodData16.ReviewApproveData = ReviewApproveDataTransform.Transform(acqusitionMethodMigrationData.ReviewApproveData);
            if (acqusitionMethodMigrationData.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in acqusitionMethodMigrationData.AuditTrailLogs)
                    acqusitionMethodData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            return acqusitionMethodData16;
        }

    }
}
