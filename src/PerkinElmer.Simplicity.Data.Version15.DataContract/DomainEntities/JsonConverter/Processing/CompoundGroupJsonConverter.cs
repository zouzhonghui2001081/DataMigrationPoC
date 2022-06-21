using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CompoundGroupJsonConverter : IJsonConverter<ICompoundGroup>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string StartTimeKeyName = "StartTime";
        private const string EndTimeKeyName = "EndTime";
        private const string CompoundGuidsKeyName = "CompoundGuids";
        private const string CompoundTypeKeyName = "CompoundType";
        private const string IdentificationParametersKeyName = "IdentificationParameters";
        private const string CalibrationParametersKeyName = "CalibrationParameters";
        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string ChannelIndexKeyName = "ChannelIndex";
        private const string ProcessingMethodChannelGuidKeyName = "ProcessingMethodChannelGuid";

        public JObject ToJson(ICompoundGroup instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {StartTimeKeyName, new JValue(instance.StartTime)},
                {EndTimeKeyName, new JValue(instance.EndTime)},
                {CompoundGuidsKeyName, instance.CompoundGuids == null ? null : JArray.FromObject(instance.CompoundGuids) },
                {CompoundTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.CompoundType, new StringEnumConverter()))},
                {IdentificationParametersKeyName, JsonConverterRegistry.GetConverter<IIdentificationParameters>().ToJson(instance.IdentificationParameters)},
                {CalibrationParametersKeyName, JsonConverterRegistry.GetConverter<ICalibrationParameters>().ToJson(instance.CalibrationParameters)},
                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {ChannelIndexKeyName, new JValue(instance.ChannelIndex)},
                {ProcessingMethodChannelGuidKeyName, new JValue(instance.ProcessingMethodChannelGuid)},
            };
        }

        public ICompoundGroup FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundGroup = DomainFactory.Create<ICompoundGroup>();
            compoundGroup.StartTime = (double)jObject[StartTimeKeyName];
            compoundGroup.EndTime = (double)jObject[EndTimeKeyName];
            compoundGroup.CompoundGuids = JsonConverterHelper.GetListPropertyFromJson<Guid>(jObject, CompoundGuidsKeyName);
            compoundGroup.CompoundType = JsonConvert.DeserializeObject<CompoundType>((string)jObject[CompoundTypeKeyName]); 
            compoundGroup.IdentificationParameters = jObject[IdentificationParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IIdentificationParameters>().FromJson((JObject)jObject[IdentificationParametersKeyName]);
            compoundGroup.CalibrationParameters = jObject[CalibrationParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<ICalibrationParameters>().FromJson((JObject)jObject[CalibrationParametersKeyName]);
            compoundGroup.Guid = (Guid)jObject[GuidKeyName];
            compoundGroup.Name = (string)jObject[NameKeyName];
            compoundGroup.ChannelIndex = (int)jObject[ChannelIndexKeyName];
            compoundGroup.ProcessingMethodChannelGuid = (Guid)jObject[ProcessingMethodChannelGuidKeyName];
            return compoundGroup;
        }
    }
}
