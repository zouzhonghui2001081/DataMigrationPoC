using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class Project
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }

        public string Description { get; set; }

        public Guid Guid { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsSecurityOn { get; set; }

        public bool IsESignatureOn { get; set; }

        public bool IsReviewApprovalOn { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}