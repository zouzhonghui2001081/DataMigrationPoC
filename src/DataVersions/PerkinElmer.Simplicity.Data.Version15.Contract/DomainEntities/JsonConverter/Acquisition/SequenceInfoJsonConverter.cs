using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
    internal class SequenceInfoJsonConverter: IJsonConverter<ISequenceInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public ISequenceInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var sequenceInfo = DomainFactory.Create<ISequenceInfo>();
            sequenceInfo.Guid = (Guid)jObject[GuidKeyName];
            sequenceInfo.Name = (string)jObject[NameKeyName];
            sequenceInfo.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            sequenceInfo.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
            sequenceInfo.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            sequenceInfo.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            return sequenceInfo;
        }

        public JObject ToJson(ISequenceInfo instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
            };            
            return jObject;
        }
    }
}
