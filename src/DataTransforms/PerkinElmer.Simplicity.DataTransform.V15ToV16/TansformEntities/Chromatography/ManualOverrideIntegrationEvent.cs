using ManualOverrideIntegrationEvent15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ManualOverrideIntegrationEvent;
using ManualOverrideIntegrationEvent16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ManualOverrideIntegrationEvent;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class ManualOverrideIntegrationEvent
    {
        public static ManualOverrideIntegrationEvent16 Transform(
            ManualOverrideIntegrationEvent15 integrationEvent)
        {
            if (integrationEvent == null) return null;
            return new ManualOverrideIntegrationEvent16
            {
                ManualOverrideMapId = integrationEvent.ManualOverrideMapId,
                Id = integrationEvent.Id,
                EventType = integrationEvent.EventType,
                EventId = integrationEvent.EventId,
                StartTime = integrationEvent.StartTime,
                EndTime = integrationEvent.EndTime,
                Value = integrationEvent.Value
            };
        }
    }
}
