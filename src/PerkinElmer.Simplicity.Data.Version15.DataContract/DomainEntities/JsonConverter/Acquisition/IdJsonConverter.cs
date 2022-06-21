using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
    public class IdJsonConverter : IJsonConverter<Id>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string IdKeyName = "Id";

        public JObject ToJson(Id instance)
        {
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {IdKeyName, new JValue(instance.ToString())},
            };
            return jObject;
        }

        public Id FromJson(JObject jObject)
        {
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            if (jObject[IdKeyName].Type != JTokenType.Null)
                return new Id((string)jObject[IdKeyName]);
            return new Id();
        }
    }
}
