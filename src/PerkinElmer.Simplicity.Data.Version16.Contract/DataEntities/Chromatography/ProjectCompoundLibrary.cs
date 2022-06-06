using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography
{
    public class ProjectCompoundLibrary
	{
		public ProjectCompoundLibrary()
		{
			CompoundLibraryItems = new List<CompoundLibraryItem>();
		}
		public long Id { get; set; }
		public long ProjectId { get; set; }
		public string LibraryName { get; set; }
        public Guid LibraryGuid { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUserId { get; set; }
		public string CreatedUserName { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string ModifiedUserId { get; set; }
		public string ModifiedUserName { get; set; }
		public List<CompoundLibraryItem> CompoundLibraryItems { get; set; }
	}
}
