using PdaLibrarySearchSelectedLibraries15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibrarySearchSelectedLibraries;
using PdaLibrarySearchSelectedLibraries16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibrarySearchSelectedLibraries;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaLibrarySearchSelectedLibraries
    {
        public static PdaLibrarySearchSelectedLibraries16 Transform(
            PdaLibrarySearchSelectedLibraries15 pdaLibrarySearchSelectedLibraries)
        {
            if (pdaLibrarySearchSelectedLibraries == null) return null;
            var pdaLibrarySearchSelectedLibraries16 = new PdaLibrarySearchSelectedLibraries16
            {
                Id = pdaLibrarySearchSelectedLibraries.Id,
                PdaLibrarySearchParameterId = pdaLibrarySearchSelectedLibraries.PdaLibrarySearchParameterId,
                SelectedLibraries = pdaLibrarySearchSelectedLibraries.SelectedLibraries
            };
            return pdaLibrarySearchSelectedLibraries16;
        }
    }
}
