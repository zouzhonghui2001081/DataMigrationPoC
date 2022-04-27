using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.LabManagement
{
    public interface IProjectInfo: IPersistable
    {
        Guid Guid { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsEnabled { get; set; }
        bool IsSecurityOn { get; set; }
        bool IsESignatureOn { get; set; }
        bool IsReviewApprovalOn { get; set; }
        DateTime? StartDateUtc { get; set; }
        DateTime? EndDateUtc { get; set; }
    }
}
