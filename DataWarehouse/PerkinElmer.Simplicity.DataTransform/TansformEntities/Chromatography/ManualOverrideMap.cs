using ManualOverrideMap15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ManualOverrideMap;
using ManualOverrideMap16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ManualOverrideMap;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class ManualOverrideMap
    {
        public static ManualOverrideMap16 Transform(ManualOverrideMap15 manualOverrideMap)
        {
            if (manualOverrideMap == null) return null;
            var manualOverrideMap16 = new ManualOverrideMap16
            {
                Id = manualOverrideMap.Id,
                AnalysisResultSetId = manualOverrideMap.AnalysisResultSetId,
                BatchRunChannelGuid = manualOverrideMap.BatchRunChannelGuid,
                BatchRunGuid = manualOverrideMap.BatchRunGuid
            };
            foreach (var manualOverrideIntegrationEvent in manualOverrideMap.ManualOverrideIntegrationEvents)
                manualOverrideMap16.ManualOverrideIntegrationEvents.Add(
                    ManualOverrideIntegrationEvent.Transform(manualOverrideIntegrationEvent));
            return manualOverrideMap16;
        }

    }
}
