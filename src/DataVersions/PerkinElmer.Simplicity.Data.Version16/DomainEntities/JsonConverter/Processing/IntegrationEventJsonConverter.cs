using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class IntegrationEventJsonConverter : IJsonConverter<IIntegrationEvent>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string EndTimeKeyName = "EndTime";
        private const string ValueKeyName = "Value";
        private const string EventTypeKeyName = "EventType";
        private const string StartTimeKeyName = "StartTime";
        private const string EventIdKeyName = "EventId";

        public JObject ToJson(IIntegrationEvent instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {EndTimeKeyName, new JValue(instance.EndTime)},
                {ValueKeyName, new JValue(instance.Value)},
                {EventTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.EventType, new StringEnumConverter()))},
                {StartTimeKeyName, new JValue(instance.StartTime)},
                {EventIdKeyName, new JValue(instance.EventId)}
            };
        }

        public IIntegrationEvent FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var integrationEvent = DomainFactory.Create<IIntegrationEvent>();

            integrationEvent.EndTime = (double?)jObject[EndTimeKeyName];
            integrationEvent.Value = (double?)jObject[ValueKeyName];
            integrationEvent.EventType = JsonConvert.DeserializeObject<IntegrationEventType>((string)jObject[EventTypeKeyName]);
            integrationEvent.StartTime = (double)jObject[StartTimeKeyName];
            integrationEvent.EventId = (int)jObject[EventIdKeyName];
            return integrationEvent;
        }
    }
}
