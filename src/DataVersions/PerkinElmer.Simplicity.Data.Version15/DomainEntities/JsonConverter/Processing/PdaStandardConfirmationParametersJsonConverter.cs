using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class PdaStandardConfirmationParametersJsonConverter : IJsonConverter<IPdaStandardConfirmationParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MinWavelengthKeyName = "MinWavelength";
        private const string MaxWavelengthKeyName = "MaxWavelength";
        private const string MinimumDataPointsKeyName = "MinimumDataPoints";
        private const string PassThresholdKeyName = "PassThreshold";
        private const string ApplyBaselineCorrectionKeyName = "ApplyBaselineCorrection";
        private const string UseAutoAbsorbanceThresholdForSampleKeyName = "UseAutoAbsorbanceThresholdForSample";
        private const string ManualAbsorbanceThresholdForSampleKeyName = "ManualAbsorbanceThresholdForSample";
        private const string UseAutoAbsorbanceThresholdForStandardKeyName = "UseAutoAbsorbanceThresholdForStandard";
        private const string ManualAbsorbanceThresholdForStandardKeyName = "ManualAbsorbanceThresholdForStandard";
        private const string StandardTypeKeyName = "StandardType";

        public JObject ToJson(IPdaStandardConfirmationParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinWavelengthKeyName, new JValue(instance.MinWavelength)},
                {MaxWavelengthKeyName, new JValue(instance.MaxWavelength)},
                {MinimumDataPointsKeyName, new JValue(instance.MinimumDataPoints)},
                {PassThresholdKeyName, new JValue(instance.PassThreshold)},
                {ApplyBaselineCorrectionKeyName, new JValue(instance.ApplyBaselineCorrection)},
                {UseAutoAbsorbanceThresholdForSampleKeyName, new JValue(instance.UseAutoAbsorbanceThresholdForSample)},
                {ManualAbsorbanceThresholdForSampleKeyName, new JValue(instance.ManualAbsorbanceThresholdForSample)},
                {UseAutoAbsorbanceThresholdForStandardKeyName, new JValue(instance.UseAutoAbsorbanceThresholdForStandard)},
                {ManualAbsorbanceThresholdForStandardKeyName, new JValue(instance.ManualAbsorbanceThresholdForStandard)},
                {StandardTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.StandardType, new StringEnumConverter()))}
            };
        }

        public IPdaStandardConfirmationParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaStandardConfirmationParameters = DomainFactory.Create<IPdaStandardConfirmationParameters>();

            pdaStandardConfirmationParameters.MinWavelength = (double)jObject[MinWavelengthKeyName];
            pdaStandardConfirmationParameters.MaxWavelength = (double)jObject[MaxWavelengthKeyName];
            pdaStandardConfirmationParameters.MinimumDataPoints = (int)jObject[MinimumDataPointsKeyName];
            pdaStandardConfirmationParameters.PassThreshold = (double)jObject[PassThresholdKeyName];
            pdaStandardConfirmationParameters.ApplyBaselineCorrection = (bool)jObject[ApplyBaselineCorrectionKeyName];
            pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForSample = (bool)jObject[UseAutoAbsorbanceThresholdForSampleKeyName];
            pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForSample = (double)jObject[ManualAbsorbanceThresholdForSampleKeyName];
            pdaStandardConfirmationParameters.UseAutoAbsorbanceThresholdForStandard = (bool)jObject[UseAutoAbsorbanceThresholdForStandardKeyName];
            pdaStandardConfirmationParameters.ManualAbsorbanceThresholdForStandard = (double)jObject[ManualAbsorbanceThresholdForStandardKeyName];
            pdaStandardConfirmationParameters.StandardType = jObject.ContainsKey(StandardTypeKeyName) ? 
                JsonConvert.DeserializeObject<StandardType>((string)jObject[StandardTypeKeyName]) : StandardType.None;
            return pdaStandardConfirmationParameters;
        }
    }
}
