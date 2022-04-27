using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing.SpectralAbsorbanceRatio
{
    public class AbsorbanceRatioResult : IAbsorbanceRatioResult
    {
        public double? AbsorbanceRatio { get; set; }
        public AbsorbanceRatioError Error { get; set; }
        public double AbsorbanceAtA { get; set; }
        public double AbsorbanceAtB { get; set; }
        public IList<(double Wavelength, double Response)> Spectrum { get; set; }
        public IList<(double Wavelength, double Response)> BaselineSpectrum { get; set; }
    }
}
