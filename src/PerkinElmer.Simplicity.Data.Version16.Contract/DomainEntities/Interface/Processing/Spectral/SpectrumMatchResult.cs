using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral
{
    public class SpectrumMatchResult
    {
        public string CompoundName { get; set; } //for LibraryConfirmation always match the name of our peak
        public string LibraryName { get; set; }
        public Guid LibraryGuid { get; set; }
        public double HitDistance { get; set; }
        public bool Confirmed { get; set; } // this will only be filled for Library Confirmation
        public LibrarySpectrum LibrarySpectrum { get; set; } //? should we fill this for Library Search/Confirmation which are Batch operations (probably not)
        
    }

    public class LibrarySpectrum
    {
        public double StartWavelength { get; set; }
        public double EndWavelength { get; set; }
        public double Step { get; set; }
        public IList<double> Intensities { get; set; }
    }

    public class LibrarySearchResult
    {
        public IList<SpectrumMatchResult> LibraryHits { get; set; }
    }
}