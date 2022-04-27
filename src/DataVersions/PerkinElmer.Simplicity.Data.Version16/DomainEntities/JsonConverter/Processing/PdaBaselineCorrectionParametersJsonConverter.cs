using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class PdaBaselineCorrectionParametersJsonConverter : IJsonConverter<IPdaBaselineCorrectionParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string CorrectionTypeKeyName = "CorrectionType";
        private const string SelectedSpectrumTimeKeyName = "SelectedSpectrumTime";
        private const string RangeStartKeyName = "RangeStart";
        private const string RangeEndKeyName = "RangeEnd";
        
        public JObject ToJson(IPdaBaselineCorrectionParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {CorrectionTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.CorrectionType, new StringEnumConverter()))},
                {SelectedSpectrumTimeKeyName, new JValue(instance.SelectedSpectrumTimeInSeconds)},
                {RangeStartKeyName, new JValue(instance.RangeStartInSeconds)},
                {RangeEndKeyName, new JValue(instance.RangeEndInSeconds)}
            };
        }

        public IPdaBaselineCorrectionParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaBaselineCorrectionParameters = DomainFactory.Create<IPdaBaselineCorrectionParameters>();

            pdaBaselineCorrectionParameters.CorrectionType = JsonConvert.DeserializeObject<BaselineCorrectionType>((string)jObject[CorrectionTypeKeyName]);
            pdaBaselineCorrectionParameters.SelectedSpectrumTimeInSeconds = (double?)jObject[SelectedSpectrumTimeKeyName];
            pdaBaselineCorrectionParameters.RangeStartInSeconds = (double?)jObject[RangeStartKeyName];
            pdaBaselineCorrectionParameters.RangeEndInSeconds = (double?)jObject[RangeEndKeyName];
            return pdaBaselineCorrectionParameters;
        }
    }
}
