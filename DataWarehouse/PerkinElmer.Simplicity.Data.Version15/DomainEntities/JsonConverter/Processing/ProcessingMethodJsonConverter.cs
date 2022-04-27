using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
	internal class ProcessingMethodJsonConverter : IJsonConverter<IProcessingMethod>
    {
        private const int CurrentVersion = 2;
        private const int Version1 = 1;
        private const bool Enabled = false;
        private const PharmacopeiaType SelectedPharmacopeiaType = PharmacopeiaType.Usp32;
        private const bool IsEfficiencyInPlates = true;
        private const double ColumnLength = 0.250;
        private const bool SignalToNoiseWindowEnabled = false;
        private const double SignalToNoiseWindowStart = 0.000;
        private const double SignalToNoiseWindowEnd = 60.000;
        private const bool AnalyzeAdjacentPeaks = true;
        private const string VersionKeyName = "Version";
        private const string InfoKeyName = "Info";
        private const string ForExternalMethodKeyName = "ForExternalMethod";
        private const string ProcessingDeviceMethodsKeyName = "ProcessingDeviceMethods";
        private const string ChannelMethodsKeyName = "ChannelMethods";
        private const string CalibrationGlobalParametersKeyName = "CalibrationGlobalParameters";
        private const string CompoundsKeyName = "Compounds";
        private const string SpectrumMethodsKeyName = "SpectrumMethods";
        private const string CompoundCalibrationResultsMapKeyName = "CompoundCalibrationResultsMap";
        private const string ApexOptimizedParametersKeyName = "ApexOptimizedParameters";
        private const string SuitabilityParametersKeyName = "SSTParameters";

        public IProcessingMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var processingMethod = DomainFactory.Create<IProcessingMethod>();
            processingMethod.Info = jObject[InfoKeyName].Type == JTokenType.Null?
                null : JsonConverterRegistry.GetConverter<IProcessingMethodInfo>().FromJson((JObject)jObject[InfoKeyName]);
			processingMethod.ForExternalMethod = (bool)jObject[ForExternalMethodKeyName];
			processingMethod.ProcessingDeviceMethods = JsonConverterHelper.GetListPropertyFromJson<IProcessingDeviceMethod>(jObject, ProcessingDeviceMethodsKeyName);
			processingMethod.ChannelMethods = JsonConverterHelper.GetListPropertyFromJson<IChannelMethod>(jObject, ChannelMethodsKeyName);
			processingMethod.Compounds = JsonConverterHelper.GetListPropertyFromJson<ICompound>(jObject, CompoundsKeyName);
            processingMethod.SpectrumMethods = JsonConverterHelper.GetListPropertyFromJson<ISpectrumMethod>(jObject, SpectrumMethodsKeyName);
			processingMethod.CalibrationGlobalParameters = jObject[CalibrationGlobalParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<ICalibrationGlobalParameters>().FromJson((JObject)jObject[CalibrationGlobalParametersKeyName]);
            if (version == Version1)
            {
                processingMethod.SuitabilityMethod = DomainFactory.Create<ISuitabilityMethod>();
                processingMethod.SuitabilityMethod.Enabled = Enabled;
                processingMethod.SuitabilityMethod.SelectedPharmacopeiaType = SelectedPharmacopeiaType;
                processingMethod.SuitabilityMethod.IsEfficiencyInPlates = IsEfficiencyInPlates;
                processingMethod.SuitabilityMethod.ColumnLength = ColumnLength;
                processingMethod.SuitabilityMethod.SignalToNoiseWindowEnabled = SignalToNoiseWindowEnabled;
                processingMethod.SuitabilityMethod.SignalToNoiseWindowStart = SignalToNoiseWindowStart;
                processingMethod.SuitabilityMethod.SignalToNoiseWindowEnd = SignalToNoiseWindowEnd; // in seconds
                processingMethod.SuitabilityMethod.AnalyzeAdjacentPeaks = AnalyzeAdjacentPeaks;
                processingMethod.SuitabilityMethod.CompoundPharmacopeiaDefinitions = null;
            }
            else
            {
                processingMethod.SuitabilityMethod = jObject[SuitabilityParametersKeyName].Type == JTokenType.Null ?
                    null : JsonConverterRegistry.GetConverter<ISuitabilityMethod>().FromJson((JObject)jObject[SuitabilityParametersKeyName]);
            }

            processingMethod.ApexOptimizedParameters = jObject[ApexOptimizedParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaApexOptimizedParameters>().FromJson((JObject)jObject[ApexOptimizedParametersKeyName]);
            processingMethod.CompoundCalibrationResultsMap = JsonConverterHelper.GetDictionaryPropertyFromJson<Guid, ICompoundCalibrationResults>(jObject, CompoundCalibrationResultsMapKeyName,
				s => new Guid(s));

			return processingMethod;
        }

        public JObject ToJson(IProcessingMethod instance)
        {
            if (instance == null) return null;
	        var jObject = new JObject
	        {
		        {VersionKeyName, new JValue(CurrentVersion)},
		        {InfoKeyName, JsonConverterRegistry.GetConverter<IProcessingMethodInfo>().ToJson(instance.Info)},
		        {ForExternalMethodKeyName, new JValue(instance.ForExternalMethod)},
		        {CalibrationGlobalParametersKeyName, JsonConverterRegistry.GetConverter<ICalibrationGlobalParameters>().ToJson(instance.CalibrationGlobalParameters)},
                {ApexOptimizedParametersKeyName, JsonConverterRegistry.GetConverter<IPdaApexOptimizedParameters>().ToJson(instance.ApexOptimizedParameters) },
                {SuitabilityParametersKeyName, JsonConverterRegistry.GetConverter<ISuitabilityMethod>().ToJson(instance.SuitabilityMethod) }
            };
	        JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ProcessingDeviceMethods, ProcessingDeviceMethodsKeyName);
	        JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ChannelMethods, ChannelMethodsKeyName);
	        JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.Compounds, CompoundsKeyName);
	        JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.SpectrumMethods, SpectrumMethodsKeyName);

			if (instance.CompoundCalibrationResultsMap == null)
			{
				jObject.Add(CompoundCalibrationResultsMapKeyName, new JValue(instance.CompoundCalibrationResultsMap));
			}
			else
			{
				jObject.Add(CompoundCalibrationResultsMapKeyName, JObject.FromObject(instance.CompoundCalibrationResultsMap));
			}

			return jObject;
        }
    }
}
