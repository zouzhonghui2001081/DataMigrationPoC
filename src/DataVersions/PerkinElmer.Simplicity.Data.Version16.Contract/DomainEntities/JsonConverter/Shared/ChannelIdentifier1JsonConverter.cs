using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
    public class ChannelIdentifier1JsonConverter: IJsonConverter<IChannelIdentifier1>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ChannelIndexKeyName = "ChannelIndex";
        private const string AuxiliaryKeyName = "Auxiliary";

        public JObject ToJson(IChannelIdentifier1 instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ChannelIndexKeyName, new JValue(instance.ChannelIndex)},
                {AuxiliaryKeyName, new JValue(instance.Auxiliary)},
            };
        }

        public IChannelIdentifier1 FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IChannelIdentifier1>();
            instance.ChannelIndex = (int)jObject[ChannelIndexKeyName];
            instance.Auxiliary = (bool)jObject[AuxiliaryKeyName];

            return instance;
        }
    }
}
