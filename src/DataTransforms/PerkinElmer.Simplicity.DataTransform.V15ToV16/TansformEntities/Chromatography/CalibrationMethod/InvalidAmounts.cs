using InvalidAmounts15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod.InvalidAmounts;
using InvalidAmounts16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod.InvalidAmounts;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class InvalidAmounts
    {
        public static InvalidAmounts16 Transform(InvalidAmounts15 invalidAmounts)
        {
            if (invalidAmounts == null) return null;
            return new InvalidAmounts16
            {
                Id = invalidAmounts.Id,
                CompoundCalibrationResultsId = invalidAmounts.CompoundCalibrationResultsId,
                Amount = invalidAmounts.Amount
            };
        }
    }
}
