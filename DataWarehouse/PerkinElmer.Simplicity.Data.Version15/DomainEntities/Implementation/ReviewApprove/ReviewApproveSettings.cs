﻿using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.ReviewApprove
{
	internal class ReviewApproveSettings : IReviewApproveSettings
	{
		public bool IsHideApproved { get; set; }
		public bool IsShowMyTasks { get; set; }
		public int ReviewRound { get; set; }
		public int ApproveRound { get; set; }
	}
}
