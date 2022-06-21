using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    public class Persistable: IPersistable
    {
        public object Clone()
        {
            return (Persistable)this.MemberwiseClone();
        }

        public string Name { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public IUserInfo CreatedByUser { get; set; }
        public DateTime ModifiedDateUtc { get; set; }
        public IUserInfo ModifiedByUser { get; set; }
    }
}
