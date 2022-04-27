using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class PdaAbsorbanceRatioParametersJsonConverter : IJsonConverter<IPdaAbsorbanceRatioParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string WavelengthAKeyName = "WavelengthA";
        private const string WavelengthBKeyName = "WavelengthB";
        private const string ApplyBaselineCorrectionKeyName = "ApplyBaselineCorrection";
        private const string UseAutoAbsorbanceThresholdKeyName = "UseAutoAbsorbanceThreshold";
        private const string ManualAbsorbanceThresholdKeyName = "ManualAbsorbanceThreshold";

        public JObject ToJson(IPdaAbsorbanceRatioParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {WavelengthAKeyName, new JValue(instance.WavelengthA)},
                {WavelengthBKeyName, new JValue(instance.WavelengthB)},
                {ApplyBaselineCorrectionKeyName, new JValue(instance.ApplyBaselineCorrection)},
                {UseAutoAbsorbanceThresholdKeyName, new JValue(instance.UseAutoAbsorbanceThreshold)},
                {ManualAbsorbanceThresholdKeyName, new JValue(instance.ManualAbsorbanceThreshold)},
            };
        }

        public IPdaAbsorbanceRatioParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaAbsorbanceRatioParameters = DomainFactory.Create<IPdaAbsorbanceRatioParameters>();

            pdaAbsorbanceRatioParameters.WavelengthA = (double)jObject[WavelengthAKeyName];
            pdaAbsorbanceRatioParameters.WavelengthB = (double)jObject[WavelengthBKeyName];
            pdaAbsorbanceRatioParameters.ApplyBaselineCorrection = (bool)jObject[ApplyBaselineCorrectionKeyName];
            pdaAbsorbanceRatioParameters.UseAutoAbsorbanceThreshold = (bool)jObject[UseAutoAbsorbanceThresholdKeyName];
            pdaAbsorbanceRatioParameters.ManualAbsorbanceThreshold = (double)jObject[ManualAbsorbanceThresholdKeyName];
            return pdaAbsorbanceRatioParameters;
        }
    }
}
