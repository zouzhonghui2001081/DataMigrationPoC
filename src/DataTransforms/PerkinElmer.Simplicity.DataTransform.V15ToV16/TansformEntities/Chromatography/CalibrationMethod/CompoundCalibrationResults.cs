using CompoundCalibrationResults15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod.CompoundCalibrationResults;
using CompoundCalibrationResults16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod.CompoundCalibrationResults;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class CompoundCalibrationResults
    {
        public static CompoundCalibrationResults16 Transform(
            CompoundCalibrationResults15 compoundCalibrationResults)
        {
            if (compoundCalibrationResults == null) return null;
            var compoundCalibrationResults16 = new CompoundCalibrationResults16
            {
                Id = compoundCalibrationResults.Id,
                ProcessingMethodId = compoundCalibrationResults.ProcessingMethodId,
                NotEnoughLevelsFoundError = compoundCalibrationResults.NotEnoughLevelsFoundError,
                InvalidAmountError = compoundCalibrationResults.InvalidAmountError,
                RegressionType = compoundCalibrationResults.RegressionType,
                Coefficients = compoundCalibrationResults.Coefficients,
                RSquare = compoundCalibrationResults.RSquare,
                RelativeStandardErrorValue = compoundCalibrationResults.RelativeStandardErrorValue,
                Guid = compoundCalibrationResults.Guid,
                Name = compoundCalibrationResults.Name,
                ChannelIndex = compoundCalibrationResults.ChannelIndex,
                ConfLimitTestResult = compoundCalibrationResults.ConfLimitTestResult,
                RelativeStandardDeviationPercent = compoundCalibrationResults.RelativeStandardDeviationPercent,
                CorrelationCoefficient = compoundCalibrationResults.CorrelationCoefficient
            };
            if (compoundCalibrationResults.CalibrationPointResponses != null)
            {
                foreach (var calibrationPointResponse in compoundCalibrationResults.CalibrationPointResponses)
                    compoundCalibrationResults16.CalibrationPointResponses.Add(CalibrationPointResponse.Transform(calibrationPointResponse));
            }

            if (compoundCalibrationResults.CompCalibResultCoefficients != null)
            {
                foreach (var compCalibResultCoefficient in compoundCalibrationResults.CompCalibResultCoefficients)
                    compoundCalibrationResults16.CompCalibResultCoefficients.Add(CompCalibResultCoefficient.Transform(compCalibResultCoefficient));
            }

            if (compoundCalibrationResults.InvalidAmounts != null)
            {
                foreach (var invalidAmount in compoundCalibrationResults.InvalidAmounts)
                    compoundCalibrationResults16.InvalidAmounts.Add(InvalidAmounts.Transform(invalidAmount));
            }
            return compoundCalibrationResults16;
        }
    }
}
