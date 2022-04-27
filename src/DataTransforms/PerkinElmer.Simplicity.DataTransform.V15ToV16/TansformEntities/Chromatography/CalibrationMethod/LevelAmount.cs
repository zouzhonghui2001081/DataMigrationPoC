using LevelAmount15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod.LevelAmount;
using LevelAmount16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod.LevelAmount;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class LevelAmount
    {
        public static LevelAmount16 Transform(LevelAmount15 levelAmount)
        {
            if (levelAmount == null) return null;
            return new LevelAmount16
            {
                Id = levelAmount.Id,
                CompoundId = levelAmount.CompoundId,
                Level = levelAmount.Level,
                Amount = levelAmount.Amount
            };
        }
    }
}
