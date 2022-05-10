using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;
using BatchResultDeviceModuleDetails = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchResultDeviceModuleDetails;
using BatchResultSet = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchResultSet;
using BatchRun = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchRun;
using BatchRunData15 = PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography.BatchRunData;
using DeviceDriverItemDetails = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.DeviceDriverItemDetails;
using NamedContent = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.NamedContent;
using SequenceSampleInfoBatchResult = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.SequenceSampleInfoBatchResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class BatchResultSetDataTransform : TransformBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override MigrationVersion FromVersion => MigrationVersion.Version15;

        public override MigrationVersion ToVersion => MigrationVersion.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var batchResultSetTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(
                fromVersionData =>
                {
                    if (fromVersionData.MigrationVersion != MigrationVersion.Version15 ||
                        !(fromVersionData is Data.Version15.MigrationData.Chromatography.BatchResultSetMigrationData batchResultSetData))
                        throw new ArgumentException("From version data is incorrect!");
                    return Transform(batchResultSetData);
                }, transformContext.BlockOption);
            batchResultSetTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"batch result set transform complete with State{_.Status}");
            });
            return batchResultSetTransformBlock;
        }

        internal static BatchResultSetMigrationData Transform(Data.Version15.MigrationData.Chromatography.BatchResultSetMigrationData batchResultSetMigrationData)
        {
            if (batchResultSetMigrationData == null) throw new ArgumentNullException(nameof(batchResultSetMigrationData));

            var batchResultSetData16 = new BatchResultSetMigrationData
            {
                ProjectGuid = batchResultSetMigrationData.ProjectGuid,
                BatchResultSet = BatchResultSet.Transform(batchResultSetMigrationData.BatchResultSet),
                CreateBatchResultSet = batchResultSetMigrationData.CreateBatchResultSet
            };
            
            foreach (var deviceModuleDetails in batchResultSetMigrationData.DeviceModuleDetails)
                batchResultSetData16.DeviceModuleDetails.Add(BatchResultDeviceModuleDetails.Transform(deviceModuleDetails));

            foreach (var deviceDriverItemDetails in batchResultSetMigrationData.DeviceDriverItemDetails)
                batchResultSetData16.DeviceDriverItemDetails.Add(DeviceDriverItemDetails.Transform(deviceDriverItemDetails));

            foreach (var batchRun in batchResultSetMigrationData.BatchRuns)
                batchResultSetData16.BatchRuns.Add(Transform(batchRun));

            return batchResultSetData16;
        }

        internal static BatchRunMigrationData Transform(BatchRunData15 batchRunData)
        {
            if (batchRunData == null) return null;

            var batchRunData16 = new BatchRunMigrationData
            {
                BatchRun = BatchRun.Transform(batchRunData.BatchRun),
                AcquisitionMethod = AcquisitionMethod.Transform(batchRunData.AcquisitionMethod),
                SequenceSampleInfoBatchResult = SequenceSampleInfoBatchResult.Transform(batchRunData.SequenceSampleInfoBatchResult),
                ProcessingMethod = ProcessingMethod.Transform(batchRunData.ProcessingMethod),
                NamedContents = new List<Data.Version16.DataEntities.Chromatography.NamedContent>(),
                StreamDataBatchResults = new List<Data.Version16.DataEntities.Chromatography.StreamDataBatchResult>()
            };
            foreach (var namedContent in batchRunData.NamedContents)
                batchRunData16.NamedContents.Add(NamedContent.Transform(namedContent));

            foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
                batchRunData16.StreamDataBatchResults.Add(StreamDataBatchResult.Transform(streamDataBatchResult));

            return batchRunData16;
        }
    }
}
