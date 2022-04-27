using System;
using PerkinElmer.Domain.Contracts.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface IModifiableProcessingMethod : IProcessingMethod
	{
		bool ModifiedFromOriginal { get; set; }
		Guid OriginalReadOnlyMethodGuid { get; set; }
	    void CloneFromOriginal(IProcessingMethod originalMethod, bool cloneReviewApproveState = false);
    }
}