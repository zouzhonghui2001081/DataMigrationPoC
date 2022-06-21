using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IAnalysisResultSetDescriptor : IPersistable, IEquatable<IAnalysisResultSetDescriptor>
	{
		IOriginalAnalysisResultSetDescriptor OriginalDescriptor { get; set; }
		bool OnlyOriginalExists { get; set; }
		bool Partial { get; set; }
		ReviewApproveState ReviewApproveState { get; set; }
        string ReviewedBy { get; set; }
        DateTime? ReviewedTimeStamp { get; set; }
        string ApprovedBy { get; set; }
        DateTime? ApprovedTimeStamp { get; set; }
        bool IsCopy { get; set; }
		string DataIntegrated { get; set; }
        DataSourceType DataSourceType { get; set; }
    }
}