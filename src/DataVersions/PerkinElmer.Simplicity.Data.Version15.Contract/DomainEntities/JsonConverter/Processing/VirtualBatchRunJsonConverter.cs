using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class VirtualBatchRunJsonConverter : IJsonConverter<IVirtualBatchRun>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string OriginalBatchRunKeyName = "OriginalBatchRun";
        private const string OriginalBatchResultSetInfoKeyName = "OriginalBatchResultSetInfo";
        private const string ModifiableProcessingMethodGuidKeyName = "ModifiableProcessingMethodGuid";
        private const string ModifiableBatchRunInfoKeyName = "ModifiableBatchRunInfo";
        private const string CalculatedChannelDataKeyName = "CalculatedChannelData";

        public JObject ToJson(IVirtualBatchRun instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {OriginalBatchRunKeyName, JsonConverterRegistry.GetConverter<IBatchRun>().ToJson(instance.OriginalBatchRun)},
                {OriginalBatchResultSetInfoKeyName, JsonConverterRegistry.GetConverter<IBatchResultSetInfo>().ToJson(instance.OriginalBatchResultSetInfo)},
                {ModifiableProcessingMethodGuidKeyName, new JValue(instance.ModifiableProcessingMethodGuid)},
                {ModifiableBatchRunInfoKeyName, JsonConverterRegistry.GetConverter<IBatchRunInfo>().ToJson(instance.ModifiableBatchRunInfo)}
            };
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.CalculatedBatchRunChannelGuids, CalculatedChannelDataKeyName);
            return jObject;
        }

        public IVirtualBatchRun FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var virtualBatchRun = DomainFactory.Create<IVirtualBatchRun>();

            virtualBatchRun.OriginalBatchRun = jObject[OriginalBatchRunKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<IBatchRun>().FromJson((JObject)jObject[OriginalBatchRunKeyName]);
            virtualBatchRun.OriginalBatchResultSetInfo = jObject[OriginalBatchResultSetInfoKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<IBatchResultSetInfo>().FromJson((JObject)jObject[OriginalBatchResultSetInfoKeyName]);
            virtualBatchRun.ModifiableProcessingMethodGuid = (Guid)jObject[ModifiableProcessingMethodGuidKeyName];
            virtualBatchRun.ModifiableBatchRunInfo = jObject[ModifiableBatchRunInfoKeyName].Type == JTokenType.Null ? 
                null :JsonConverterRegistry.GetConverter<IBatchRunInfo>().FromJson((JObject)jObject[ModifiableBatchRunInfoKeyName]);
            virtualBatchRun.CalculatedBatchRunChannelGuids = JsonConverterHelper.GetArrayPropertyFromJson<Guid>(jObject, CalculatedChannelDataKeyName);
            return virtualBatchRun;
        }
    }
}
