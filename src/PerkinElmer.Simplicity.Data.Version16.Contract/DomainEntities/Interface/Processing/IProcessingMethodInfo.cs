using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IProcessingMethodInfo : IPersistable, IEquatable<IProcessingMethodInfo>
	{
		string Description { get; set; }
	    bool IsDefault { get; set; }
        int? VersionNumber { get; set; }
        ReviewApproveState ReviewApproveState { get; set; }
    }
}