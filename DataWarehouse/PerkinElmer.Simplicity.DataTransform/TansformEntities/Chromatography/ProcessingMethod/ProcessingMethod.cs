using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod;
using ProcessingMethod15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.ProcessingMethod;
using ProcessingMethod16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.ProcessingMethod;


namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class ProcessingMethod
    {
        public static ProcessingMethod16 Transform(ProcessingMethod15 processingMethod)
        {
            if (processingMethod == null) return null;

            var processingMethod16 = new ProcessingMethod16
            {
                Id = processingMethod.Id,
                Guid = processingMethod.Guid,
                IsDefault = processingMethod.IsDefault,
                Name = processingMethod.Name,
                VersionNumber = processingMethod.VersionNumber,
                CreatedDate = processingMethod.CreatedDate,
                CreatedUserId = processingMethod.CreatedUserId,
                CreatedUserName = processingMethod.CreatedUserName,
                ModifiedDate = processingMethod.ModifiedDate,
                ModifiedUserId = processingMethod.ModifiedUserId,
                ModifiedUserName = processingMethod.ModifiedUserName,
                NumberOfLevels = processingMethod.NumberOfLevels,
                AmountUnits = processingMethod.AmountUnits,
                UnidentifiedPeakCalibrationType = processingMethod.UnidentifiedPeakCalibrationType,
                UnidentifiedPeakCalibrationFactor = processingMethod.UnidentifiedPeakCalibrationFactor,
                UnidentifiedPeakReferenceCompoundGuid = processingMethod.UnidentifiedPeakReferenceCompoundGuid,
                ModifiedFromOriginal = processingMethod.ModifiedFromOriginal,
                OriginalReadOnlyMethodGuid = processingMethod.OriginalReadOnlyMethodGuid,
                Description = processingMethod.Description,
                ReviewApproveState = processingMethod.ReviewApproveState,
                PdaApexOptimizedParameters = PdaApexOptimizedParameters.Transform(processingMethod.PdaApexOptimizedParameters),
                SuitabilityMethod = SuitabilityMethod.Transform(processingMethod.SuitabilityMethod)
            };
            if (processingMethod.ChannelMethods != null)
            {
                foreach (var channelMethod in processingMethod.ChannelMethods)
                    processingMethod16.ChannelMethods.Add(ChannelMethod.Transform(channelMethod));
            }

            if (processingMethod.Compounds != null)
            {
                foreach (var compound in processingMethod.Compounds)
                    processingMethod16.Compounds.Add(Compound.Transform(compound));
            }

            if (processingMethod.CompoundCalibrationResults != null)
            {
                foreach (var compoundCalibrationResult in processingMethod.CompoundCalibrationResults)
                    processingMethod16.CompoundCalibrationResults.Add(CompoundCalibrationResults.Transform(compoundCalibrationResult));
            }

            if (processingMethod.ProcessingDeviceMethods != null)
            {
                if (processingMethod16.ProcessingDeviceMethods == null)
                    processingMethod16.ProcessingDeviceMethods = new System.Collections.Generic.List<Data.Version16.DataEntities.Chromatography.ProcessingMethod.ProcessingDeviceMethod>();
                foreach (var processingDeviceMethod in processingMethod.ProcessingDeviceMethods)
                    processingMethod16.ProcessingDeviceMethods.Add(ProcessingDeviceMethod.Transform(processingDeviceMethod));
                    
            }

            if (processingMethod.SpectrumMethods != null)
            {
                foreach (var spectrumMethod in processingMethod.SpectrumMethods)
                    processingMethod16.SpectrumMethods.Add(SpectrumMethod.Transform(spectrumMethod));
            }

            if (processingMethod.CalibrationBatchRunInfos != null)
            {
                foreach (var calibrationBatchRunInfo in processingMethod.CalibrationBatchRunInfos)
                    processingMethod16.CalibrationBatchRunInfos.Add(CalibrationBatchRunInfo.Transform(calibrationBatchRunInfo));
            }

            return processingMethod16;
        }
    }
}
