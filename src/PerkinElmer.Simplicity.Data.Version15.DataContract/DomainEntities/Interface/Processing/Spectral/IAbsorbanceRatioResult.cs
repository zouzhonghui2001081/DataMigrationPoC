using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing.Spectral
{
    public interface IAbsorbanceRatioResult
    {
        double? AbsorbanceRatio { get; set; }
        AbsorbanceRatioError Error { get; set; }
        double AbsorbanceAtA { get; set; }
        double AbsorbanceAtB { get; set; }
        IList<(double Wavelength, double Response)> Spectrum { get; set; }
        IList<(double Wavelength, double Response)> BaselineSpectrum { get; set; }
    }
}