using PdaLibraryConfirmationSelectedLibraries15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.PdaLibraryConfirmationSelectedLibraries;
using PdaLibraryConfirmationSelectedLibraries16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.PdaLibraryConfirmationSelectedLibraries;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaLibraryConfirmationSelectedLibrariesTransform
    {
        public static PdaLibraryConfirmationSelectedLibraries16 Transform(
            PdaLibraryConfirmationSelectedLibraries15 pdaLibraryConfirmationSelectedLibraries)
        {
            if (pdaLibraryConfirmationSelectedLibraries == null) return null;
            var pdaLibraryConfirmationSelectedLibraries16 = new PdaLibraryConfirmationSelectedLibraries16
            {
                Id = pdaLibraryConfirmationSelectedLibraries.Id,
                PdaLibraryConfirmationParameterId =
                    pdaLibraryConfirmationSelectedLibraries.PdaLibraryConfirmationParameterId,
                SelectedLibraries = pdaLibraryConfirmationSelectedLibraries.SelectedLibraries
            };
            return pdaLibraryConfirmationSelectedLibraries16;
        }
    }
}
