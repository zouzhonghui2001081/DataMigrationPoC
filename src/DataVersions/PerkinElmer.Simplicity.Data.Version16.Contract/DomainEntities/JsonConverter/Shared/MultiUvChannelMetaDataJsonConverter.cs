using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
    internal class MultiUvChannelMetaDataJsonConverter : IJsonConverter<IMultiUVChannelMetaData>
    {
        private const int CurrentVersion = 2;
        private const int Version1 = 1;
        private const string VersionKeyName = "Version";
        private const string ResponseUnitKeyName = "ResponseUnit";
        private const string WavelengthInNanometersKeyName = "WavelengthInNanometers";
        private const string ProgrammedKeyName = "Programmed";

        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";
        public JObject ToJson(IMultiUVChannelMetaData instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
                {WavelengthInNanometersKeyName, new JValue(instance.WavelengthInNanometers)},
                {ProgrammedKeyName, new JValue(instance.Programmed)},
                {DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
                {DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
                {MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
                {MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
                {SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)},
            };
        }

        public IMultiUVChannelMetaData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IMultiUVChannelMetaData>();
            instance.ResponseUnit = (string) jObject[ResponseUnitKeyName];
            instance.WavelengthInNanometers = (double) jObject[WavelengthInNanometersKeyName];
            instance.Programmed = (bool) jObject[ProgrammedKeyName];
            instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
            instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
            instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
            instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
            if (version == Version1)
            {
                instance.SamplingRateInMilliseconds = 0;
                
            }
            else
            {
                instance.SamplingRateInMilliseconds = (double)jObject[SamplingRateInMilliseconds];
            }
            return instance;
        }
    }
}