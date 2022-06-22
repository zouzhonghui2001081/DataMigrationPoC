using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class PdaApexOptimizedParametersJsonConverter : IJsonConverter<IPdaApexOptimizedParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MinWavelengthKeyName = "MinWavelength";
        private const string MaxWavelengthKeyName = "MaxWavelength";
        private const string WavelengthBandwidthKeyName = "WavelengthBandwidth";
        private const string UseReferenceKeyName = "UseReference";
        private const string ReferenceWavelengthKeyName = "ReferenceWavelength";
        private const string ReferenceWavelengthBandwidthKeyName = "ReferenceWavelengthBandwidth";
        private const string ApplyBaselineCorrectionKeyName = "ApplyBaselineCorrection";
        private const string UseAutoAbsorbanceThresholdKeyName = "UseAutoAbsorbanceThreshold";
        private const string ManualAbsorbanceKeyName = "ManualAbsorbanceThreshold";


        public JObject ToJson(IPdaApexOptimizedParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinWavelengthKeyName, new JValue(instance.MinWavelength)},
                {MaxWavelengthKeyName, new JValue(instance.MaxWavelength)},
                {WavelengthBandwidthKeyName, new JValue(instance.WavelengthBandwidth)},
                {UseReferenceKeyName, new JValue(instance.UseReference)},
                {ReferenceWavelengthKeyName, new JValue(instance.ReferenceWavelength)},
                {ReferenceWavelengthBandwidthKeyName, new JValue(instance.ReferenceWavelengthBandwidth)},
                {ApplyBaselineCorrectionKeyName, new JValue(instance.ApplyBaselineCorrection)},
                {UseAutoAbsorbanceThresholdKeyName, new JValue(instance.UseAutoAbsorbanceThreshold)},
                {ManualAbsorbanceKeyName, new JValue(instance.ManualAbsorbanceThreshold)},
            };
        }

        public IPdaApexOptimizedParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaApexOptimizedParameters = DomainFactory.Create<IPdaApexOptimizedParameters>();

            pdaApexOptimizedParameters.MinWavelength = (double)jObject[MinWavelengthKeyName];
            pdaApexOptimizedParameters.MaxWavelength = (double)jObject[MaxWavelengthKeyName];
            pdaApexOptimizedParameters.WavelengthBandwidth = (double)jObject[WavelengthBandwidthKeyName];
            pdaApexOptimizedParameters.UseReference = (bool)jObject[UseReferenceKeyName];
            pdaApexOptimizedParameters.ReferenceWavelength = (double)jObject[ReferenceWavelengthKeyName];
            pdaApexOptimizedParameters.ReferenceWavelengthBandwidth = (double)jObject[ReferenceWavelengthBandwidthKeyName];
            pdaApexOptimizedParameters.ApplyBaselineCorrection = (bool)jObject[ApplyBaselineCorrectionKeyName];
            pdaApexOptimizedParameters.UseAutoAbsorbanceThreshold = (bool)jObject[UseAutoAbsorbanceThresholdKeyName];
            pdaApexOptimizedParameters.ManualAbsorbanceThreshold = (double)jObject[ManualAbsorbanceKeyName];
            return pdaApexOptimizedParameters;
        }
    }
}
