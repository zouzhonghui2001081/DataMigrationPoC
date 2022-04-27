using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.LabManagement
{
    public interface IApprovalReviewItemInfo : IPersistable
    {
        int DisplayOrder { get; set; }

        bool IsApprovalReviewOn { get; set; }

        bool IsSubmitReviewApprove { get; set; }

        bool IsSubmitApprove { get; set; }

        string DisplayName { get; set; }
    }
}
