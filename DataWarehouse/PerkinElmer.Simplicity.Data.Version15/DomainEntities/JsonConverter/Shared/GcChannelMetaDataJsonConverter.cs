using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class GcChannelMetaDataJsonConverter : IJsonConverter<IGCChannelMetaData>
	{
		private const int CurrentVersion = 2;
		private const string VersionKeyName = "Version";
		private const string ResponseUnitKeyName = "ResponseUnit";
		private const string DetectorTypeKeyName = "DetectorType";

        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";

        public JObject ToJson(IGCChannelMetaData instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{ResponseUnitKeyName, new JValue(instance.ResponseUnit)}, 
				{DetectorTypeKeyName, new JValue(instance.DetectorType)},
                {DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
                {DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
                {MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
                {MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
                {SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)}
            };
		}

		public IGCChannelMetaData FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IGCChannelMetaData>();
			instance.ResponseUnit = (string) jObject[ResponseUnitKeyName];
			instance.DetectorType = (string) jObject[DetectorTypeKeyName];
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