using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchRunJsonConverter : IJsonConverter<BatchRun>
    {
        private const string IdKeyName = "Id";
        private const string NameKeyName = "Name";
        private const string GuidKeyName = "Guid";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string CreatedUserIdKeyName = "CreatedUserId";
        private const string ModifiedDateKeyName = "ModifiedDate";
        private const string ModifiedUserIdKeyName = "ModifiedUserId";
        private const string IsBaselineRunKeyName = "IsBaselineRun";
        private const string AcquisitionCompletionStateKeyName = "AcquisitionCompletionState";
        private const string AcquisitionTimeKeyName = "AcquisitionTime";
        private const string RepeatIndexKeyName = "RepeatIndex";
        private const string SequenceSampleInfoBatchResultIdKeyName = "SequenceSampleInfoBatchResultId";
        private const string BatchResultSetIdKeyName = "BatchResultSetId";
        private const string ProcessingMethodBatchResultIdKeyName = "ProcessingMethodBatchResultId";
        private const string CalibrationMethodBatchResultIdKeyName = "CalibrationMethodBatchResultId";
        private const string AcquisitionMethodBatchResultIdKeyName = "AcquisitionMethodBatchResultId";
        private const string DataSourceTypeKeyName = "DataSourceType";
        private const string IsModifiedAfterSubmissionKeyName = "IsModifiedAfterSubmission";
        private const string AcquisitionCompletionStateDetailsKeyName = "AcquisitionCompletionStateDetails";
        private const string StreamDataListKeyName = "StreamDataList";

        public JObject ToJson(BatchRun instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {IdKeyName, instance.Id},
                {NameKeyName, instance.Name},
                {GuidKeyName, instance.Guid},
                {CreatedDateKeyName, instance.CreatedDate},
                {CreatedUserIdKeyName, instance.CreatedUserId},
                {ModifiedDateKeyName, instance.ModifiedDate},
                {ModifiedUserIdKeyName, instance.ModifiedUserId},
                {IsBaselineRunKeyName, instance.IsBaselineRun},
                {AcquisitionCompletionStateKeyName, instance.AcquisitionCompletionState},
                {AcquisitionTimeKeyName, instance.AcquisitionTime},
                {RepeatIndexKeyName, instance.RepeatIndex},
                {SequenceSampleInfoBatchResultIdKeyName, instance.SequenceSampleInfoBatchResultId},
                {BatchResultSetIdKeyName, instance.BatchResultSetId},
                {ProcessingMethodBatchResultIdKeyName, instance.ProcessingMethodBatchResultId},
                {CalibrationMethodBatchResultIdKeyName, instance.CalibrationMethodBatchResultId},
                {AcquisitionMethodBatchResultIdKeyName, instance.AcquisitionMethodBatchResultId},
                {DataSourceTypeKeyName, instance.DataSourceType},
                {IsModifiedAfterSubmissionKeyName, instance.IsModifiedAfterSubmission},
                {AcquisitionCompletionStateDetailsKeyName, instance.AcquisitionCompletionStateDetails},
            };
            
            JsonConverterHelper.SetCollectionPropertyToJObject<StreamDataBatchResult>(jObject, instance.StreamDataList, StreamDataListKeyName);
            return jObject;
        }

        public BatchRun FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var batchRun = new BatchRun
            {
                Id = (long) jObject[IdKeyName],
                Name = (string) jObject[NameKeyName],
                Guid = (Guid) jObject[GuidKeyName],
                CreatedDate = (DateTime) jObject[CreatedDateKeyName],
                CreatedUserId = (string) jObject[CreatedUserIdKeyName],
                ModifiedDate = (DateTime) jObject[ModifiedDateKeyName],
                ModifiedUserId = (string) jObject[ModifiedUserIdKeyName],
                IsBaselineRun = (bool) jObject[IsBaselineRunKeyName],
                AcquisitionCompletionState = (short) jObject[AcquisitionCompletionStateKeyName],
                RepeatIndex = (int) jObject[RepeatIndexKeyName],
                SequenceSampleInfoBatchResultId = (long) jObject[SequenceSampleInfoBatchResultIdKeyName],
                BatchResultSetId = (long) jObject[BatchResultSetIdKeyName],
                ProcessingMethodBatchResultId = (long) jObject[ProcessingMethodBatchResultIdKeyName],
                CalibrationMethodBatchResultId = (long) jObject[CalibrationMethodBatchResultIdKeyName],
                AcquisitionMethodBatchResultId = (long) jObject[AcquisitionMethodBatchResultIdKeyName],
                DataSourceType = (short) jObject[DataSourceTypeKeyName],
                IsModifiedAfterSubmission = (bool) jObject[IsModifiedAfterSubmissionKeyName],
                AcquisitionCompletionStateDetails = (string) jObject[AcquisitionCompletionStateDetailsKeyName],
                StreamDataList = JsonConverterHelper.GetListPropertyFromJson<StreamDataBatchResult>(jObject, StreamDataListKeyName),
            };
            return batchRun;
        }
    }
}
