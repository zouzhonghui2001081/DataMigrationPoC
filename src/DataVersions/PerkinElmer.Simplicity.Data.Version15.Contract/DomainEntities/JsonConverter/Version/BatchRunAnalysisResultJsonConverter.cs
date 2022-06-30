using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    public class BatchRunAnalysisResultJsonConverter : IJsonConverter<BatchRunAnalysisResult>
    {
        private const string IdKeyName = "Id";
        private const string AnalysisResultSetIdKeyName = "AnalysisResultSetId";
        private const string SequenceSampleInfoBatchResultIdKeyName = "SequenceSampleInfoBatchResultId";
        private const string ProjectIdKeyName = "ProjectId";
        private const string OriginalBatchResultSetGuidKeyName = "OriginalBatchResultSetGuid";
        private const string ModifiableBatchRunInfoGuidKeyName = "ModifiableBatchRunInfoGuid";
        private const string OriginalBatchRunInfoGuidKeyName = "OriginalBatchRunInfoGuid";
        private const string BatchRunIdKeyName = "BatchRunId";
        private const string BatchRunNameKeyName = "BatchRunName";
        private const string BatchRunCreatedDateKeyName = "BatchRunCreatedDate";
        private const string BatchRunCreatedUserIdKeyName = "BatchRunCreatedUserId";
        private const string BatchRunModifiedDateKeyName = "BatchRunModifiedDate";
        private const string BatchRunModifiedUserIdKeyName = "BatchRunModifiedUserId";
        private const string SequenceSampleInfoModifiableIdKeyName = "SequenceSampleInfoModifiableId";
        private const string ProcessingMethodModifiableIdKeyName = "ProcessingMethodModifiableId";
        private const string CalibrationMethodModifiableIdKeyName = "CalibrationMethodModifiableId";
        private const string IsBlankSubtractorKeyName = "IsBlankSubtractor";
        private const string DataSourceTypeKeyName = "DataSourceType";

        public JObject ToJson(BatchRunAnalysisResult instance)
        {
            if (instance == null) return null;

            return new JObject
            {
                {IdKeyName, instance.Id},
                {AnalysisResultSetIdKeyName, instance.AnalysisResultSetId},
                {SequenceSampleInfoBatchResultIdKeyName, instance.SequenceSampleInfoBatchResultId},
                {ProjectIdKeyName, instance.ProjectId},
                {OriginalBatchResultSetGuidKeyName, instance.OriginalBatchResultSetGuid},
                {ModifiableBatchRunInfoGuidKeyName, instance.ModifiableBatchRunInfoGuid},
                {OriginalBatchRunInfoGuidKeyName, instance.OriginalBatchRunInfoGuid},
                {BatchRunIdKeyName, instance.BatchRunId},
                {BatchRunNameKeyName, instance.BatchRunName},
                {BatchRunCreatedDateKeyName, instance.BatchRunCreatedDate},
                {BatchRunCreatedUserIdKeyName, instance.BatchRunCreatedUserId},
                {BatchRunModifiedDateKeyName, instance.BatchRunModifiedDate},
                {BatchRunModifiedUserIdKeyName, instance.BatchRunModifiedUserId},
                {SequenceSampleInfoModifiableIdKeyName, instance.SequenceSampleInfoModifiableId},
                {ProcessingMethodModifiableIdKeyName, instance.ProcessingMethodModifiableId},
                {CalibrationMethodModifiableIdKeyName, instance.CalibrationMethodModifiableId},
                {IsBlankSubtractorKeyName, instance.IsBlankSubtractor},
                {DataSourceTypeKeyName, instance.DataSourceType},
            };
        }

        public BatchRunAnalysisResult FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            return new BatchRunAnalysisResult
            {
                Id = (long) jObject[IdKeyName],
                AnalysisResultSetId = (long) jObject[AnalysisResultSetIdKeyName],
                SequenceSampleInfoBatchResultId = (long) jObject[SequenceSampleInfoBatchResultIdKeyName],
                ProjectId = (long) jObject[ProjectIdKeyName],
                OriginalBatchResultSetGuid = (Guid) jObject[OriginalBatchResultSetGuidKeyName],
                ModifiableBatchRunInfoGuid = (Guid) jObject[ModifiableBatchRunInfoGuidKeyName],
                OriginalBatchRunInfoGuid = (Guid) jObject[OriginalBatchRunInfoGuidKeyName],
                BatchRunId = (long) jObject[BatchRunIdKeyName],
                BatchRunName = (string) jObject[BatchRunNameKeyName],
                BatchRunCreatedDate = (DateTime) jObject[BatchRunCreatedDateKeyName],
                BatchRunCreatedUserId = (string) jObject[BatchRunCreatedUserIdKeyName],
                BatchRunModifiedDate = (DateTime) jObject[BatchRunModifiedDateKeyName],
                BatchRunModifiedUserId = (string) jObject[BatchRunModifiedUserIdKeyName],
                SequenceSampleInfoModifiableId = (long) jObject[SequenceSampleInfoModifiableIdKeyName],
                ProcessingMethodModifiableId = (long) jObject[ProcessingMethodModifiableIdKeyName],
                CalibrationMethodModifiableId = (long) jObject[CalibrationMethodModifiableIdKeyName],
                IsBlankSubtractor = (bool) jObject[IsBlankSubtractorKeyName],
                DataSourceType = (short) jObject[DataSourceTypeKeyName],
            };
        }
    }
}
