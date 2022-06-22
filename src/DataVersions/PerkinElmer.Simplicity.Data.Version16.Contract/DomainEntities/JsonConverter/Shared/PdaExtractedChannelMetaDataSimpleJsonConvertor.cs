using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
	internal class PdaExtractedChannelMetaDataSimpleJsonConvertor : IJsonConverter<IPdaExtractedChannelMetaDataSimple>
	{
		private const int CurrentVersion = 2;
		private const string VersionKeyName = "Version";
		private const string ResponseUnitKeyName = "ResponseUnit";
		private const string WaveLengthKeyName = "Wavelength";
		private const string WavelengthBandwidthKeyName = "WavelengthBandwidth";
		private const string UseReferenceKeyName = "UseReference";
		private const string ReferenceWavelengthKeyName = "ReferenceWavelength";
		private const string ReferenceWavelengthBandwidthKeyName = "ReferenceWavelengthBandwidth";
		private const string IsApexOptimized = "IsApexOptimized";

        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";
        
        public JObject ToJson(IPdaExtractedChannelMetaDataSimple instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
				{WaveLengthKeyName, new JValue(instance.Wavelength)},
				{WavelengthBandwidthKeyName, new JValue(instance.WavelengthBandwidth)},
				{UseReferenceKeyName, new JValue(instance.UseReference)},
				{ReferenceWavelengthKeyName, new JValue(instance.ReferenceWavelength)},
				{ReferenceWavelengthBandwidthKeyName, new JValue(instance.ReferenceWavelengthBandwidth)},
				{IsApexOptimized, new JValue(instance.IsApexOptimized)},
                {DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
                {DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
                {MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
                {MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
                {SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)},
			};
		}

		public IPdaExtractedChannelMetaDataSimple FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var instance = DomainFactory.Create<IPdaExtractedChannelMetaDataSimple>();
			instance.ResponseUnit = (string) jObject[ResponseUnitKeyName];
			instance.Wavelength = (double) jObject[WaveLengthKeyName];
			instance.WavelengthBandwidth = (double) jObject[WavelengthBandwidthKeyName];
			instance.UseReference = (bool) jObject[UseReferenceKeyName];
			instance.ReferenceWavelength = (double)jObject[ReferenceWavelengthKeyName];
			instance.ReferenceWavelengthBandwidth = (double)jObject[ReferenceWavelengthBandwidthKeyName];
			instance.IsApexOptimized = (bool)jObject[IsApexOptimized];
            instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
            instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
            instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
            instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
            if (version >= 2)
            {
                instance.SamplingRateInMilliseconds = (double)jObject[SamplingRateInMilliseconds];
            }
            else
            {
                instance.SamplingRateInMilliseconds = 0;
            }
            return instance;
		}
	}
}