using SuitabilityMethod15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.SuitabilityMethod;
using SuitabilityMethod16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.SuitabilityMethod;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class SuitabilityMethod
    {
        public static SuitabilityMethod16 Transform(SuitabilityMethod15 suitabilityMethod)
        {
            if (suitabilityMethod == null) return null;
            return new SuitabilityMethod16
            {
                Id = suitabilityMethod.Id,
                ProcessingMethodId = suitabilityMethod.ProcessingMethodId,
                Enabled = suitabilityMethod.Enabled,
                SelectedPharmacopeiaType = suitabilityMethod.SelectedPharmacopeiaType,
                IsEfficiencyInPlates = suitabilityMethod.IsEfficiencyInPlates,
                ColumnLength = suitabilityMethod.ColumnLength,
                SignalToNoiseWindowStart = suitabilityMethod.SignalToNoiseWindowStart,
                SignalToNoiseWindowEnd = suitabilityMethod.SignalToNoiseWindowEnd,
                SignalToNoiseEnabled = suitabilityMethod.SignalToNoiseEnabled,
                //TODO : Default value for this property
                PerformAdditionalSearchForNoiseWindow = false,

                AnalyzeAdjacentPeaks = suitabilityMethod.AnalyzeAdjacentPeaks,
                CompoundPharmacopeiaDefinitions = suitabilityMethod.CompoundPharmacopeiaDefinitions,
                VoidTimeType = suitabilityMethod.VoidTimeType,
                VoidTimeCustomValueInSeconds = suitabilityMethod.VoidTimeCustomValueInSeconds
            };
        }
    }
}
