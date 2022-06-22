using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IErrorIndicatorDetails
    {
        bool IsInError { get; set; }
        IList<string> ErrorDescriptions { get; set; }
    }
}