using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove
{
	public class ReviewApproveEntityStateChangedEventArgs : EventArgs
	{
		public IReviewApprovableEntity Entity { get; set; }
	}
}
