using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;
using PdaLibraryConfirmationParameters15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibraryConfirmationParameters;
using PdaLibraryConfirmationParameters16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibraryConfirmationParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaLibraryConfirmationParameters
    {
        public static PdaLibraryConfirmationParameters16 Transform(
            PdaLibraryConfirmationParameters15 pdaLibraryConfirmParameters)
        {
            if (pdaLibraryConfirmParameters == null) return null;
            var pdaLibraryConfirmationParameters16 = new PdaLibraryConfirmationParameters16
            {
                Id = pdaLibraryConfirmParameters.Id,
                ChannelMethodId = pdaLibraryConfirmParameters.ChannelMethodId,
                MinWavelength = pdaLibraryConfirmParameters.MinWavelength,
                MaxWavelength = pdaLibraryConfirmParameters.MaxWavelength,
                BaselineCorrectionEnabled = pdaLibraryConfirmParameters.BaselineCorrectionEnabled,
                HitDistanceThreshold = pdaLibraryConfirmParameters.HitDistanceThreshold
            };
            if (pdaLibraryConfirmParameters.SelectedLibraries == null) return pdaLibraryConfirmationParameters16;
            pdaLibraryConfirmationParameters16.SelectedLibraries = new List<PdaLibraryConfirmationSelectedLibraries>();
            foreach (var selectedLibrary in pdaLibraryConfirmParameters.SelectedLibraries)
                pdaLibraryConfirmationParameters16.SelectedLibraries.Add(PdaLibraryConfirmationSelectedLibrariesTransform.Transform(selectedLibrary));
            return pdaLibraryConfirmationParameters16;
        }
    }
}
