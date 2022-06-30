
using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class AnalysisResultSetJsonConverter : IJsonConverter<AnalysisResultSet>
    {
        private const string IdKeyName = "Id";
        private const string GuidKeyName = "Guid";
        private const string ProjectIdKeyName = "ProjectId";
        private const string NameKeyName = "Name";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string CreatedUserIdKeyName = "CreatedUserId";
        private const string CreatedUserNameKeyName = "CreatedUserName";
        private const string ModifiedDateKeyName = "ModifiedDate";
        private const string ModifiedUserIdKeyName = "ModifiedUserId";
        private const string ModifiedUserNameKeyName = "ModifiedUserName";
        private const string TypeKeyName = "Type";
        private const string ProjectNameKeyName = "ProjectName";
        private const string BatchResultSetGuidKeyName = "BatchResultSetGuid";
        private const string BatchResultSetNameKeyName = "BatchResultSetName";
        private const string BatchResultSetCreatedDateKeyName = "BatchResultSetCreatedDate";
        private const string BatchResultSetCreatedUserIdKeyName = "BatchResultSetCreatedUserId";
        private const string BatchResultSetModifiedDateKeyName = "BatchResultSetModifiedDate";
        private const string BatchResultSetModifiedUserIdKeyName = "BatchResultSetModifiedUserId";
        private const string ReviewApproveStateKeyName = "ReviewApproveState";
        private const string ImportedKeyName = "Imported";
        private const string AutoProcessedKeyName = "AutoProcessed";
        private const string PartialKeyName = "Partial";
        private const string OnlyOriginalExistsKeyName = "OnlyOriginalExists";
        private const string OriginalAnalysisResultSetGuidKeyName = "OriginalAnalysisResultSetGuid";
        private const string ReviewedByKeyName = "ReviewedBy";
        private const string ReviewedTimeStampKeyName = "ReviewedTimeStamp";
        private const string ApprovedByKeyName = "ApprovedBy";
        private const string ApprovedTimeStampKeyName = "ApprovedTimeStamp";
        private const string IsCopyKeyName = "IsCopy";

        public JObject ToJson(AnalysisResultSet instance)
        {
            if (instance == null) return null;
            return new JObject
            {
                {IdKeyName, instance.Id},
                {GuidKeyName, instance.Guid},
                {ProjectIdKeyName, instance.ProjectId},
                {NameKeyName, instance.Name},
                {CreatedDateKeyName, instance.CreatedDate},
                {CreatedUserIdKeyName, instance.CreatedUserId},
                {CreatedUserNameKeyName, instance.CreatedUserName},
                {ModifiedDateKeyName, instance.ModifiedDate},
                {ModifiedUserIdKeyName, instance.ModifiedUserId},
                {ModifiedUserNameKeyName, instance.ModifiedUserName},
                {TypeKeyName, instance.Type},
                {ProjectNameKeyName, instance.ProjectName},
                {BatchResultSetGuidKeyName, instance.BatchResultSetGuid},
                {BatchResultSetNameKeyName, instance.BatchResultSetName},
                {BatchResultSetCreatedDateKeyName, instance.BatchResultSetCreatedDate},
                {BatchResultSetCreatedUserIdKeyName, instance.BatchResultSetCreatedUserId},
                {BatchResultSetModifiedDateKeyName, instance.BatchResultSetModifiedDate},
                {BatchResultSetModifiedUserIdKeyName, instance.BatchResultSetModifiedUserId},
                {ReviewApproveStateKeyName, instance.ReviewApproveState},
                {ImportedKeyName, instance.Imported},
                {AutoProcessedKeyName, instance.AutoProcessed},
                {PartialKeyName, instance.Partial},
                {OnlyOriginalExistsKeyName, instance.OnlyOriginalExists},
                {OriginalAnalysisResultSetGuidKeyName, instance.OriginalAnalysisResultSetGuid},
                {ReviewedByKeyName, instance.ReviewedBy},
                {ReviewedTimeStampKeyName, instance.ReviewedTimeStamp},
                {ApprovedByKeyName, instance.ApprovedBy},
                {ApprovedTimeStampKeyName, instance.ApprovedTimeStamp},
                {IsCopyKeyName, instance.IsCopy}
            };
        }

        public AnalysisResultSet FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            return new AnalysisResultSet
            {
                Id = (long) jObject[IdKeyName],
                Guid = (Guid) jObject[GuidKeyName],
                ProjectId = (long) jObject[ProjectIdKeyName],
                Name = (string) jObject[NameKeyName],
                CreatedDate = (DateTime) jObject[CreatedDateKeyName],
                CreatedUserId = (string) jObject[CreatedUserIdKeyName],
                CreatedUserName = (string) jObject[CreatedUserNameKeyName],
                ModifiedDate = (DateTime) jObject[ModifiedDateKeyName],
                ModifiedUserId = (string) jObject[ModifiedUserIdKeyName],
                ModifiedUserName = (string) jObject[ModifiedUserNameKeyName],
                Type = (short) jObject[TypeKeyName],
                ProjectName = (string) jObject[ProjectNameKeyName],
                BatchResultSetGuid = (Guid) jObject[BatchResultSetGuidKeyName],
                BatchResultSetName = (string) jObject[BatchResultSetNameKeyName],
                BatchResultSetCreatedDate = (DateTime) jObject[BatchResultSetCreatedDateKeyName],
                BatchResultSetCreatedUserId = (string) jObject[BatchResultSetCreatedUserIdKeyName],
                BatchResultSetModifiedDate = (DateTime) jObject[BatchResultSetModifiedDateKeyName],
                BatchResultSetModifiedUserId = (string) jObject[BatchResultSetModifiedUserIdKeyName],
                ReviewApproveState = (short) jObject[ReviewApproveStateKeyName],
                Imported = (bool) jObject[ImportedKeyName],
                AutoProcessed = (bool) jObject[AutoProcessedKeyName],
                Partial = (bool) jObject[PartialKeyName],
                OnlyOriginalExists = (bool) jObject[OnlyOriginalExistsKeyName],
                OriginalAnalysisResultSetGuid = (Guid) jObject[OriginalAnalysisResultSetGuidKeyName],
                ReviewedBy = (string) jObject[ReviewedByKeyName],
                ReviewedTimeStamp =  (DateTime?) jObject[ReviewedTimeStampKeyName],
                ApprovedBy = (string) jObject[ApprovedByKeyName],
                ApprovedTimeStamp = (DateTime?) jObject[ApprovedTimeStampKeyName],
                IsCopy = (bool) jObject[IsCopyKeyName],
            };
        }
    }
}
