using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Shared
{
    internal class AuxChannelMetaDataJsonConverter : IJsonConverter<IAuxChannelMetaData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ResponseUnitKeyName = "ResponseUnit";
        private const string NameKeyName = "Name";

        public JObject ToJson(IAuxChannelMetaData instance)
        {
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
                {NameKeyName, new JValue(instance.Name)}
            };

            return jObject;
        }

        public IAuxChannelMetaData FromJson(JObject jObject)
        {
            var version = (int)jObject[VersionKeyName];

            if (version != CurrentVersion)
                throw new Exception("Unsupported serialized object version!");

            var auxChannelMetaData = DomainFactory.Create<IAuxChannelMetaData>();
            auxChannelMetaData.ResponseUnit = (string)jObject[ResponseUnitKeyName];
            auxChannelMetaData.Name = (string)jObject[NameKeyName];
            return auxChannelMetaData;
        }
    }
}