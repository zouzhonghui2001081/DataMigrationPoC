using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class SuitabilityMethodParametersJsonConverter : IJsonConverter<ISuitabilityMethod>
    {
        private const int CurrentVersion = 3;
        private const int Version1 = 1;
        private const int Version2 = 2;
        private const string EnabledKeyName = "Enabled";
        private const string VersionKeyName = "Version";
        private const string SelectedPharmacopeiaTypeKeyName = "SelectedPharmacopeiaType";
        private const string IsEfficiencyInPlatesKeyName = "IsEfficiencyInPlates";
        private const string ColumnLengthKeyName = "ColumnLength";
        private const string SignalToNoiseRatioKeyName = "SignalToNoiseRatio";
        private const string SignalToNoiseWindowStartKeyName = "SignalToNoiseWindowStart";
        private const string SignalToNoiseWindowEndKeyName = "SignalToNoiseWindowEnd";
        private const string AnalyzeAdjacentPeaksKeyName = "AnalyzeAdjacentPeaks";
        private const string VoidTimeTypeKeyName = "VoidTimeType";
        private const string VoidTimeCustomValueInSecondsKeyName = "VoidTimeCustomValueInSeconds";
        private const string CompoundPharmacopeiaDefinitionsKeyName = "CompoundPharmacopeiaDefinitions";
        private const string PerformAdditionalSearchForNoiseWindowKeyName = "PerformAdditionalSearchForNoiseWindow";
        

        public JObject ToJson(ISuitabilityMethod instance)
        {
            if (instance == null) return null;
          
           var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {EnabledKeyName, new JValue(instance.Enabled)},
                {SelectedPharmacopeiaTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.SelectedPharmacopeiaType, new StringEnumConverter()))},
                {IsEfficiencyInPlatesKeyName, new JValue(instance.IsEfficiencyInPlates)},
                {ColumnLengthKeyName, new JValue(instance.ColumnLength)},
                {SignalToNoiseRatioKeyName, new JValue(instance.SignalToNoiseWindowEnabled)},
                {SignalToNoiseWindowStartKeyName, new JValue(instance.SignalToNoiseWindowStart)},
                {SignalToNoiseWindowEndKeyName, new JValue(instance.SignalToNoiseWindowEnd) },
                {AnalyzeAdjacentPeaksKeyName, new JValue(instance.AnalyzeAdjacentPeaks) },
                {CompoundPharmacopeiaDefinitionsKeyName, JsonConverterRegistry.GetConverter<IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>>().ToJson(instance.CompoundPharmacopeiaDefinitions) }
            };

           if (Version2 > Version1)
           {
               jObject.Add(VoidTimeTypeKeyName, new JValue(instance.VoidTimeType));
               jObject.Add(VoidTimeCustomValueInSecondsKeyName, new JValue(instance.VoidTimeCustomValueInSeconds));
           }


           if (CurrentVersion > Version2)
           {
              jObject.Add(PerformAdditionalSearchForNoiseWindowKeyName, new JValue(instance.PerformAdditionalSearchForNoiseWindow));
           }

            return jObject;
        }

        public ISuitabilityMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var sstSuitabilityMethodParameters = DomainFactory.Create<ISuitabilityMethod>();
            sstSuitabilityMethodParameters.Enabled = (bool)jObject[EnabledKeyName];
            sstSuitabilityMethodParameters.SelectedPharmacopeiaType =
                JsonConvert.DeserializeObject<PharmacopeiaType>(
                    (string)jObject[SelectedPharmacopeiaTypeKeyName]);
            sstSuitabilityMethodParameters.IsEfficiencyInPlates = (bool)jObject[IsEfficiencyInPlatesKeyName];

            sstSuitabilityMethodParameters.ColumnLength =
                (double)jObject[ColumnLengthKeyName];
            sstSuitabilityMethodParameters.SignalToNoiseWindowEnabled =
                (bool)jObject[SignalToNoiseRatioKeyName];
            sstSuitabilityMethodParameters.SignalToNoiseWindowStart =
                (double)jObject[SignalToNoiseWindowStartKeyName];
            sstSuitabilityMethodParameters.SignalToNoiseWindowEnd =
                (double)jObject[SignalToNoiseWindowEndKeyName];
            sstSuitabilityMethodParameters.AnalyzeAdjacentPeaks = (bool)jObject[AnalyzeAdjacentPeaksKeyName];
            sstSuitabilityMethodParameters.CompoundPharmacopeiaDefinitions = jObject[CompoundPharmacopeiaDefinitionsKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>>().FromJson((JObject)jObject[CompoundPharmacopeiaDefinitionsKeyName]);

            if (Version2 > Version1)
            {
              //  VoidTimeType voidTimeType = ConvertVoidTimeTypeStringToEnum((string)jObject[VoidTimeTypeKeyName]);
                VoidTimeType voidTimeType = JsonConvert.DeserializeObject<VoidTimeType>((string)jObject[VoidTimeTypeKeyName]);
                sstSuitabilityMethodParameters.VoidTimeType = voidTimeType;
                sstSuitabilityMethodParameters.VoidTimeCustomValueInSeconds = (double)jObject[VoidTimeCustomValueInSecondsKeyName];
            }

            if (version > Version2)
            {
                sstSuitabilityMethodParameters.PerformAdditionalSearchForNoiseWindow = (bool)jObject[PerformAdditionalSearchForNoiseWindowKeyName];
            }

            return sstSuitabilityMethodParameters;
        }

        private VoidTimeType ConvertVoidTimeTypeStringToEnum(string v1ExtractionTypeString)
        {
            var cleanedV1ExtractionTypeString = v1ExtractionTypeString.Replace("\"", "");
            switch (cleanedV1ExtractionTypeString)
            {
                case "None":
                    return VoidTimeType.None;
                case "UseFirstPeak":
                    return VoidTimeType.UseFirstPeak;
                case "UseValue":
                    return VoidTimeType.UseValue;
              
                default:
                    throw new ArgumentException("Unknown value for VoidTimeType");
            }
        }

    }
}
