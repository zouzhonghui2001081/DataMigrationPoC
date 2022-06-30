using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchRunAnalysisResultDataJsonConverter : IJsonConverter<BatchRunAnalysisResultData>
    {
        private const string BatchRunAnalysisResultKeyName = "BatchRunAnalysisResult";
        private const string SequenceSampleInfoModifiableKeyName = "SequenceSampleInfoModifiable";
        private const string ModifiableProcessingMethodKeyName = "ModifiableProcessingMethod";
        private const string CalculatedChannelDataKeyName = "CalculatedChannelData";


        public JObject ToJson(BatchRunAnalysisResultData instance)
        {
            if (instance == null) return null;

            var sequenceSampleInfo = DomainFactory.Create<ISequenceSampleInfo>();
            DomainContractAdaptor.PopulateSequenceSample(instance.SequenceSampleInfoModifiable, sequenceSampleInfo);

            var processingMethod = DomainFactory.Create<IProcessingMethod>();
            DomainContractAdaptor.PopulateProcessingMethod(instance.ModifiableProcessingMethod, processingMethod);

            var jObject = new JObject
            {
                { BatchRunAnalysisResultKeyName, JsonConverterRegistry.GetConverter<BatchRunAnalysisResult>().ToJson(instance.BatchRunAnalysisResult)},
                { SequenceSampleInfoModifiableKeyName, JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().ToJson(sequenceSampleInfo)},
                { ModifiableProcessingMethodKeyName, JsonConverterRegistry.GetConverter<IProcessingMethod>().ToJson(processingMethod)},
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<CalculatedChannelCompositeData>(jObject, instance.CalculatedChannelData, CalculatedChannelDataKeyName);
            return jObject;
        }

        public BatchRunAnalysisResultData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var batchRunAnalysisResultData = new BatchRunAnalysisResultData
            {
                BatchRunAnalysisResult = jObject[BatchRunAnalysisResultKeyName].Type == JTokenType.Null
                    ? null : JsonConverterRegistry.GetConverter<BatchRunAnalysisResult>().FromJson((JObject)jObject[BatchRunAnalysisResultKeyName])
            };

            var sequenceSampleInfo = jObject[SequenceSampleInfoModifiableKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().FromJson((JObject) jObject[SequenceSampleInfoModifiableKeyName]);
            if (sequenceSampleInfo != null)
            {
                var sequenceSampleInfoModifiable = new SequenceSampleInfoModifiable();
                DomainContractAdaptor.PopulateSequenceSampleInfoEntity(sequenceSampleInfo, sequenceSampleInfoModifiable);
                sequenceSampleInfoModifiable.AnalysisResultSetId = batchRunAnalysisResultData.BatchRunAnalysisResult.AnalysisResultSetId;
                batchRunAnalysisResultData.SequenceSampleInfoModifiable = sequenceSampleInfoModifiable;
            }
            
            var processingMethodDomain = jObject[ModifiableProcessingMethodKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<IProcessingMethod>().FromJson((JObject)jObject[ModifiableProcessingMethodKeyName]);
            if (processingMethodDomain != null)
            {
                var processingMethodEntity = new ProcessingMethod();
                DomainContractAdaptor.PopulateProcessingMethodEntity(processingMethodDomain, processingMethodEntity);
                batchRunAnalysisResultData.ModifiableProcessingMethod = processingMethodEntity;
            }
            batchRunAnalysisResultData.CalculatedChannelData = JsonConverterHelper.GetListPropertyFromJson<CalculatedChannelCompositeData>(jObject, CalculatedChannelDataKeyName);

            return batchRunAnalysisResultData;
        }
    }
}
