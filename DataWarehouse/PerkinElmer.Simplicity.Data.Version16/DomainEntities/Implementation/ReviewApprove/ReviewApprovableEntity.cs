using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.ReviewApprove
{
	internal class ReviewApprovableEntity : IReviewApprovableEntity
	{
		public long Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectId { get; set; }
		public string EntityId { get; set; }
		public string InReviewBy { get; set; }
		public string InApproveBy { get; set; }
		public List<string> ReviewedBy { get; set; }
		public List<string> ApprovedBy { get; set; }
		public string RejectedBy { get; set; }
		public string RecalledBy { get; set; }
		public string PostponedBy { get; set; }
		public string SubmittedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public DateTime SubmitTimestamp { get; set; }
		public DateTime? ReviewedTimestamp { get; set; }
		public DateTime? ApprovedTimestamp { get; set; }
		public int ReviewedCount { get; set; }
		public int ApprovedCount { get; set; }
		public ReviewApproveState EntityReviewApproveState { get; set; }
		public string EntityName { get; set; }
		public string EntityType { get; set; }
		public DateTime LastActionTimestamp { get; set; }

		public string LastModifiedByUserId { get; set; }
		public string InReviewByUserId { get; set; }
		public string InApproveByUserId { get; set; }
		public List<string> ReviewedByUserId { get; set; }
		public List<string> ApprovedByUserId { get; set; }
		public string RejectedByUserId { get; set; }
		public string RecalledByUserId { get; set; }
		public string PostponedByUserId { get; set; }
		public string SubmittedByUserId { get; set; }
        public string DataName { get; set; }
        public int VersionNumber { get; set; }

        public string GetApprovedByUser()
        {
			string approvedBy = null;
			if (ApprovedBy?.Count > 0)
			{
				approvedBy = string.Join(";", ApprovedBy);
			}
			return approvedBy;
		}

        public string GetReviewedByUser()
        {
			string reviewedBy = null;
			if (ReviewedBy?.Count > 0)
			{
				reviewedBy = string.Join(";", ReviewedBy);
			}
			return reviewedBy;
		}
    }
}
