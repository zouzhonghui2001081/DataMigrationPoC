using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.CompoundLibrary;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing.CompoundLibrary
{
    internal class CompoundLibraryItemContentJsonConverter : IJsonConverter<ICompoundLibraryItemContent>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string RetentionTimeKeyName = "RetentionTime";
        private const string SpectrumAbsorbancesKeyName = "SpectrumAbsorbances";
        private const string BaselineAbsorbancesKeyName = "BaselineAbsorbances";
        private const string StartWavelengthKeyName = "StartWavelength";
        private const string EndWavelengthKeyName = "EndWavelength";
        private const string StepKeyName = "Step";

        public JObject ToJson(ICompoundLibraryItemContent instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {RetentionTimeKeyName, new JValue(instance.RetentionTime)},
                {StartWavelengthKeyName, new JValue(instance.StartWavelength)},
                {EndWavelengthKeyName, new JValue(instance.EndWavelength)},
                {StepKeyName, new JValue(instance.Step)},
                {SpectrumAbsorbancesKeyName,  instance.SpectrumAbsorbances == null ? 
                    null : JArray.FromObject(instance.SpectrumAbsorbances)},
                {BaselineAbsorbancesKeyName, instance.BaselineAbsorbances == null ? 
                    null : JArray.FromObject(instance.BaselineAbsorbances) }
            };
        }

        public ICompoundLibraryItemContent FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var compoundLibraryItemContent = DomainFactory.Create<ICompoundLibraryItemContent>();
            compoundLibraryItemContent.RetentionTime = (double)jObject[RetentionTimeKeyName];
            compoundLibraryItemContent.StartWavelength = (double)jObject[StartWavelengthKeyName];
            compoundLibraryItemContent.EndWavelength = (double)jObject[EndWavelengthKeyName];
            compoundLibraryItemContent.Step = (double)jObject[StepKeyName];
            compoundLibraryItemContent.SpectrumAbsorbances =
                JsonConverterHelper.GetListPropertyFromJson<double>(jObject, SpectrumAbsorbancesKeyName); 
            compoundLibraryItemContent.BaselineAbsorbances =
                JsonConverterHelper.GetListPropertyFromJson<double>(jObject, BaselineAbsorbancesKeyName);
            return compoundLibraryItemContent;
        }

        
    }
}
