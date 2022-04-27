using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
	internal class PdaChannelMetaDataJsonConverter : IJsonConverter<IPdaChannelMetaData>
	{
		private const int CurrentVersion = 2;
		private const string VersionKeyName = "Version";
		private const string ResponseUnitKeyName = "ResponseUnit";
		private const string StartWavelengthKeyName = "StartWavelength";
		private const string EndWavelengthKeyName = "EndWavelength";
		private const string LowResolutionKeyName = "LowResolution";

        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";
        
        public JObject ToJson(IPdaChannelMetaData instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
				{StartWavelengthKeyName, new JValue(instance.StartWavelength)},
				{EndWavelengthKeyName, new JValue(instance.EndWavelength)},
				{LowResolutionKeyName, new JValue(instance.LowResolution)},
				{DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
				{DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
				{MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
				{MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
				{SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)}
			};
		}

		public IPdaChannelMetaData FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IPdaChannelMetaData>();
			instance.StartWavelength = (double) jObject[StartWavelengthKeyName];
			instance.EndWavelength = (double) jObject[EndWavelengthKeyName];
			instance.LowResolution = (bool) jObject[LowResolutionKeyName];
            instance.ResponseUnit = (string)jObject[ResponseUnitKeyName];
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
                instance.SamplingRateInMilliseconds  = 0;
            }
            return instance;
		}
	}
}