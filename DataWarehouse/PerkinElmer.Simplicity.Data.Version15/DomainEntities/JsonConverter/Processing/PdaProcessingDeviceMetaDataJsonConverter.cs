using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class PdaProcessingDeviceMetaDataJsonConverter : IJsonConverter<IPdaProcessingDeviceMetaData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string AutoZeroKeyName = "AutoZero";
        
        public JObject ToJson(IPdaProcessingDeviceMetaData instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {AutoZeroKeyName, new JValue(instance.AutoZero)}
            };
        }

        public IPdaProcessingDeviceMetaData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaProcessingDeviceMetaData = DomainFactory.Create<IPdaProcessingDeviceMetaData>();
            pdaProcessingDeviceMetaData.AutoZero = (bool)jObject[AutoZeroKeyName];
            return pdaProcessingDeviceMetaData;
        }
    }
}
