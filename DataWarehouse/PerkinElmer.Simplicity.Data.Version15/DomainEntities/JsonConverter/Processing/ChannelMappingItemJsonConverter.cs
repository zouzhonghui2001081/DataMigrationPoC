using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class ChannelMappingItemJsonConverter : IJsonConverter<IChannelMappingItem>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string BatchRunGuidKeyName = "BatchRunGuid";
        private const string OriginalBatchRunGuidKeyName = "OriginalBatchRunGuid";
        private const string BatchRunChannelDescriptorKeyName = "BatchRunChannelDescriptor";
        private const string ProcessingMethodChannelGuidKeyName = "ProcessingMethodChannelGuid";
        private const string ProcessingMethodGuidKeyName = "ProcessingMethodGuid";

        public JObject ToJson(IChannelMappingItem instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {BatchRunChannelGuidKeyName, new JValue(instance.BatchRunChannelGuid)},
                {BatchRunGuidKeyName, new JValue(instance.BatchRunGuid)},
                {OriginalBatchRunGuidKeyName, new JValue(instance.OriginalBatchRunGuid)},
                {BatchRunChannelDescriptorKeyName, JsonConverterRegistry.GetConverter<IChromatographicChannelDescriptor>().ToJson(instance.BatchRunChannelDescriptor)},
                {ProcessingMethodChannelGuidKeyName,  new JValue(instance.ProcessingMethodChannelGuid)},
                {ProcessingMethodGuidKeyName, new JValue(instance.ProcessingMethodGuid) }
            };
        }

        public IChannelMappingItem FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var channelMappingItem = DomainFactory.Create<IChannelMappingItem>();
            channelMappingItem.BatchRunChannelGuid = (Guid) jObject[BatchRunChannelGuidKeyName];
            channelMappingItem.BatchRunGuid = (Guid)jObject[BatchRunGuidKeyName];
            channelMappingItem.OriginalBatchRunGuid = (Guid)jObject[OriginalBatchRunGuidKeyName];
            channelMappingItem.BatchRunChannelDescriptor = jObject[BatchRunChannelDescriptorKeyName].Type == JTokenType.Null ? 
                null: JsonConverterRegistry.GetConverter<IChromatographicChannelDescriptor>().FromJson((JObject)jObject[BatchRunChannelDescriptorKeyName]);
            channelMappingItem.ProcessingMethodChannelGuid = (Guid)jObject[ProcessingMethodChannelGuidKeyName];
            channelMappingItem.ProcessingMethodGuid = (Guid)jObject[ProcessingMethodGuidKeyName];
            return channelMappingItem;
        }
    }
}
