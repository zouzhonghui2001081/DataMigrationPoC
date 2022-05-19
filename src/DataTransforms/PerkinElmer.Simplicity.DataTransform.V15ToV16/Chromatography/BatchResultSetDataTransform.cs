using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;
using BatchResultDeviceModuleDetails = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchResultDeviceModuleDetails;
using BatchResultSet = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchResultSet;
using BatchResultSetData = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.BatchResultSetData;
using BatchRun = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.BatchRun;
using BatchRunData = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.BatchRunData;
using BatchRunData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.BatchRunData;
using DeviceDriverItemDetails = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.DeviceDriverItemDetails;
using NamedContent = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.NamedContent;
using SequenceSampleInfoBatchResult = PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.SequenceSampleInfoBatchResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class BatchResultSetDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
       
        internal static BatchResultSetData Transform(Data.Version15.Version.Data.Chromatography.BatchResultSetData batchResultSetData)
        {
            if (batchResultSetData == null) throw new ArgumentNullException(nameof(batchResultSetData));

            var batchResultSetData16 = new BatchResultSetData
            {
                ProjectGuid = batchResultSetData.ProjectGuid,
                BatchResultSet = BatchResultSet.Transform(batchResultSetData.BatchResultSet)
            };
            
            foreach (var deviceModuleDetails in batchResultSetData.DeviceModuleDetails)
                batchResultSetData16.DeviceModuleDetails.Add(BatchResultDeviceModuleDetails.Transform(deviceModuleDetails));

            foreach (var deviceDriverItemDetails in batchResultSetData.DeviceDriverItemDetails)
                batchResultSetData16.DeviceDriverItemDetails.Add(DeviceDriverItemDetails.Transform(deviceDriverItemDetails));

            foreach (var batchRun in batchResultSetData.BatchRuns)
                batchResultSetData16.BatchRuns.Add(Transform(batchRun));

            return batchResultSetData16;
        }

        internal static BatchRunData Transform(BatchRunData15 batchRunData)
        {
            if (batchRunData == null) return null;

            var batchRunData16 = new BatchRunData
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
