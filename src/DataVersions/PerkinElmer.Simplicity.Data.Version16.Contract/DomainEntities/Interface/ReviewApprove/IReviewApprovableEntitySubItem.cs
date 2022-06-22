namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove
{
	public interface IReviewApprovableEntitySubItem
	{
		long Id { get; set; }
		// The owner reivew approve id of this item
		long EntityReviewApproveId { get; set; }
		string ProjectName { get; set; }
		string ProjectId { get; set; }
		string EntitySubItemId { get; set; }
		string EntitySubItemName { get; set; }
		string EntitySubItemType { get; set; }
		ReviewApproveState EntitySubItemReviewApproveState { get; set; }
		string EntitySubItemSampleReportTemplate { get; set; }
		string EntitySubItemSummaryReportGroup { get; set; }
		string ReviewApproveComment { get; set; }
	}
}
