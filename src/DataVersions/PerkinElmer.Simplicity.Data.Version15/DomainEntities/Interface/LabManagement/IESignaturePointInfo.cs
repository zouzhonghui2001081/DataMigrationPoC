using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.LabManagement
{
    public interface IESignaturePointInfo : IPersistable
    {
        string ModuleName { get; set; }

        int DisplayOrder { get; set; }

        bool IsUseAuth { get; set; }

        bool IsCustomReason { get; set; }

        bool IsPredefinedReason { get; set; }

        IList<string> PredefineReasons { get; set; }
    }
}
