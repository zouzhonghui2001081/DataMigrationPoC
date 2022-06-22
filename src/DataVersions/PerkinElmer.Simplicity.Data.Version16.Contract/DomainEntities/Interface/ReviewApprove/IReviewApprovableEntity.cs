using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove
{
	public interface IReviewApprovableEntity
	{
		long Id { get; set; }
		ReviewApproveState EntityReviewApproveState { get; set; }
		string ProjectName { get; set; }
		string ProjectId { get; set; }
		string EntityId { get; set; }
		string EntityName { get; set; }
		string EntityType { get; set; }
		DateTime LastActionTimestamp { get; set; }
		string InReviewBy { get; set; }
		string InApproveBy { get; set; }
		List<string> ReviewedBy { get; set; }
		List<string> ApprovedBy { get; set; }
		string RejectedBy { get; set; }
		string RecalledBy { get; set; }
		string PostponedBy { get; set; }
		string SubmittedBy { get; set; }
		string LastModifiedBy { get; set; }
		DateTime SubmitTimestamp { get; set; }
		DateTime? ReviewedTimestamp { get; set; }
		DateTime? ApprovedTimestamp { get; set; }

		string InReviewByUserId { get; set; }
		string InApproveByUserId { get; set; }
		List<string> ReviewedByUserId { get; set; }
		List<string> ApprovedByUserId { get; set; }
		string RejectedByUserId { get; set; }
		string RecalledByUserId { get; set; }
		string PostponedByUserId { get; set; }
		string SubmittedByUserId { get; set; }
		string LastModifiedByUserId { get; set; }

		int ReviewedCount { get; set; }
		int ApprovedCount { get; set; }
        string DataName { get; set; }
        int VersionNumber { get; set; }

		string GetReviewedByUser();
		string GetApprovedByUser();
	}
}
