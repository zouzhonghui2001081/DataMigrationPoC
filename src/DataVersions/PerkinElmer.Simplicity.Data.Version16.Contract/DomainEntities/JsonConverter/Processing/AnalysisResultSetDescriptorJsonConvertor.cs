using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class AnalysisResultSetDescriptorJsonConvertor: IJsonConverter<IAnalysisResultSetDescriptor>
    {
        const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string NameKeyName = "Name";
        private const string GuidKeyName = "Guid";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public JObject ToJson(IAnalysisResultSetDescriptor instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {NameKeyName, new JValue(instance.Name)},
                {GuidKeyName, new JValue(instance.Guid.ToString())},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)}
            };
        }

        public IAnalysisResultSetDescriptor FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var analysisResultSetDescriptor = DomainFactory.Create<IAnalysisResultSetDescriptor>();
            analysisResultSetDescriptor.Name = (string)jObject[NameKeyName];
            analysisResultSetDescriptor.Guid = (Guid)jObject[GuidKeyName];
            analysisResultSetDescriptor.CreatedDateUtc= (DateTime)jObject[CreatedDateUtcKeyName];
            analysisResultSetDescriptor.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            analysisResultSetDescriptor.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            analysisResultSetDescriptor.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            return analysisResultSetDescriptor;
        }
    }
}
