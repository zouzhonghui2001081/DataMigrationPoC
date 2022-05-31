namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ReviewApprove
{
    public class ReviewApprovableDataEntitySubItem
	{
		public long Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectId { get; set; }
		public long EntityReviewApproveId { get; set; }
		public string EntitySubItemId { get; set; }
		public string EntitySubItemName { get; set; }
		public string EntitySubItemType { get; set; }
		public short EntitySubItemReviewApproveState { get; set; }
		public string EntitySubItemSampleReportTemplate { get; set; }
		public string EntitySubItemSummaryReportGroup { get; set; }
		public string ReviewApproveComment { get; set; }
	}
}
