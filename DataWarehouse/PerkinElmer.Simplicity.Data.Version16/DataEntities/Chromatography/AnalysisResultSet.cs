using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class AnalysisResultSet
    {
		public long Id { get; set; }
		public Guid Guid { get; set; }
		public long ProjectId { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public string CreatedUserId { get; set; }
		public string CreatedUserName { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string ModifiedUserId { get; set; }
		public string ModifiedUserName { get; set; }
		public short Type { get; set; }
		public string ProjectName { get; set; }
		public Guid BatchResultSetGuid { get; set; }
		public string BatchResultSetName { get; set; }
		public DateTime BatchResultSetCreatedDate { get; set; }
		public string BatchResultSetCreatedUserId { get; set; }
		public DateTime BatchResultSetModifiedDate { get; set; }
		public string BatchResultSetModifiedUserId { get; set; }
		public short ReviewApproveState { get; set; }
        public bool Imported { get; set; }
        public bool AutoProcessed { get; set; }
        public bool Partial { get; set; }
        public bool OnlyOriginalExists { get; set; }
        public Guid OriginalAnalysisResultSetGuid { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedTimeStamp { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedTimeStamp { get; set; }
        public bool IsCopy { get; set; }
    }
}
