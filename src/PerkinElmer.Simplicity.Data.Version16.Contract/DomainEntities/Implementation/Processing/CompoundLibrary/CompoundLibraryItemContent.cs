using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.CompoundLibrary;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing.CompoundLibrary
{
    public class CompoundLibraryItemContent : ICompoundLibraryItemContent
    {
        public double RetentionTime { get; set; }
        public IList<double> SpectrumAbsorbances { get; set; }
        public IList<double> BaselineAbsorbances { get; set; }
        public double StartWavelength { get; set; }
        public double EndWavelength { get; set; }
        public double Step { get; set; }        
    }
}