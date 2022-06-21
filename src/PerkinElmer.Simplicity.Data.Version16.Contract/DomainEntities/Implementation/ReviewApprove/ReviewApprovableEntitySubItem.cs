using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.ReviewApprove
{
	internal class ReviewApprovableEntitySubItem :IReviewApprovableEntitySubItem
	{
		public long Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectId { get; set; }
		public long EntityReviewApproveId { get; set; }
		public string EntitySubItemId { get; set; }
		public string EntitySubItemName { get; set; }
		public string EntitySubItemType { get; set; }
		public ReviewApproveState EntitySubItemReviewApproveState { get; set; }
		public string EntitySubItemSampleReportTemplate { get; set; }
		public string EntitySubItemSummaryReportGroup { get; set; }
		public string ReviewApproveComment { get; set; }
	}
}
