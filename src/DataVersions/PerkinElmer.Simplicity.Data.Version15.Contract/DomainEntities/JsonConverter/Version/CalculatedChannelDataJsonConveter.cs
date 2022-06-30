using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class CalculatedChannelDataJsonConveter : IJsonConverter<CalculatedChannelData>
    {
        private const string BatchRunAnalysisResultIdKeyName = "BatchRunAnalysisResultId";
        private const string IdKeyName = "Id";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string ChannelDataTypeKeyName = "ChannelDataType";
        private const string ChannelTypeKeyName = "ChannelType";
        private const string ChannelIndexKeyName = "ChannelIndex";
        private const string ChannelMetaDataKeyName = "ChannelMetaData";
        private const string RawChannelTypeKeyName = "RawChannelType";
        private const string BlankSubtractionAppliedKeyName = "BlankSubtractionApplied";
        private const string SmoothAppliedKeyName = "SmoothApplied";

        public JObject ToJson(CalculatedChannelData instance)
        {
            if (instance == null) return null;
            return new JObject
            {
                {BatchRunAnalysisResultIdKeyName, instance.BatchRunAnalysisResultId},
                {IdKeyName, instance.Id},
                {BatchRunChannelGuidKeyName, instance.BatchRunChannelGuid},
                {ChannelDataTypeKeyName, instance.ChannelDataType},
                {ChannelTypeKeyName, instance.ChannelType},
                {ChannelIndexKeyName, instance.ChannelIndex},
                {ChannelMetaDataKeyName, instance.ChannelMetaData},
                {RawChannelTypeKeyName, instance.RawChannelType},
                {BlankSubtractionAppliedKeyName, instance.BlankSubtractionApplied},
                {SmoothAppliedKeyName, instance.SmoothApplied},
            };
        }

        public CalculatedChannelData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            return new CalculatedChannelData
            {
                BatchRunAnalysisResultId = (long)jObject[BatchRunAnalysisResultIdKeyName],
                Id = (long)jObject[IdKeyName],
                BatchRunChannelGuid = (Guid)jObject[BatchRunChannelGuidKeyName],
                ChannelDataType = (int)jObject[ChannelDataTypeKeyName],
                ChannelType = (int)jObject[ChannelTypeKeyName],
                ChannelIndex = (int)jObject[ChannelIndexKeyName],
                ChannelMetaData = (string)jObject[ChannelMetaDataKeyName],
                RawChannelType = (int)jObject[RawChannelTypeKeyName],
                BlankSubtractionApplied = (bool)jObject[BlankSubtractionAppliedKeyName],
                SmoothApplied = (bool)jObject[SmoothAppliedKeyName]
            };
        }
    }
}
