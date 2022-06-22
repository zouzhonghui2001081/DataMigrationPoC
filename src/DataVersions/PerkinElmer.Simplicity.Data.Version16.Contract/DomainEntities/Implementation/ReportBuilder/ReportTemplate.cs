using System;
using System.IO;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.ReportBuilder
{
    public class ReportTemplate : IReportTemplate
    {
        public ReportTemplate()
        {
            CreatedByUser = new UserInfo();
            ModifiedByUser = new UserInfo();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ReportTemplateType Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public IUserInfo CreatedByUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public IUserInfo ModifiedByUser { get; set; }
        public Stream Content { get; set; }
        public Stream Config { get; set; }
        public long? ProjectId { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsDefault { get; set; } = false;
        public ReviewApproveState ReviewApproveState { get; set; } = ReviewApproveState.NeverSubmitted;

		public void Dispose()
		{
			if(Content!=null)
			{
                Content.Dispose();
            }

            if (Config != null)
            {
                Config.Dispose();
            }
        }
    }
}
