using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.LabManagement
{
    public class ESignaturePoint
    {
        public long Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string ModuleName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsUseAuth { get; set; }

        public bool IsCustomReason { get; set; }

        public bool IsPredefinedReason { get; set; }

        public string Reasons { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedUserId { get; set; }
    }
}
