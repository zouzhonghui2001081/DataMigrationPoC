using ESignaturePoint15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement.ESignaturePoint;
using ESignaturePoint16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.LabManagement.ESignaturePoint;


namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.LabManagement
{
    public class ESignaturePoint
    {
        public static ESignaturePoint16 Transform(ESignaturePoint15 eSignaturePoint)
        {
            if (eSignaturePoint == null) return null;
            return new ESignaturePoint16
            {
                Id = eSignaturePoint.Id,
                Guid = eSignaturePoint.Guid,
                Name = eSignaturePoint.Name,
                ModuleName = eSignaturePoint.ModuleName,
                DisplayOrder = eSignaturePoint.DisplayOrder,
                IsUseAuth = eSignaturePoint.IsUseAuth,
                IsCustomReason = eSignaturePoint.IsCustomReason,
                IsPredefinedReason = eSignaturePoint.IsPredefinedReason,
                Reasons = eSignaturePoint.Reasons,
                CreatedDate = eSignaturePoint.CreatedDate,
                CreatedUserId = eSignaturePoint.CreatedUserId,
                ModifiedDate = eSignaturePoint.ModifiedDate,
                ModifiedUserId = eSignaturePoint.ModifiedUserId
            };
        }
    }
}
