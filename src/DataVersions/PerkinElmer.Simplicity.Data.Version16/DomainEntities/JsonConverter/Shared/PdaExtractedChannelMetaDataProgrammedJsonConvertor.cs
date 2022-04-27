using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
	internal class PdaExtractedChannelMetaDataProgrammedJsonConvertor : IJsonConverter<IPdaExtractedChannelMetaDataProgrammed>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";

		private const string ResponseUnitKeyName = "ResponseUnit";
        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";

		private const string ExtractionSegmentsKeyName = "ExtractionSegments";

		public JObject ToJson(IPdaExtractedChannelMetaDataProgrammed instance)
		{
            var jObject = instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
                {DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
                {DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
                {MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
                {MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
                {SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)},
			};

			JsonConverterHelper.SetCollectionPropertyToJObject<IPdaExtractionSegment>(jObject, instance.ExtractionSegments, ExtractionSegmentsKeyName);
            return jObject;
		}

		public IPdaExtractedChannelMetaDataProgrammed FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int) jObject[VersionKeyName];
			if (version != CurrentVersion)
				throw new Exception("Unsupported serialized object version!");

			var instance = DomainFactory.Create<IPdaExtractedChannelMetaDataProgrammed>();
			instance.ResponseUnit = (string) jObject[ResponseUnitKeyName];
            instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
            instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
            instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
            instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
            instance.SamplingRateInMilliseconds = (double)jObject[SamplingRateInMilliseconds];

			instance.ExtractionSegments = JsonConverterHelper.GetListPropertyFromJson<IPdaExtractionSegment>(jObject, ExtractionSegmentsKeyName);

			return instance;
		}
	}
}