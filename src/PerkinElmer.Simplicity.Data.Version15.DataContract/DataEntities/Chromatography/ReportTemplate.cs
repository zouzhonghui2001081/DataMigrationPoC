using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography
{
    public class ReportTemplate
    {
        public Guid Id { get; set; }       
        public string Category { get; set; }
        public string Name { get; set; }       
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }
        public byte[] Content { get; set; }
        public byte[] Config { get; set; }
        public long? ProjectId { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsDefault { get; set; } = false;
        public short ReviewApproveState { get; set; } = 0;
    }
}
