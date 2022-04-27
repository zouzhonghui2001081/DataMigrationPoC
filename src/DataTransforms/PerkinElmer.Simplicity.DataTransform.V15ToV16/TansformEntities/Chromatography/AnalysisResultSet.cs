using AnalysisResultSet15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AnalysisResultSet;
using AnalysisResultSet16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AnalysisResultSet;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class AnalysisResultSet
    {
        public static AnalysisResultSet16 Transform(AnalysisResultSet15 analysisResultSet)
        {
            if (analysisResultSet == null) return null;
            return new AnalysisResultSet16
            {
                Id = analysisResultSet.Id,
                Guid = analysisResultSet.Guid,
                ProjectId = analysisResultSet.ProjectId,
                Name = analysisResultSet.Name,
                CreatedDate = analysisResultSet.CreatedDate,
                CreatedUserId = analysisResultSet.CreatedUserId,
                CreatedUserName = analysisResultSet.CreatedUserName,
                ModifiedDate = analysisResultSet.ModifiedDate,
                ModifiedUserId = analysisResultSet.ModifiedUserId,
                ModifiedUserName = analysisResultSet.ModifiedUserName,
                Type = analysisResultSet.Type,
                ProjectName = analysisResultSet.ProjectName,
                BatchResultSetGuid = analysisResultSet.BatchResultSetGuid,
                BatchResultSetName = analysisResultSet.BatchResultSetName,
                BatchResultSetCreatedDate = analysisResultSet.BatchResultSetCreatedDate,
                BatchResultSetCreatedUserId = analysisResultSet.BatchResultSetCreatedUserId,
                BatchResultSetModifiedDate = analysisResultSet.BatchResultSetModifiedDate,
                BatchResultSetModifiedUserId = analysisResultSet.BatchResultSetModifiedUserId,
                ReviewApproveState = analysisResultSet.ReviewApproveState,
                Imported = analysisResultSet.Imported,
                AutoProcessed = analysisResultSet.AutoProcessed,
                Partial = analysisResultSet.Partial,
                OnlyOriginalExists = analysisResultSet.OnlyOriginalExists,
                OriginalAnalysisResultSetGuid = analysisResultSet.OriginalAnalysisResultSetGuid,
                ReviewedBy = analysisResultSet.ReviewedBy,
                ReviewedTimeStamp = analysisResultSet.ReviewedTimeStamp,
                ApprovedBy = analysisResultSet.ApprovedBy,
                ApprovedTimeStamp = analysisResultSet.ApprovedTimeStamp,
                IsCopy = analysisResultSet.IsCopy
            };
        }
    }
}
