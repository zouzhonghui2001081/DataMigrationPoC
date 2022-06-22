using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.CompoundLibrary
{
    public interface ICompoundLibraryItemContent
    {
        double RetentionTime { get; set; }
        IList<double> SpectrumAbsorbances { get; set; }
        IList<double> BaselineAbsorbances { get; set; }
        double StartWavelength { get; set; }
        double EndWavelength { get; set; }
        double Step { get; set; }
    }
}