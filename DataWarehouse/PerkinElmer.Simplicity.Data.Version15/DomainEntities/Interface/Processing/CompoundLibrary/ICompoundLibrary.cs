using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.CompoundLibrary
{
    public interface ICompoundLibrary : IPersistable
    {
        string Name { get; set; }
        Guid Guid { get; set; }
        string Description { get; set; }
        string CreatedTime { get; set; }
        string ModifiedTime { get; set; }
    }
}
