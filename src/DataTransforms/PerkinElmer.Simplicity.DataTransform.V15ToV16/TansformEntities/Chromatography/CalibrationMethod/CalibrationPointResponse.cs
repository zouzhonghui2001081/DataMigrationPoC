using CalibrationPointResponse15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod.CalibrationPointResponse;
using CalibrationPointResponse16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod.CalibrationPointResponse;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class CalibrationPointResponse
    {
        public static CalibrationPointResponse16 Transform(
            CalibrationPointResponse15 calibrationPointResponse)
        {
            if (calibrationPointResponse == null) return null;
            return new CalibrationPointResponse16
            {
                Id = calibrationPointResponse.Id,
                CompoundCalibrationResultsId = calibrationPointResponse.CompoundCalibrationResultsId,
                Level = calibrationPointResponse.Level,
                QuantifyUsingArea = calibrationPointResponse.QuantifyUsingArea,
                UseInternalStandard = calibrationPointResponse.UseInternalStandard,
                Area = calibrationPointResponse.Area,
                AreaRatio = calibrationPointResponse.AreaRatio,
                Height = calibrationPointResponse.Height,
                PeakNotFoundError = calibrationPointResponse.PeakNotFoundError,
                InternalStandardPeakNotFoundError = calibrationPointResponse.InternalStandardPeakNotFoundError,
                HeightRatio = calibrationPointResponse.HeightRatio,
                Excluded = calibrationPointResponse.Excluded,
                BatchRunGuid = calibrationPointResponse.BatchRunGuid,
                External = calibrationPointResponse.External,
                PeakAreaPercentage = calibrationPointResponse.PeakAreaPercentage,
                PointCalibrationFactor = calibrationPointResponse.PointCalibrationFactor,
                InvalidAmountError = calibrationPointResponse.InvalidAmountError,
                OutlierTestFailed = calibrationPointResponse.OutlierTestFailed,
                OutlierTestResult = calibrationPointResponse.OutlierTestResult,
                StandardAmountAdjustmentCoeff = calibrationPointResponse.StandardAmountAdjustmentCoeff,
                InternalStandardAmountAdjustmentCoeff = calibrationPointResponse.InternalStandardAmountAdjustmentCoeff
            };
        }
    }
}
