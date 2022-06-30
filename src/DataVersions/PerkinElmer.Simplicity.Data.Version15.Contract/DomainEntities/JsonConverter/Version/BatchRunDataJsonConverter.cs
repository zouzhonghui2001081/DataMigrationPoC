using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchRunDataJsonConverter : IJsonConverter<BatchRunData>
    {
        private const string BatchRunKeyName = "BatchRun";
        private const string AcquisitionMethodKeyName = "AcquisitionMethod";
        private const string SequenceSampleInfoBatchResultKeyName = "SequenceSampleInfoBatchResult";
        private const string ProcessingMethodKeyName = "ProcessingMethod";
        private const string NamedContentsKeyName = "NamedContents";
        private const string StreamDataBatchResultsKeyName = "StreamDataBatchResults";

        public JObject ToJson(BatchRunData instance)
        {
            if (instance == null) return null;

            var acqusitionMethodDomain = DomainFactory.Create<IAcquisitionMethod>();
            var sequenceSampleDomain = DomainFactory.Create<ISequenceSampleInfo>();
            var processingMethodDomain = DomainFactory.Create<IProcessingMethod>();
            DomainContractAdaptor.PopulateAcquisitionMethod(instance.AcquisitionMethod, acqusitionMethodDomain);
            DomainContractAdaptor.PopulateSequenceSample(instance.SequenceSampleInfoBatchResult, sequenceSampleDomain);
            DomainContractAdaptor.PopulateProcessingMethod(instance.ProcessingMethod, processingMethodDomain);

            var jObject = new JObject
            {
                {BatchRunKeyName, JsonConverterRegistry.GetConverter<BatchRun>().ToJson(instance.BatchRun)},
                {AcquisitionMethodKeyName, JsonConverterRegistry.GetConverter<IAcquisitionMethod>().ToJson(acqusitionMethodDomain)},
                {SequenceSampleInfoBatchResultKeyName, JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().ToJson(sequenceSampleDomain)},
                {ProcessingMethodKeyName, JsonConverterRegistry.GetConverter<IProcessingMethod>().ToJson(processingMethodDomain)},
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<NamedContent>(jObject, instance.NamedContents, NamedContentsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<StreamDataBatchResult>(jObject, instance.StreamDataBatchResults, StreamDataBatchResultsKeyName);
            return jObject;
        }

        public BatchRunData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var acqusitionMethodDomain = jObject[AcquisitionMethodKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<IAcquisitionMethod>().FromJson((JObject) jObject[AcquisitionMethodKeyName]);
            var sequenceSampleDomain = jObject[SequenceSampleInfoBatchResultKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().FromJson((JObject)jObject[SequenceSampleInfoBatchResultKeyName]);
            var processingMethodDomain = jObject[ProcessingMethodKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<IProcessingMethod>().FromJson((JObject)jObject[ProcessingMethodKeyName]);

            var batchRun = jObject[BatchRunKeyName].Type == JTokenType.Null
                ? null : JsonConverterRegistry.GetConverter<BatchRun>().FromJson((JObject)jObject[BatchRunKeyName]);

            var batchRunData = new BatchRunData
            {
                BatchRun = batchRun,
                NamedContents = JsonConverterHelper.GetListPropertyFromJson<NamedContent>(jObject, NamedContentsKeyName),
                StreamDataBatchResults = JsonConverterHelper.GetListPropertyFromJson<StreamDataBatchResult>(jObject, StreamDataBatchResultsKeyName),
            };

            if (acqusitionMethodDomain != null)
            {
                var acqusitionMethodEntity = new AcquisitionMethod();
                DomainContractAdaptor.PopulateAcquisitionMethodEntity(acqusitionMethodDomain, acqusitionMethodEntity);
                batchRunData.AcquisitionMethod = acqusitionMethodEntity;
            }

            if (sequenceSampleDomain != null)
            {
                var sequenceSampleEntity = new SequenceSampleInfoBatchResult();
                DomainContractAdaptor.PopulateSequenceSampleInfoEntity(sequenceSampleDomain, sequenceSampleEntity);
                if(batchRun != null)
                    sequenceSampleEntity.BatchResultSetId = batchRun.BatchResultSetId;
                batchRunData.SequenceSampleInfoBatchResult = sequenceSampleEntity;
            }

            if (processingMethodDomain != null)
            {
                var processingMethodEntity = new ProcessingMethod();
                DomainContractAdaptor.PopulateProcessingMethodEntity(processingMethodDomain, processingMethodEntity);
                batchRunData.ProcessingMethod = processingMethodEntity;
            }

            return batchRunData;
        }
    }
}
