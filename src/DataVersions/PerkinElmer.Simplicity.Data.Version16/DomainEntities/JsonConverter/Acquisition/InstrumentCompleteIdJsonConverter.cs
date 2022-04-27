using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
    public class InstrumentCompleteIdJsonConverter : IJsonConverter<InstrumentCompleteId>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InstrumentMasterIdKeyName = "InstrumentMasterId";
        private const string InstrumentIdKeyName = "InstrumentId";

        public JObject ToJson(InstrumentCompleteId instance)
        {
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InstrumentMasterIdKeyName, new JValue(instance.InstrumentMasterId.ToString())},
                {InstrumentIdKeyName, new JValue(instance.InstrumentId.ToString())},
            };
            return jObject;
        }

        public InstrumentCompleteId FromJson(JObject jObject)
        {
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            if (jObject[InstrumentMasterIdKeyName].Type != JTokenType.Null && jObject[InstrumentIdKeyName].Type != JTokenType.Null)
                return new InstrumentCompleteId(new Id((string)jObject[InstrumentMasterIdKeyName]),
                new Id((string)jObject[InstrumentIdKeyName]));
            return new InstrumentCompleteId();
        }
    }
}
