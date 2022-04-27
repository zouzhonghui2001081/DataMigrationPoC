using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.CompoundLibrary;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing.CompoundLibrary
{
    public class CompoundLibraryItem : ICompoundLibraryItem
    {
        public string CompoundName { get; set; }
        public Guid CompoundGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICompoundLibraryItemContent ItemContent { get; set; } = new CompoundLibraryItemContent();
        public bool IsBaselineCorrected { get; set; }

    }
}