using System.Collections.Generic;
using System.Linq;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d
{
    public static class SpectrumUtility
    {
        public static IList<double> AverageSpectrum(IList<IList<double>> spectra)
        {
            var wavelengthCount = spectra[0].Count;
            var averageSpectrum = new double[wavelengthCount];

            for (int wavelengthIndex = 0; wavelengthIndex < wavelengthCount; wavelengthIndex++)
            {
                averageSpectrum[wavelengthIndex] = spectra.Sum(s => s[wavelengthIndex]) / spectra.Count;
            }

            return averageSpectrum;
        }
    }
}
