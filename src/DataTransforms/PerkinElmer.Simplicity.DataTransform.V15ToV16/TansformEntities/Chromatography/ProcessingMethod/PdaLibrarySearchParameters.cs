using System.Collections.Generic;
using PdaLibrarySearchParameters15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibrarySearchParameters;
using PdaLibrarySearchParameters16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibrarySearchParameters;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class PdaLibrarySearchParameters
    {
        public static PdaLibrarySearchParameters16 Transform(
            PdaLibrarySearchParameters15 pdaLibrarySearchParameters)
        {
            if (pdaLibrarySearchParameters == null) return null;
            var pdaLibrarySearchParameters16 = new PdaLibrarySearchParameters16
            {
                Id = pdaLibrarySearchParameters.Id,
                ChannelMethodId = pdaLibrarySearchParameters.ChannelMethodId,
                MinWavelength = pdaLibrarySearchParameters.MinWavelength,
                MaxWavelength = pdaLibrarySearchParameters.MaxWavelength,
                MatchRetentionTimeWindow = pdaLibrarySearchParameters.MatchRetentionTimeWindow,
                MatchRetentionTimeWindowEnabled = pdaLibrarySearchParameters.MatchRetentionTimeWindowEnabled,
                BaselineCorrectionEnabled = pdaLibrarySearchParameters.BaselineCorrectionEnabled,
                HitDistanceThreshold = pdaLibrarySearchParameters.HitDistanceThreshold,
                PeakLibrarySearch = pdaLibrarySearchParameters.PeakLibrarySearch,
                UseWavelengthLimits = pdaLibrarySearchParameters.UseWavelengthLimits,
                MaxNumberOfResults = pdaLibrarySearchParameters.MaxNumberOfResults
            };
            if (pdaLibrarySearchParameters.SelectedLibraries == null) return pdaLibrarySearchParameters16;
            pdaLibrarySearchParameters16.SelectedLibraries = new List<Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod.PdaLibrarySearchSelectedLibraries>();
            foreach (var selectedLibrary in pdaLibrarySearchParameters.SelectedLibraries)
                pdaLibrarySearchParameters16.SelectedLibraries.Add(PdaLibrarySearchSelectedLibraries.Transform(selectedLibrary));
            return pdaLibrarySearchParameters16;
        }
    }
}
