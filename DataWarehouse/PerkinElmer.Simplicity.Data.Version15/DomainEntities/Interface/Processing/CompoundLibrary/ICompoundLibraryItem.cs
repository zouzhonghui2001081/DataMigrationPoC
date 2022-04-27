using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.CompoundLibrary
{
    public interface ICompoundLibraryItem
    {
        string CompoundName { get; set; }
        Guid CompoundGuid { get; set; }
        DateTime CreatedDate { get; set; }
        ICompoundLibraryItemContent ItemContent { get; set; }
        bool IsBaselineCorrected { get; set; }
    }
}

