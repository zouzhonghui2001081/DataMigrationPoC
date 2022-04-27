using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface IProcessingMethodInfo : IPersistable, IEquatable<IProcessingMethodInfo>
	{
		string Description { get; set; }
	    bool IsDefault { get; set; }
        int? VersionNumber { get; set; }
        ReviewApproveState ReviewApproveState { get; set; }
    }
}