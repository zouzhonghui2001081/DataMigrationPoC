using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.CompoundLibrary
{
    public interface ICompoundLibraryDescriptor : IEquatable<ICompoundLibraryDescriptor>
    {
        string Name{get;set;}
        Guid Guid{get;set;}
        string CreatedDate { get; set; }
        string ModifiedDate { get; set; }
	}
}
