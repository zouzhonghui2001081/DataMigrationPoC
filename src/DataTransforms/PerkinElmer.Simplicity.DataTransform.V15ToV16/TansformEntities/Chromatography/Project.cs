using Project15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.Project;
using Project16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.Project;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class Project
    {
        public static Project16 Transform(Project15 project)
        {
            var project16 = new Project16
            {
                Id = project.Id,
                Name = project.Name,
                CreatedDate = project.CreatedDate,
                CreatedUserId = project.CreatedUserId,
                CreatedUserName = project.CreatedUserName,
                ModifiedDate = project.ModifiedDate,
                ModifiedUserId = project.ModifiedUserId,
                ModifiedUserName = project.ModifiedUserName,
                Description = project.Description,
                Guid = project.Guid,
                IsEnabled = project.IsEnabled,
                IsSecurityOn = project.IsSecurityOn,
                IsESignatureOn = project.IsESignatureOn,
                IsReviewApprovalOn = project.IsReviewApprovalOn,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
            return project16;
        }
    }
}
