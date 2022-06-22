using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove
{
    public class ReviewApprovableDataEntity
	{
		public long Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectId { get; set; }
		public string EntityId { get; set; }
		public string InReviewBy { get; set; }
		public string InApproveBy { get; set; }
		public string ReviewedBy { get; set; }
		public string ApprovedBy { get; set; }
		public string RejectedBy { get; set; }
		public string RecalledBy { get; set; }
		public string PostponedBy { get; set; }
		public string SubmittedBy { get; set; }
		public string LastModifiedBy { get; set; }
		public string InReviewByUserId { get; set; }
		public string InApproveByUserId { get; set; }
		public string ReviewedByUserId { get; set; }
		public string ApprovedByUserId { get; set; }
		public string RejectedByUserId { get; set; }
		public string RecalledByUserId { get; set; }
		public string PostponedByUserId { get; set; }
		public string SubmittedByUserId { get; set; }
		public string LastModifiedByUserId { get; set; }

		public DateTime SubmitTimestamp { get; set; }
		public DateTime? ReviewedTimestamp { get; set; }
		public DateTime? ApprovedTimestamp { get; set; }
		public int ReviewedCount { get; set; }
		public int ApprovedCount { get; set; }
		public short EntityReviewApproveState { get; set; }
		public string EntityName { get; set; }
		public string EntityType { get; set; }
		public DateTime LastActionTimestamp { get; set; }
        public string DataName { get; set; }
        public int VersionNumber { get; set; }
    }
}
