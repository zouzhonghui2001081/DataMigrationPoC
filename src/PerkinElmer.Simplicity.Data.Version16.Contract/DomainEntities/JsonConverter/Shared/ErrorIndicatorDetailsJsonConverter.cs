using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
    public class ErrorIndicatorDetailsJsonConverter : IJsonConverter<IErrorIndicatorDetails>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string IsInErrorKeyName = "IsInError";
        private const string ErrorDescriptionsKeyName = "ErrorDescriptions";

        public JObject ToJson(IErrorIndicatorDetails instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {IsInErrorKeyName, new JValue(instance.IsInError)},
                {ErrorDescriptionsKeyName, instance.ErrorDescriptions ==null?
                    null: JArray.FromObject(instance.ErrorDescriptions)},
            };
        }

        public IErrorIndicatorDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IErrorIndicatorDetails>();
            instance.IsInError = (bool)jObject[IsInErrorKeyName];
            instance.ErrorDescriptions =
                JsonConverterHelper.GetListPropertyFromJson<string>(jObject, ErrorDescriptionsKeyName);
            return instance;
        }
    }
}
