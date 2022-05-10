using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class AnalysisResultSetReviewApproveInfo : IAnalysisResultSetReviewApproveInfo
	{
		public AnalysisResultSetReviewApproveInfo()
		{
			SubItems = new List<IAnalysisResultSetReviewApproveSubItemInfo>();
		}
		public string AnalysisResultSetId { get; set; }

		public string AnalysisResultSetName { get; set; }

		public List<IAnalysisResultSetReviewApproveSubItemInfo> SubItems { get; set; }

	}

	public class AnalysisResultSetReviewApproveSubItemInfo : IAnalysisResultSetReviewApproveSubItemInfo
	{
		public string SubItemId { get; set; }

		public string SubItemName { get; set; }
		public string EntitySubItemSampleReportTemplate { get; set; }
		public string EntitySubItemSummaryReportGroup { get; set; }
		public string SubItemType { get; set; }
	}
}