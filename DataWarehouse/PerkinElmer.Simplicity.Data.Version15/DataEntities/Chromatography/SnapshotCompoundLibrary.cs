using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography
{
    public class SnapshotCompoundLibrary
	{
		public long Id { get; set; }
		public long AnalysisResultSetId { get; set; }
		public string LibraryName { get; set; }
		public Guid LibraryGuid { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }

	}
}
