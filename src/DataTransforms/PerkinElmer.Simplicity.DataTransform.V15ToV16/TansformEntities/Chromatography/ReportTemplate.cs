using ReportTemplate15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReportTemplate;
using ReportTemplate16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReportTemplate;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class ReportTemplate
    {
        public static ReportTemplate16 Transform(ReportTemplate15 reportTemplate)
        {
            var reportTemplate16 = new ReportTemplate16
            {
                Id = reportTemplate.Id,
                Category = reportTemplate.Category,
                Name = reportTemplate.Name,
                CreatedDate = reportTemplate.CreatedDate,
                CreatedUserId = reportTemplate.CreatedUserId,
                CreatedUserName = reportTemplate.CreatedUserName,
                ModifiedDate = reportTemplate.ModifiedDate,
                ModifiedUserId = reportTemplate.ModifiedUserId,
                ModifiedUserName = reportTemplate.ModifiedUserName,
                Content = reportTemplate.Content,
                Config = reportTemplate.Config,
                ProjectId = reportTemplate.ProjectId,
                IsGlobal = reportTemplate.IsGlobal,
                IsDefault = reportTemplate.IsDefault,
                ReviewApproveState = reportTemplate.ReviewApproveState
            };
            return reportTemplate16;
        }
    }
}
