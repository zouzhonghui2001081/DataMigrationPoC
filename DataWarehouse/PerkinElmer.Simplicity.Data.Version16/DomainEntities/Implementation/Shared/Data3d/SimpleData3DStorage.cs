using System;
using System.Collections.Generic;
using System.Linq;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public class SimpleData3DStorage : IData3DStorage
    {
        private readonly IList<IList<double>> _data;

        public SimpleData3DStorage(IList<IList<double>> data)
        {
            _data = data;
        }

        public IList<double> GetSpectrum(int timeIndex)
        {
            return _data[timeIndex];
        }

        public IList<double> GetChromatogram(int wlIndex)
        {
            return _data.Select(x => x[wlIndex]).ToList();
        }

        public IList<IList<double>> GetSpectra(int[] timeIndices)
        {
            return Array.ConvertAll(timeIndices, GetSpectrum);
        }

        public IList<IList<double>> GetChromatograms(int[] wlIndices)
        {
            return Array.ConvertAll(wlIndices, GetChromatogram);
        }

        public IList<double> GetMaximumIntensityChromatogram()
        {
            return _data.Select(x => x.Max()).ToList();
        }

        public IList<double> GetAverageSpectrum(int startIndex, int endIndex)
        {
            var timeIndices = Enumerable.Range(startIndex, endIndex - startIndex + 1).ToArray();
            var spectra = GetSpectra(timeIndices);
            return SpectrumUtility.AverageSpectrum(spectra);
        }

        public void Reset()
        {
        }
    }
}