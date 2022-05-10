using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
    internal class SequenceJsonConverter: IJsonConverter<ISequence>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InfoKeyName = "Info";
        private const string SamplesKeyName = "Samples";
        private const string ExternalBaselineRunsKeyName = "ExternalBaselineRuns";
        private const string ExternalBaselineRunsAcquisitionMethodsKeyName = "ExternalBaselineRunsAcquisitionMethods";
        private const string ExternalBaselineRunsProcessingMethodsKeyName = "ExternalBaselineRunsProcessingMethods";

        public ISequence FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var sequenceInfo = DomainFactory.Create<ISequence>();
            sequenceInfo.Info = jObject[InfoKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<ISequenceInfo>().FromJson((JObject)jObject[InfoKeyName]);
            sequenceInfo.Samples = JsonConverterHelper.GetArrayPropertyFromJson<ISequenceSampleInfo>(jObject, SamplesKeyName);
            sequenceInfo.ExternalBaselineRuns = JsonConverterHelper.GetArrayPropertyFromJson<IBatchRun>(jObject, ExternalBaselineRunsKeyName);
            sequenceInfo.ExternalBaselineRunsAcquisitionMethods = JsonConverterHelper.GetListPropertyFromJson<IAcquisitionMethod>(jObject, ExternalBaselineRunsAcquisitionMethodsKeyName);
            sequenceInfo.ExternalBaselineRunsProcessingMethods = JsonConverterHelper.GetListPropertyFromJson<IProcessingMethod>(jObject, ExternalBaselineRunsProcessingMethodsKeyName);
            return sequenceInfo;
        }

        public JObject ToJson(ISequence instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InfoKeyName, JsonConverterRegistry.GetConverter<ISequenceInfo>().ToJson(instance.Info)},
			};

            JsonConverterHelper.SetCollectionPropertyToJObject<ISequenceSampleInfo>(jObject, instance.Samples, SamplesKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<IBatchRun>(jObject, instance.ExternalBaselineRuns, ExternalBaselineRunsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<IAcquisitionMethod>(jObject, instance.ExternalBaselineRunsAcquisitionMethods, ExternalBaselineRunsAcquisitionMethodsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<IProcessingMethod>(jObject, instance.ExternalBaselineRunsProcessingMethods, ExternalBaselineRunsProcessingMethodsKeyName);

            return jObject;
        }
    }
}
