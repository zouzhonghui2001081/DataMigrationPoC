using CompCalibResultCoefficient15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod.CompCalibResultCoefficient;
using CompCalibResultCoefficient16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod.CompCalibResultCoefficient;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class CompCalibResultCoefficient
    {
        public static CompCalibResultCoefficient16 Transform(
            CompCalibResultCoefficient15 compCalibResultCoefficient)
        {
            if (compCalibResultCoefficient == null) return null;
            return new CompCalibResultCoefficient16
            {
                Id = compCalibResultCoefficient.Id,
                CompoundCalibrationResultsId = compCalibResultCoefficient.CompoundCalibrationResultsId,
                Coefficients = compCalibResultCoefficient.Coefficients
            };
        }

    }
}
