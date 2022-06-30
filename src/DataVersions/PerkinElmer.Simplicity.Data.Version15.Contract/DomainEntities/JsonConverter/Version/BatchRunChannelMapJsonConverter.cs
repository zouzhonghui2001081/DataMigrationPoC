

using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchRunChannelMapJsonConverter : IJsonConverter<BatchRunChannelMap>
    {
        private const string IdKeyName = "Id";
        private const string AnalysisResultSetIdKeyName = "AnalysisResultSetId";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string BatchRunGuidKeyName = "BatchRunGuid";
        private const string OriginalBatchRunGuidKeyName = "OriginalBatchRunGuid";
        private const string BatchRunChannelDescriptorTypeKeyName = "BatchRunChannelDescriptorType";
        private const string BatchRunChannelDescriptorKeyName = "BatchRunChannelDescriptor";
        private const string ProcessingMethodGuidKeyName = "ProcessingMethodGuid";
        private const string ProcessingMethodChannelGuidKeyName = "ProcessingMethodChannelGuid";
        private const string XDataKeyName = "XData";
        private const string YDataKeyName = "YData";

        public JObject ToJson(BatchRunChannelMap instance)
        {
            if (instance == null) return null;
            return new JObject
            {
                {IdKeyName, instance.Id},
                {AnalysisResultSetIdKeyName, instance.AnalysisResultSetId},
                {BatchRunChannelGuidKeyName, instance.BatchRunChannelGuid},
                {BatchRunGuidKeyName, instance.BatchRunGuid},
                {OriginalBatchRunGuidKeyName, instance.OriginalBatchRunGuid},
                {BatchRunChannelDescriptorTypeKeyName, instance.BatchRunChannelDescriptorType},
                {BatchRunChannelDescriptorKeyName, instance.BatchRunChannelDescriptor},
                {ProcessingMethodGuidKeyName, instance.ProcessingMethodGuid},
                {ProcessingMethodChannelGuidKeyName, instance.ProcessingMethodChannelGuid},
                {XDataKeyName, instance.XData == null ? null : JArray.FromObject(instance.XData)},
                {YDataKeyName, instance.YData == null ? null : JArray.FromObject(instance.YData)},
            };
        }

        public BatchRunChannelMap FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            return new BatchRunChannelMap
            {
                Id = (long) jObject[IdKeyName],
                AnalysisResultSetId = (long) jObject[AnalysisResultSetIdKeyName],
                BatchRunChannelGuid = (Guid) jObject[BatchRunChannelGuidKeyName],
                BatchRunGuid = (Guid) jObject[BatchRunGuidKeyName],
                OriginalBatchRunGuid = (Guid) jObject[OriginalBatchRunGuidKeyName],
                BatchRunChannelDescriptorType = (string) jObject[BatchRunChannelDescriptorTypeKeyName],
                BatchRunChannelDescriptor = (string) jObject[BatchRunChannelDescriptorKeyName],
                ProcessingMethodGuid = (Guid) jObject[ProcessingMethodGuidKeyName],
                ProcessingMethodChannelGuid = (Guid) jObject[ProcessingMethodChannelGuidKeyName],
                XData = JsonConverterHelper.GetArrayPropertyFromJson<double>(jObject, XDataKeyName),
                YData = JsonConverterHelper.GetArrayPropertyFromJson<double>(jObject, YDataKeyName)
            };
        }
    }
}
