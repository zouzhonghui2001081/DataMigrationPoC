using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CompoundIdJsonConverter : IJsonConverter<ICompoundId>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string ChannelIndexKeyName = "ChannelIndex";

        public JObject ToJson(ICompoundId instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {ChannelIndexKeyName, new JValue(instance.ChannelIndex)},
            };
        }

        public ICompoundId FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundId = DomainFactory.Create<ICompoundId>();
           
            compoundId.Guid = (Guid)jObject[GuidKeyName];
            compoundId.Name = (string)jObject[NameKeyName];
            compoundId.ChannelIndex = (int)jObject[ChannelIndexKeyName];
            return compoundId;
        }
    }
}
