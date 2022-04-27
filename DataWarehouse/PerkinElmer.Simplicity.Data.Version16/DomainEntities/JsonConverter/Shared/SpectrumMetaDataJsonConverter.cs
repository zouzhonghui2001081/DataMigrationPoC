using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
    public class SpectrumMetaDataJsonConverter : IJsonConverter<ISpectrumMetaData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string TimeInSecondsKeyName = "TimeInSeconds";
        private const string UsePreviousXValuesKeyName = "UsePreviousXValues";
        private const string XValuesKeyName = "XValues";

        public JObject ToJson(ISpectrumMetaData instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {TimeInSecondsKeyName, new JValue(instance.TimeInSeconds)},
                {UsePreviousXValuesKeyName, new JValue(instance.UsePreviousXValues)},
                {XValuesKeyName, instance.XValues ==null?
                    null: JArray.FromObject(instance.XValues)},
            };
        }

        public ISpectrumMetaData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<ISpectrumMetaData>();
            instance.TimeInSeconds = (double)jObject[TimeInSecondsKeyName];
            instance.UsePreviousXValues = (bool)jObject[UsePreviousXValuesKeyName];
            instance.XValues =
                JsonConverterHelper.GetListPropertyFromJson<double>(jObject, XValuesKeyName);
            return instance;
        }
    }
}
