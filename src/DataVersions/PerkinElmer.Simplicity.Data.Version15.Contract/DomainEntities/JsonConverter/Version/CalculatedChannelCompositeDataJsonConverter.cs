using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class CalculatedChannelCompositeDataJsonConverter : IJsonConverter<CalculatedChannelCompositeData>
    {
        private const string CalculatedChannelDataKeyName = "CalculatedChannelData";
        private const string RunPeakResultsKeyName = "RunPeakResults";
        private const string SuitabilityResultsKeyName = "SuitabilityResults";

        public JObject ToJson(CalculatedChannelCompositeData instance)
        {
            if (instance == null) return null;

            var runPeakResultsDomain = new List<IRunPeakResult>();
            var suitabilityResultResultsDomain = new List<ISuitabilityResult>();
            if (instance.RunPeakResults != null)
            {
                foreach (var runPeakResult in instance.RunPeakResults)
                {
                    var runPeakResultDomain = DomainFactory.Create<IRunPeakResult>();
                    DomainContractAdaptor.PopulateRunPeakResult(runPeakResult, runPeakResultDomain);
                    runPeakResultsDomain.Add(runPeakResultDomain);
                }
                    
            }

            if (instance.SuitabilityResults != null)
            {
                foreach (var suitabilityResult in instance.SuitabilityResults)
                {
                    var suitabilityResultDomain = DomainFactory.Create<ISuitabilityResult>();
                    DomainContractAdaptor.PopulateSuitabilityResult(suitabilityResult, suitabilityResultDomain);
                    suitabilityResultResultsDomain.Add(suitabilityResultDomain);
                }
            }

            var jObject = new JObject
            {
                { CalculatedChannelDataKeyName, JsonConverterRegistry.GetConverter<CalculatedChannelData>().ToJson(instance.CalculatedChannelData)}
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<IRunPeakResult>(jObject, runPeakResultsDomain, RunPeakResultsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<ISuitabilityResult>(jObject, suitabilityResultResultsDomain, SuitabilityResultsKeyName);
            return jObject;
        }

        public CalculatedChannelCompositeData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var calculatedChannelCompositeData = new CalculatedChannelCompositeData
            {
                CalculatedChannelData = jObject[CalculatedChannelDataKeyName].Type == JTokenType.Null
                    ? null : JsonConverterRegistry.GetConverter<CalculatedChannelData>().FromJson((JObject) jObject[CalculatedChannelDataKeyName])
            };

            var runPeakResultsDomain = JsonConverterHelper.GetListPropertyFromJson<IRunPeakResult>(jObject, RunPeakResultsKeyName);
            var suitabilityResultsDomain = JsonConverterHelper.GetListPropertyFromJson<ISuitabilityResult>(jObject, RunPeakResultsKeyName);

            var runPeakResultsEntities = new List<RunPeakResult>();
            var suitabilityResultsEntities = new List<SuitabilityResult>();
            if (runPeakResultsDomain != null)
            {
                foreach (var compoundLibraryItemDomain in runPeakResultsDomain)
                {
                    var runPeakResultEntity = new RunPeakResult();
                    DomainContractAdaptor.PopulateRunPeakResultEntity(compoundLibraryItemDomain, runPeakResultEntity);
                    runPeakResultsEntities.Add(runPeakResultEntity);
                }
            }

            if (suitabilityResultsDomain != null)
            {
                foreach (var suitabilityResultDomain in suitabilityResultsDomain)
                {
                    var suitabilityResultEntity = new SuitabilityResult();
                    DomainContractAdaptor.PopulateSuitabilityResultEntity(suitabilityResultDomain, suitabilityResultEntity);
                    suitabilityResultsEntities.Add(suitabilityResultEntity);
                }
            }

            calculatedChannelCompositeData.RunPeakResults = runPeakResultsEntities;
            calculatedChannelCompositeData.SuitabilityResults = suitabilityResultsEntities;
            return calculatedChannelCompositeData;
        }
    }
}
