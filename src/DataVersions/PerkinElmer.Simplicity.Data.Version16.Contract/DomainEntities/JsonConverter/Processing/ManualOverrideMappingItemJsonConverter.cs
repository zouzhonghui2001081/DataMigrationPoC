using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class ManualOverrideMappingItemJsonConverter : IJsonConverter<IManualOverrideMappingItem>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string BatchRunGuidKeyName = "BatchRunGuid";
        private const string TimedIntegrationParametersKeyName = "TimedIntegrationParameters";

        public JObject ToJson(IManualOverrideMappingItem instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {BatchRunChannelGuidKeyName, new JValue(instance.BatchRunChannelGuid)},
                {BatchRunGuidKeyName, new JValue(instance.BatchRunGuid)},
                
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<IIntegrationEvent>(jObject, instance.TimedIntegrationParameters, TimedIntegrationParametersKeyName);
            return jObject;
        }

        public IManualOverrideMappingItem FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var manualOverrideMappingItem = DomainFactory.Create<IManualOverrideMappingItem>();

            manualOverrideMappingItem.BatchRunChannelGuid = (Guid)jObject[BatchRunChannelGuidKeyName];
            manualOverrideMappingItem.BatchRunGuid = (Guid)jObject[BatchRunGuidKeyName];
            manualOverrideMappingItem.TimedIntegrationParameters = JsonConverterHelper.GetListPropertyFromJson<IIntegrationEvent>(jObject, TimedIntegrationParametersKeyName);
            return manualOverrideMappingItem;
        }
    }
}
