using System;
using System.IO;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Reporting
{
    public interface IReportTemplate : IDisposable
    {
        Guid Id { get; set; }
        ReportTemplateType Category { get; set; }
        string Name { get; set; }
        DateTime CreatedDate { get; set; }
        IUserInfo CreatedByUser { get; set; }
        DateTime ModifiedDate { get; set; }
        IUserInfo ModifiedByUser { get; set; }
        Stream Content { get; set; }
        Stream Config { get; set; }
        long? ProjectId { get; set; }
        bool IsGlobal { get; set; }
        bool IsDefault { get; set; }
        ReviewApproveState ReviewApproveState { get; set; }
    }
}
