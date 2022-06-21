using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class ModifiableProcessingMethodJsonConverter : IJsonConverter<IModifiableProcessingMethod>
    {
        private const int CurrentVersion = 3;
        private const int Version1 = 1;
        private const int Version2 = 2;
        private const bool Enabled = false;
        private const PharmacopeiaType SelectedPharmacopeiaType = PharmacopeiaType.Usp32;
        private const bool IsEfficiencyInPlates = true;
        private const double ColumnLength = 0.250;
        private const bool SignalToNoiseWindowEnabled = false;
        private const bool PerformAdditionalSearchForNoiseWindow = true;
        private const double SignalToNoiseWindowStart = 0.000;
        private const double SignalToNoiseWindowEnd = 60.000;
        private const bool AnalyzeAdjacentPeaks = true;
        private const string VersionKeyName = "Version";
        private const string ModifiedFromOriginalKeyName = "ModifiedFromOriginal";
        private const string OriginalReadOnlyMethodGuidKeyName = "OriginalReadOnlyMethodGuid";
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


        public JObject ToJson(IModifiableProcessingMethod instance)
        {
            if (instance == null) return null;
            var jObject =  new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ModifiedFromOriginalKeyName, new JValue(instance.ModifiedFromOriginal)},
                {OriginalReadOnlyMethodGuidKeyName, new JValue(instance.OriginalReadOnlyMethodGuid)},
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

        public IModifiableProcessingMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var modifiableProcessingMethod = DomainFactory.Create<IModifiableProcessingMethod>();
            modifiableProcessingMethod.ModifiedFromOriginal = (bool)jObject[ModifiedFromOriginalKeyName];
            modifiableProcessingMethod.OriginalReadOnlyMethodGuid = (Guid)jObject[OriginalReadOnlyMethodGuidKeyName];
            modifiableProcessingMethod.Info = jObject[InfoKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IProcessingMethodInfo>().FromJson((JObject)jObject[InfoKeyName]);
            modifiableProcessingMethod.ForExternalMethod = (bool)jObject[ForExternalMethodKeyName];
            modifiableProcessingMethod.ProcessingDeviceMethods = JsonConverterHelper.GetListPropertyFromJson<IProcessingDeviceMethod>(jObject, ProcessingDeviceMethodsKeyName);
            modifiableProcessingMethod.ChannelMethods = JsonConverterHelper.GetListPropertyFromJson<IChannelMethod>(jObject, ChannelMethodsKeyName);
            modifiableProcessingMethod.Compounds = JsonConverterHelper.GetListPropertyFromJson<ICompound>(jObject, CompoundsKeyName);
            modifiableProcessingMethod.SpectrumMethods = JsonConverterHelper.GetListPropertyFromJson<ISpectrumMethod>(jObject, SpectrumMethodsKeyName);
            modifiableProcessingMethod.CalibrationGlobalParameters = jObject[CalibrationGlobalParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<ICalibrationGlobalParameters>().FromJson((JObject)jObject[CalibrationGlobalParametersKeyName]);
            if (version > Version1)
            {
                modifiableProcessingMethod.SuitabilityMethod = DomainFactory.Create<ISuitabilityMethod>();
                modifiableProcessingMethod.SuitabilityMethod.Enabled = Enabled;
                modifiableProcessingMethod.SuitabilityMethod.SelectedPharmacopeiaType = SelectedPharmacopeiaType;
                modifiableProcessingMethod.SuitabilityMethod.IsEfficiencyInPlates = IsEfficiencyInPlates;
                modifiableProcessingMethod.SuitabilityMethod.ColumnLength = ColumnLength;
                modifiableProcessingMethod.SuitabilityMethod.SignalToNoiseWindowEnabled = SignalToNoiseWindowEnabled;
                modifiableProcessingMethod.SuitabilityMethod.SignalToNoiseWindowStart = SignalToNoiseWindowStart;
                modifiableProcessingMethod.SuitabilityMethod.SignalToNoiseWindowEnd = SignalToNoiseWindowEnd; // in seconds
                modifiableProcessingMethod.SuitabilityMethod.AnalyzeAdjacentPeaks = AnalyzeAdjacentPeaks;
            }
            if (version > Version2) 
            {
                modifiableProcessingMethod.SuitabilityMethod.PerformAdditionalSearchForNoiseWindow = PerformAdditionalSearchForNoiseWindow;
            }

            if (Version2 > Version1)
            {
                modifiableProcessingMethod.SuitabilityMethod = jObject[SuitabilityParametersKeyName].Type == JTokenType.Null ?
                    null : JsonConverterRegistry.GetConverter<ISuitabilityMethod>().FromJson((JObject)jObject[SuitabilityParametersKeyName]);
            }

            modifiableProcessingMethod.ApexOptimizedParameters = jObject[ApexOptimizedParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaApexOptimizedParameters>().FromJson((JObject)jObject[ApexOptimizedParametersKeyName]);
            modifiableProcessingMethod.CompoundCalibrationResultsMap = JsonConverterHelper.GetDictionaryPropertyFromJson<Guid, ICompoundCalibrationResults>(jObject, CompoundCalibrationResultsMapKeyName,
                s => new Guid(s));
            return modifiableProcessingMethod;
        }
    }
}
