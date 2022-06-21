using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
    public class PersistableJsonConverter : IJsonConverter<IPersistable>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string NameKeyName = "Name";
        private const string GuidKeyName = "Guid";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public JObject ToJson(IPersistable instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {NameKeyName, new JValue(instance.Name)},
                {GuidKeyName, new JValue(instance.Guid)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},

            };
        }

        public IPersistable FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IPersistable>();
            instance.Name = (string)jObject[NameKeyName];
            instance.Guid = (Guid)jObject[GuidKeyName];
            instance.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
            instance.CreatedByUser = JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            instance.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            instance.ModifiedByUser = JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);

            return instance;
        }
    }
}
