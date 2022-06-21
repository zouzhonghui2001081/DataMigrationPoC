using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface IModifiableProcessingMethod : IProcessingMethod
	{
		bool ModifiedFromOriginal { get; set; }
		Guid OriginalReadOnlyMethodGuid { get; set; }
	    void CloneFromOriginal(IProcessingMethod originalMethod, bool cloneReviewApproveState = false);
    }
}