using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared.Data3d
{
    public class SpectraCache3DStorage : IData3DStorage
    {
        private readonly IData3DStorage _storage;

        private readonly ConcurrentDictionary<string, IList<double>> _cache = new ConcurrentDictionary<string, IList<double>>();


        public SpectraCache3DStorage(IData3DStorage storage)
        {
            _storage = storage;
        }

        public IList<double> GetChromatogram(int index)
        {
            return _storage.GetChromatogram(index);
        }

        public IList<IList<double>> GetChromatograms(int[] indices)
        {
            return _storage.GetChromatograms(indices);
        }

        public IList<double> GetMaximumIntensityChromatogram()
        {
            return _storage.GetMaximumIntensityChromatogram();
        }

        public IList<double> GetAverageSpectrum(int startIndex, int endIndex)
        {
            return _cache.GetOrAdd($"{startIndex}-{endIndex}", _ => _storage.GetAverageSpectrum(startIndex, endIndex));
        }

        public void Reset()
        {
            _cache.Clear();
        }

        public IList<double> GetSpectrum(int index)
        {
            return _cache.GetOrAdd(index.ToString(), _ => _storage.GetSpectrum(index));
        }

        public IList<IList<double>> GetSpectra(int[] indices)
        {
            var ret = new IList<double>[indices.Length];
            for (var i = 0; i < indices.Length; i++)
            {
                ret[i] = GetSpectrum(indices[i]);
            }

            return ret;
        }

    }
}