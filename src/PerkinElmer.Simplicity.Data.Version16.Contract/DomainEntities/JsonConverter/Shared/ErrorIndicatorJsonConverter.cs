using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
    public class ErrorIndicatorJsonConverter : IJsonConverter<IErrorIndicator>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ErrorIndicatorDetailsKeyName = "ErrorIndicatorDetails";

        public JObject ToJson(IErrorIndicator instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ErrorIndicatorDetailsKeyName, JsonConverterRegistry.GetConverter<IErrorIndicatorDetails>().ToJson(instance.ErrorIndicatorDetails)},
            };
        }

        public IErrorIndicator FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IErrorIndicator>();
            // ErrorIndicatorDetails is readonly property, so not deserialize it.
            //instance.ErrorIndicatorDetails = JsonConverterRegistry.GetConverter<IErrorIndicatorDetails>().FromJson((JObject)jObject[ErrorIndicatorDetailsKeyName]);
            return instance;
        }
    }
}
