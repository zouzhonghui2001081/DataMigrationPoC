using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.LabManagement
{
    public class ProjectInfo: IProjectInfo
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }
        
        public DateTime CreatedDateUtc { get; set; }

        public IUserInfo CreatedByUser { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public IUserInfo ModifiedByUser { get; set; }

        public string Description { get; set; }

        public bool IsEnabled { get; set; }
		public bool IsSecurityOn { get; set; }

        public bool IsESignatureOn { get; set; }

        public bool IsReviewApprovalOn { get; set; }        public DateTime? StartDateUtc { get; set; }

        public DateTime? EndDateUtc { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
