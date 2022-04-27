
using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod
{
    public class CalibrationBatchRunInfo
	{
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public Guid Key { get; set; }
		public Guid BatchRunGuid { get; set; }
		public Guid BatchResultSetGuid { get; set; }
		public string BatchRunName { get; set; }
		public string ResultSetName { get; set; }
		public DateTime BatchRunAcquisitionTime { get; set; }
	}
}
