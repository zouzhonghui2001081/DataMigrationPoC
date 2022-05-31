using SequenceSampleInfo15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.SequenceSampleInfo;
using SequenceSampleInfo16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.SequenceSampleInfo;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class SequenceSampleInfo
    {
        public static SequenceSampleInfo16 Transform(SequenceSampleInfo15 sequenceSampleInfo)
        {
            var sequenceSampleInfo16 = new SequenceSampleInfo16
            {
                SequenceId = sequenceSampleInfo.SequenceId,
                Id = sequenceSampleInfo.Id,
                Guid = sequenceSampleInfo.Guid,
                Selected = sequenceSampleInfo.Selected,
                SampleName = sequenceSampleInfo.SampleName,
                SampleId = sequenceSampleInfo.SampleId,
                UserComments = sequenceSampleInfo.UserComments,
                SampleType = sequenceSampleInfo.SampleType,
                NumberOfRepeats = sequenceSampleInfo.NumberOfRepeats,
                Level = sequenceSampleInfo.Level,
                Multiplier = sequenceSampleInfo.Multiplier,
                Divisor = sequenceSampleInfo.Divisor,
                UnknownAmountAdjustment = sequenceSampleInfo.UnknownAmountAdjustment,
                InternalStandardAmountAdjustment = sequenceSampleInfo.InternalStandardAmountAdjustment,
                BaselineCorrection = sequenceSampleInfo.BaselineCorrection,
                BaselineRunGuid = sequenceSampleInfo.BaselineRunGuid,
                //TODO: Research baseline run id logic.
                BaselineRunId = sequenceSampleInfo.BaselineRunId,
                RackCode = sequenceSampleInfo.RackCode,
                RackPosition = sequenceSampleInfo.RackPosition,
                PlateCode = sequenceSampleInfo.PlateCode,
                PlateCodeAsInteger = sequenceSampleInfo.PlateCodeAsInteger,
                PlateCodeAsIntegerDeviceName = sequenceSampleInfo.PlateCodeAsIntegerDeviceName,
                PlatePosition = sequenceSampleInfo.PlatePosition,
                PlatePositionAsInteger = sequenceSampleInfo.PlatePositionAsInteger,
                PlatePositionAsIntegerDeviceName = sequenceSampleInfo.PlatePositionAsIntegerDeviceName,
                VialPosition = sequenceSampleInfo.VialPosition,
                VialPositionAsInteger = sequenceSampleInfo.VialPositionAsInteger,
                VialPositionAsIntegerDeviceName = sequenceSampleInfo.VialPositionAsIntegerDeviceName,
                DestinationVial = sequenceSampleInfo.DestinationVial,
                DestinationVialAsInteger = sequenceSampleInfo.DestinationVialAsInteger,
                DestinationVialAsIntegerDeviceName = sequenceSampleInfo.DestinationVialAsIntegerDeviceName,
                InjectionVolume = sequenceSampleInfo.InjectionVolume,
                InjectionVolumeDeviceName = sequenceSampleInfo.InjectionVolumeDeviceName,
                InjectionType = sequenceSampleInfo.InjectionType,
                AcquisitionMethodName = sequenceSampleInfo.AcquisitionMethodName,
                AcquisitionMethodVersionNumber = sequenceSampleInfo.AcquisitionMethodVersionNumber,
                ProcessingMethodName = sequenceSampleInfo.ProcessingMethodName,
                ProcessingMethodVersionNumber = sequenceSampleInfo.ProcessingMethodVersionNumber,
                CalibrationCurveName = sequenceSampleInfo.CalibrationCurveName,
                InjectionPortAsInteger = sequenceSampleInfo.InjectionPortAsInteger,
                InjectionPortAsIntegerDeviceName = sequenceSampleInfo.InjectionPortAsIntegerDeviceName,
                InjectionPort = sequenceSampleInfo.InjectionPort,
                InjectionTypeAsInteger = sequenceSampleInfo.InjectionTypeAsInteger,
                InjectionTypeAsIntegerDeviceName = sequenceSampleInfo.InjectionTypeAsIntegerDeviceName,
                SampleAmount = sequenceSampleInfo.SampleAmount,
                DilutionFactor = sequenceSampleInfo.DilutionFactor,
                Addend = sequenceSampleInfo.Addend,
                NormalizationFactor = sequenceSampleInfo.NormalizationFactor,
                StandardAmountAdjustment = sequenceSampleInfo.StandardAmountAdjustment,
                SampleReportTemplate = sequenceSampleInfo.SampleReportTemplate,
                SummaryReportGroup = sequenceSampleInfo.SummaryReportGroup,
                SuitabilitySampleType = sequenceSampleInfo.SuitabilitySampleType
            };
            return sequenceSampleInfo16;
        }
    }
}
