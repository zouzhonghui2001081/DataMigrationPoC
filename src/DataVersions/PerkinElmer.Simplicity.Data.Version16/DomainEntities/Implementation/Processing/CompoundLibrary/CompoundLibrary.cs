using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing.CompoundLibrary
{
    public class CompoundLibrary : ICompoundLibrary
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public IUserInfo CreatedByUser { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public IUserInfo ModifiedByUser { get; set; }
        public string Description { get; set; }
        public string CreatedTime { get; set; }
        public string ModifiedTime { get; set; }
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
