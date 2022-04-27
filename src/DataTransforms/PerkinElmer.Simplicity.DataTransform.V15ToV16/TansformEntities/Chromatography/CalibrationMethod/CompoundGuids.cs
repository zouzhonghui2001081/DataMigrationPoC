using CompoundGuids15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod.CompoundGuids;
using CompoundGuids16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod.CompoundGuids;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.CalibrationMethod
{
    public class CompoundGuids
    {
        public static CompoundGuids16 Transform(CompoundGuids15 compoundGuids)
        {
            if (compoundGuids == null) return null;
            return new CompoundGuids16
            {
                Id = compoundGuids.Id,
                CompoundId = compoundGuids.CompoundId,
                CompoundGuid = compoundGuids.CompoundGuid

            };
        }
    }
}
