using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class IntegrationEventErrorJsonConverter : IJsonConverter<IIntegrationEventError>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ErrorMessageKeyName = "ErrorMessage";
        private const string ErrorCodeKeyName = "ErrorCode";
        private const string ManualEventKeyName = "ManualEvent";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string EndTimeKeyName = "EndTime";
        private const string ValueKeyName = "Value";
        private const string EventTypeKeyName = "EventType";
        private const string StartTimeKeyName = "StartTime";
        private const string EventIdKeyName = "EventId";

        public JObject ToJson(IIntegrationEventError instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ErrorMessageKeyName, new JValue(instance.ErrorMessage)},
                {ErrorCodeKeyName, new JValue(JsonConvert.SerializeObject(instance.ErrorCode, new StringEnumConverter()))},
                {ManualEventKeyName, new JValue(instance.ManualEvent)},
                {BatchRunChannelGuidKeyName, new JValue(instance.BatchRunChannelGuid)},
                {EndTimeKeyName, new JValue(instance.EndTime)},
                {ValueKeyName, new JValue(instance.Value)},
                {EventTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.EventType, new StringEnumConverter()))},
                {StartTimeKeyName, new JValue(instance.StartTime)},
                {EventIdKeyName, new JValue(instance.EventId)}
            };
        }

        public IIntegrationEventError FromJson(JObject jObject)
        {
            if(jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var integrationEventError = DomainFactory.Create<IIntegrationEventError>();

            integrationEventError.ErrorMessage = (string)jObject[ErrorMessageKeyName];
            integrationEventError.ErrorCode = JsonConvert.DeserializeObject<ErrorCodes>((string)jObject[ErrorCodeKeyName]); 
            integrationEventError.ManualEvent = (bool)jObject[ManualEventKeyName];
            integrationEventError.BatchRunChannelGuid = (Guid)jObject[BatchRunChannelGuidKeyName];
            integrationEventError.EndTime = (double?)jObject[EndTimeKeyName];
            integrationEventError.Value = (double?)jObject[ValueKeyName];
            integrationEventError.EventType = JsonConvert.DeserializeObject<IntegrationEventType>((string)jObject[EventTypeKeyName]);
            integrationEventError.StartTime = (double)jObject[StartTimeKeyName];
            integrationEventError.EventId = (int)jObject[EventIdKeyName];
            return integrationEventError;
        }
    }
}
