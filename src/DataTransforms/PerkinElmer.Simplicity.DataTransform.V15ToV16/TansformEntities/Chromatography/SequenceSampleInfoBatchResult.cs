using SequenceSampleInfoBatchResult15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.SequenceSampleInfoBatchResult;
using SequenceSampleInfoBatchResult16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.SequenceSampleInfoBatchResult;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class SequenceSampleInfoBatchResult
    {
        public static SequenceSampleInfoBatchResult16 Transform(
            SequenceSampleInfoBatchResult15 sequenceSampleInfoBatchResult)
        {
            var sequenceSampleInfoBatchResult16 = new SequenceSampleInfoBatchResult16
            {
                BatchResultSetId = sequenceSampleInfoBatchResult.BatchResultSetId,
                Id = sequenceSampleInfoBatchResult.Id,
                Guid = sequenceSampleInfoBatchResult.Guid,
                Selected = sequenceSampleInfoBatchResult.Selected,
                SampleName = sequenceSampleInfoBatchResult.SampleName,
                SampleId = sequenceSampleInfoBatchResult.SampleId,
                UserComments = sequenceSampleInfoBatchResult.UserComments,
                SampleType = sequenceSampleInfoBatchResult.SampleType,
                NumberOfRepeats = sequenceSampleInfoBatchResult.NumberOfRepeats,
                Level = sequenceSampleInfoBatchResult.Level,
                Multiplier = sequenceSampleInfoBatchResult.Multiplier,
                Divisor = sequenceSampleInfoBatchResult.Divisor,
                UnknownAmountAdjustment = sequenceSampleInfoBatchResult.UnknownAmountAdjustment,
                InternalStandardAmountAdjustment = sequenceSampleInfoBatchResult.InternalStandardAmountAdjustment,
                BaselineCorrection = sequenceSampleInfoBatchResult.BaselineCorrection,
                BaselineRunGuid = sequenceSampleInfoBatchResult.BaselineRunGuid,
                //TODO: Research baseline run id logic.
                BaselineRunId = sequenceSampleInfoBatchResult.BaselineRunId,
                RackCode = sequenceSampleInfoBatchResult.RackCode,
                RackPosition = sequenceSampleInfoBatchResult.RackPosition,
                PlateCode = sequenceSampleInfoBatchResult.PlateCode,
                PlateCodeAsInteger = sequenceSampleInfoBatchResult.PlateCodeAsInteger,
                PlateCodeAsIntegerDeviceName = sequenceSampleInfoBatchResult.PlateCodeAsIntegerDeviceName,
                PlatePosition = sequenceSampleInfoBatchResult.PlatePosition,
                PlatePositionAsInteger = sequenceSampleInfoBatchResult.PlatePositionAsInteger,
                PlatePositionAsIntegerDeviceName = sequenceSampleInfoBatchResult.PlatePositionAsIntegerDeviceName,
                VialPosition = sequenceSampleInfoBatchResult.VialPosition,
                VialPositionAsInteger = sequenceSampleInfoBatchResult.VialPositionAsInteger,
                VialPositionAsIntegerDeviceName = sequenceSampleInfoBatchResult.VialPositionAsIntegerDeviceName,
                DestinationVial = sequenceSampleInfoBatchResult.DestinationVial,
                DestinationVialAsInteger = sequenceSampleInfoBatchResult.DestinationVialAsInteger,
                DestinationVialAsIntegerDeviceName = sequenceSampleInfoBatchResult.DestinationVialAsIntegerDeviceName,
                InjectionVolume = sequenceSampleInfoBatchResult.InjectionVolume,
                InjectionVolumeDeviceName = sequenceSampleInfoBatchResult.InjectionVolumeDeviceName,
                InjectionType = sequenceSampleInfoBatchResult.InjectionType,
                AcquisitionMethodName = sequenceSampleInfoBatchResult.AcquisitionMethodName,
                AcquisitionMethodVersionNumber = sequenceSampleInfoBatchResult.AcquisitionMethodVersionNumber,
                ProcessingMethodName = sequenceSampleInfoBatchResult.ProcessingMethodName,
                ProcessingMethodVersionNumber = sequenceSampleInfoBatchResult.ProcessingMethodVersionNumber,
                CalibrationCurveName = sequenceSampleInfoBatchResult.CalibrationCurveName,
                InjectionPortAsInteger = sequenceSampleInfoBatchResult.InjectionPortAsInteger,
                InjectionPortAsIntegerDeviceName = sequenceSampleInfoBatchResult.InjectionPortAsIntegerDeviceName,
                InjectionPort = sequenceSampleInfoBatchResult.InjectionPort,
                InjectionTypeAsInteger = sequenceSampleInfoBatchResult.InjectionTypeAsInteger,
                InjectionTypeAsIntegerDeviceName = sequenceSampleInfoBatchResult.InjectionTypeAsIntegerDeviceName,
                SampleAmount = sequenceSampleInfoBatchResult.SampleAmount,
                DilutionFactor = sequenceSampleInfoBatchResult.DilutionFactor,
                Addend = sequenceSampleInfoBatchResult.Addend,
                NormalizationFactor = sequenceSampleInfoBatchResult.NormalizationFactor,
                StandardAmountAdjustment = sequenceSampleInfoBatchResult.StandardAmountAdjustment,
                SampleReportTemplate = sequenceSampleInfoBatchResult.SampleReportTemplate,
                SummaryReportGroup = sequenceSampleInfoBatchResult.SummaryReportGroup,
                SuitabilitySampleType = sequenceSampleInfoBatchResult.SuitabilitySampleType
            };
            return sequenceSampleInfoBatchResult16;
        }
    }
}
