using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
    internal class AToDChannelMetaDataJSonConverter : IJsonConverter<IAToDChannelMetaData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ResponseUnitKeyName = "ResponseUnit";
		private const string DetectorTypeKeyName = "DetectorType";
        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";

		public JObject ToJson(IAToDChannelMetaData instance)
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

        public IAToDChannelMetaData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];

            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IAToDChannelMetaData>();
            instance.ResponseUnit = (string)jObject[ResponseUnitKeyName];
			instance.DetectorType = (string) jObject[DetectorTypeKeyName];
            instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
            instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
            instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
            instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
            instance.SamplingRateInMilliseconds = (double)jObject[SamplingRateInMilliseconds];
            return instance;
        }
    }
}
