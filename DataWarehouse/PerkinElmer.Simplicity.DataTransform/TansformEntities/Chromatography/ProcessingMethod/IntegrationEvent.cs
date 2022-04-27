using IntegrationEvent15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod.IntegrationEvent;
using IntegrationEvent16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod.IntegrationEvent;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod
{
    public class IntegrationEvent
    {
        public static IntegrationEvent16 Transform(IntegrationEvent15 integrationEvent)
        {
            if (integrationEvent == null) return null;
            return new IntegrationEvent16
            {
                ChannelMethodId = integrationEvent.ChannelMethodId,
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
