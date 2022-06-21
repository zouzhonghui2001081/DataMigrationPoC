using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.Spectral
{
    public class StandardConfirmationResult
    {
        public string CompoundName { get; set; }
        public bool Passed { get; set; }
        public double AbsorbanceIndex { get; set; }
        public StandardConfirmationError FailureReason { get; set; }
        public IList<(double Wavelength, double Response)> SampleSpectrum { get; set; }
        public IList<(double Wavelength, double Response)> StandardSpectrum { get; set; }
        public IList<(double Wavelength, double Response)> BaselineSpectrumOfSample { get; set; }
        public IList<(double Wavelength, double Response)> BaselineSpectrumOfStandard { get; set; }
        public IList<(double Wavelength, double Response)> RatioSpectrum { get; set; }
    }
}