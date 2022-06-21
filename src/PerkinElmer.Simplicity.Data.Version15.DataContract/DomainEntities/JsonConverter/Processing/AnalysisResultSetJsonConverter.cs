using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class AnalysisResultSetJsonConverter : IJsonConverter<IAnalysisResultSet>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InfoKeyName = "Info";
        private const string BatchRunsKeyName = "BatchRuns";
        private const string ExternalBaselineRunsKeyName = "ExternalBaselineRuns";
        private const string ReadOnlyAcquisitionMethodsKeyName = "ReadOnlyAcquisitionMethods";
        private const string ReadOnlyProcessingMethodsKeyName = "ReadOnlyProcessingMethods";
        private const string ModifiableProcessingMethodsKeyName = "ModifiableProcessingMethods";
        private const string ResultsKeyName = "Results";

        public JObject ToJson(IAnalysisResultSet instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InfoKeyName, JsonConverterRegistry.GetConverter<IAnalysisResultSetDescriptor>().ToJson(instance.Descriptor)},
                {ResultsKeyName, JsonConverterRegistry.GetConverter<IProcessingResults>().ToJson(instance.Results)}
            };
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.BatchRuns, BatchRunsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ExternalBaselineRuns, ExternalBaselineRunsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ReadOnlyAcquisitionMethods, ReadOnlyAcquisitionMethodsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ReadOnlyProcessingMethods, ReadOnlyProcessingMethodsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject(jObject, instance.ModifiableProcessingMethods, ModifiableProcessingMethodsKeyName);

            return jObject;
        }

        public IAnalysisResultSet FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var analysisResultSet = DomainFactory.Create<IAnalysisResultSet>();
            analysisResultSet.Descriptor = jObject[InfoKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<IAnalysisResultSetDescriptor>().FromJson((JObject)jObject[InfoKeyName]);
            analysisResultSet.BatchRuns = JsonConverterHelper.GetArrayPropertyFromJson<IVirtualBatchRun>(jObject, BatchRunsKeyName);
            analysisResultSet.ExternalBaselineRuns = JsonConverterHelper.GetArrayPropertyFromJson<IBatchRun>(jObject, ExternalBaselineRunsKeyName);
            analysisResultSet.ReadOnlyAcquisitionMethods = JsonConverterHelper.GetArrayPropertyFromJson<IAcquisitionMethod>(jObject, ReadOnlyAcquisitionMethodsKeyName);
            analysisResultSet.ReadOnlyProcessingMethods = JsonConverterHelper.GetArrayPropertyFromJson<IProcessingMethod>(jObject, ReadOnlyProcessingMethodsKeyName);
            analysisResultSet.ModifiableProcessingMethods = JsonConverterHelper.GetArrayPropertyFromJson<IModifiableProcessingMethod>(jObject, ModifiableProcessingMethodsKeyName);
            analysisResultSet.Results = jObject[ResultsKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<IProcessingResults>().FromJson((JObject)jObject[ResultsKeyName]);

            return analysisResultSet;
        }
    }
}
