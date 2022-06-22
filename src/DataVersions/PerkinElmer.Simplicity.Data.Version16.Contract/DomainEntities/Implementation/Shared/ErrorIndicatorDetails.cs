using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    internal class ErrorIndicatorDetails : IErrorIndicatorDetails
    {
        public bool IsInError { get; set; }
        public IList<string> ErrorDescriptions { get; set; }
    }
}