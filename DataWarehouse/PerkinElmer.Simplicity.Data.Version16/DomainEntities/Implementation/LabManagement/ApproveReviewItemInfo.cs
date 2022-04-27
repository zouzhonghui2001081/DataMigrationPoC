using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.LabManagement
{
    public class ApproveReviewItemInfo : IApprovalReviewItemInfo
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsApprovalReviewOn { get; set; }

        public bool IsSubmitReviewApprove { get; set; }

        public bool IsSubmitApprove { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public IUserInfo CreatedByUser { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public IUserInfo ModifiedByUser { get; set; }

        public string DisplayName { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
        public bool Equal(IApprovalReviewItemInfo info)
        {
            bool isEqual = false;
            if(Name == info.Name && IsApprovalReviewOn== info.IsApprovalReviewOn)  
            {
                if (IsApprovalReviewOn)
                {
                    isEqual = IsSubmitApprove == info.IsSubmitApprove
                                && IsSubmitReviewApprove == info.IsSubmitReviewApprove;
                }
                else
                    isEqual = true;
            }
            return isEqual;
        }
    }
}
