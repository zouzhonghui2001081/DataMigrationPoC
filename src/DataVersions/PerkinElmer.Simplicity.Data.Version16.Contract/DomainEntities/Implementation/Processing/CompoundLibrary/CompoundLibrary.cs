using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.CompoundLibrary;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing.CompoundLibrary
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
