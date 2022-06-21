using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Acquisition
{
	internal class BatchResultSetInfoJsonConverter : IJsonConverter<IBatchResultSetInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string IsCompletedKeyName = "IsCompleted";
        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

		public IBatchResultSetInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var batchResultSetInfo = DomainFactory.Create<IBatchResultSetInfo>();
            batchResultSetInfo.Guid = (Guid)jObject[GuidKeyName];
            batchResultSetInfo.Name = (string)jObject[NameKeyName];
            batchResultSetInfo.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            batchResultSetInfo.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
            batchResultSetInfo.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            batchResultSetInfo.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            batchResultSetInfo.IsCompleted = (bool)jObject[IsCompletedKeyName];

			return batchResultSetInfo;
        }

        public JObject ToJson(IBatchResultSetInfo instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {IsCompletedKeyName, new JValue(instance.IsCompleted)},
                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
			};
        }
    }
}
