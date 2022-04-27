using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
    public class FluorescenceSpectrumChannelMetaDataJsonConverter : IJsonConverter<IFluorescenceSpectrumChannelMetaData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ResponseUnitKeyName = "ResponseUnit";
        private const string NameKeyName = "Name";

        private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
        private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
        private const string MinValidYValueKeyName = "MinValidYValue";
        private const string MaxValidYValueKeyName = "MaxValidYValue";
        private const string EMWavelengthIntervalInNanometerKeyName = "EMWavelengthIntervalInNanometer";
        private const string EXWaveLengthInNanometerKeyName = "EXWaveLengthInNanometer";
        private const string EMWaveLengthStartInNanometerKeyName = "EMWaveLengthStartInNanometer";
        private const string EMWaveLengthEndInNanometerKeyName = "EMWaveLengthEndInNanometer";
        private const string SignalNoiseRatio = "SignalNoiseRation";
        private const string PeakIntensityOfRamanScattering = "PeakIntensityOfRamanScattering";
        private const string BaselineIntensityOfRamanScattering = "BaselineIntensityOfRamanScattering";
        private const string PeakWavelengthOfRamanScattering = "PeakWavelengthOfRamanScattering";
        private const string FlowCellType = "FlowCellType";
        private const string SignalToNoiseRatioSpecification = "SignalToNoiseRatioSpecification";

        public JObject ToJson(IFluorescenceSpectrumChannelMetaData instance)
        {

            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
                {NameKeyName, new JValue(instance.Name)},
                {DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
                {DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
                {MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
                {MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
                {EMWavelengthIntervalInNanometerKeyName, instance.EMWavelengthIntervalInNanometer},
                {EXWaveLengthInNanometerKeyName, instance.EXWaveLengthInNanometer},
                {EMWaveLengthStartInNanometerKeyName, instance.EMWaveLengthStartInNanometer},
                {EMWaveLengthEndInNanometerKeyName, instance.EMWaveLengthEndInNanometer},
                {SignalNoiseRatio, instance.SignalNoiseRatio},
                {PeakIntensityOfRamanScattering, instance.PeakIntensityOfRamanScattering},
                {BaselineIntensityOfRamanScattering, instance.BaselineIntensityOfRamanScattering},
                {PeakWavelengthOfRamanScattering, instance.PeakWavelengthOfRamanScattering},
                {FlowCellType, instance.FlowCellType},
                {SignalToNoiseRatioSpecification, instance.SignalToNoiseRatioSpecification}
            };
        }

        public IFluorescenceSpectrumChannelMetaData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IFluorescenceSpectrumChannelMetaData>();
            instance.ResponseUnit = (string)jObject[ResponseUnitKeyName];
            instance.Name = (string)jObject[NameKeyName];
            instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
            instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
            instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
            instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
            instance.EMWavelengthIntervalInNanometer = (int) jObject[EMWavelengthIntervalInNanometerKeyName];
            instance.EXWaveLengthInNanometer = (int) jObject[EXWaveLengthInNanometerKeyName];
            instance.EMWaveLengthStartInNanometer = (int) jObject[EMWaveLengthStartInNanometerKeyName];
            instance.EMWaveLengthEndInNanometer = (int) jObject[EMWaveLengthEndInNanometerKeyName];
            instance.PeakIntensityOfRamanScattering = (double) jObject[PeakIntensityOfRamanScattering];
            instance.SignalNoiseRatio = (double) jObject[SignalNoiseRatio];
            instance.BaselineIntensityOfRamanScattering = (double) jObject[BaselineIntensityOfRamanScattering];
            instance.PeakWavelengthOfRamanScattering = (int) jObject[PeakWavelengthOfRamanScattering];
            instance.FlowCellType = (string) jObject[FlowCellType];
            instance.SignalToNoiseRatioSpecification = (double) jObject[SignalToNoiseRatioSpecification];
            return instance;
        }
    }
}