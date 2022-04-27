using CompoundLibraryItem15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CompoundLibraryItem;
using CompoundLibraryItem16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CompoundLibraryItem;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class CompoundLibraryItem
    {
        public static CompoundLibraryItem16 Transform(CompoundLibraryItem15 compoundLibraryItem)
        {
            var compoundLibraryItem16 = new CompoundLibraryItem16
            {
                Id = compoundLibraryItem.Id,
                CompoundName = compoundLibraryItem.CompoundName,
                CompoundGuid = compoundLibraryItem.CompoundGuid,
                CreatedDate = compoundLibraryItem.CreatedDate,
                RetentionTime = compoundLibraryItem.RetentionTime,
                SpectrumAbsorbances = compoundLibraryItem.SpectrumAbsorbances,
                BaselineAbsorbances = compoundLibraryItem.BaselineAbsorbances,
                StartWavelength = compoundLibraryItem.StartWavelength,
                EndWavelength = compoundLibraryItem.EndWavelength,
                Step = compoundLibraryItem.Step,
                IsBaselineCorrected = compoundLibraryItem.IsBaselineCorrected
            };
            return compoundLibraryItem16;
        }
    }
}
