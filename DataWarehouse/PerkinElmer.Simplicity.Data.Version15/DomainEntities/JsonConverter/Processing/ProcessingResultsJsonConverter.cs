using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class ProcessingResultsJsonConverter : IJsonConverter<IProcessingResults>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string RunResultsKeyName = "RunResults";  

        public JObject ToJson(IProcessingResults instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
            };

            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.RunResults, RunResultsKeyName);
            return jObject;
        }

        public IProcessingResults FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var processingResults = DomainFactory.Create<IProcessingResults>();
            processingResults.RunResults = JsonConverterHelper.GetArrayPropertyFromJson<IRunPeakResult>(jObject, RunResultsKeyName);
            return processingResults;
        }
    }
}
