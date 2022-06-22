using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CompoundJsonConverter : IJsonConverter<ICompound>
    {
        private const int CurrentVersion = 2;
        private const string VersionKeyName = "Version";
        private const string CompoundTypeKeyName = "CompoundType";
        private const string IdentificationParametersKeyName = "IdentificationParameters";
        private const string CalibrationParametersKeyName = "CalibrationParameters";
        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string ChannelIndexKeyName = "ChannelIndex";
        private const string ProcessingMethodChannelGuidKeyName = "ProcessingMethodChannelGuid";
        private const string UsedForSuitabilityKeyName = "UsedForSuitability";
        public JObject ToJson(ICompound instance)
        {
            if (instance == null) return null;
            if (instance.CompoundType == CompoundType.SinglePeak)
            {
                return new JObject
                {
                    {VersionKeyName, new JValue(CurrentVersion)},
                    {CompoundTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.CompoundType, new StringEnumConverter()))},
                    {IdentificationParametersKeyName, JsonConverterRegistry.GetConverter<IIdentificationParameters>().ToJson(instance.IdentificationParameters)},
                    {CalibrationParametersKeyName, JsonConverterRegistry.GetConverter<ICalibrationParameters>().ToJson(instance.CalibrationParameters)},
                    {GuidKeyName, new JValue(instance.Guid)},
                    {NameKeyName, new JValue(instance.Name)},
                    {ChannelIndexKeyName, new JValue(instance.ChannelIndex)},
                    {ProcessingMethodChannelGuidKeyName, new JValue(instance.ProcessingMethodChannelGuid)},
                    {UsedForSuitabilityKeyName, new JValue(instance.UsedForSuitability)},
                };
            }

            return JsonConverterRegistry.GetConverter<ICompoundGroup>().ToJson(instance as ICompoundGroup);
        }

        public ICompound FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundType = JsonConvert.DeserializeObject<CompoundType>((string)jObject[CompoundTypeKeyName]);
            if (compoundType == CompoundType.SinglePeak)
            {
                var compound = DomainFactory.Create<ICompound>();
                compound.CompoundType = compoundType;
                compound.IdentificationParameters = jObject[IdentificationParametersKeyName].Type == JTokenType.Null ?
                    null : JsonConverterRegistry.GetConverter<IIdentificationParameters>().FromJson((JObject)jObject[IdentificationParametersKeyName]);
                compound.CalibrationParameters = jObject[CalibrationParametersKeyName].Type == JTokenType.Null ?
                    null : JsonConverterRegistry.GetConverter<ICalibrationParameters>().FromJson((JObject)jObject[CalibrationParametersKeyName]);
                compound.Guid = (Guid)jObject[GuidKeyName];
                compound.Name = (string)jObject[NameKeyName];
                compound.ChannelIndex = (int)jObject[ChannelIndexKeyName];
                compound.ProcessingMethodChannelGuid = (Guid)jObject[ProcessingMethodChannelGuidKeyName];
                if (version >= 2)
                {
                    compound.UsedForSuitability = (bool)jObject[UsedForSuitabilityKeyName];
                }
                else
                {
                    compound.UsedForSuitability = false;
                }
                return compound;
            }
            return JsonConverterRegistry.GetConverter<ICompoundGroup>().FromJson(jObject);
        }
    }
}
