using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface IAnalysisResultSetReviewApproveInfo
	{
		string AnalysisResultSetId { get; set; }

		string AnalysisResultSetName { get; set; }

		List<IAnalysisResultSetReviewApproveSubItemInfo> SubItems { get; set; }

	}

	public interface IAnalysisResultSetReviewApproveSubItemInfo
	{
		string SubItemId { get; set; }

		string SubItemName { get; set; }

		string SubItemType { get; set; }
		string EntitySubItemSampleReportTemplate { get; set; }
		string EntitySubItemSummaryReportGroup { get; set; }
	}
}