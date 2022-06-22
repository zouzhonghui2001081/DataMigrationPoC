using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.LabManagement
{
    public class ApprovalReviewItem
    {
        public long Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsApprovalReviewOn { get; set; }

        public bool IsSubmitReviewApprove { get; set; }

        public bool IsSubmitApprove { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUserId { get; set; }
    }
}
