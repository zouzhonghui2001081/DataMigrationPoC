using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.LabManagement
{
    public class ESignaturePointInfo : IESignaturePointInfo
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string ModuleName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsUseAuth { get; set; }

        public bool IsCustomReason { get; set; }

        public bool IsPredefinedReason { get; set; }

        public IList<string> PredefineReasons { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public IUserInfo CreatedByUser { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public IUserInfo ModifiedByUser { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
