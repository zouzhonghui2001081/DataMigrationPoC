using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d
{
    public interface IData3DStorage
    {
        IList<double> GetSpectrum(int index);
        IList<IList<double>> GetSpectra(int[] indices);
        IList<double> GetChromatogram(int index);
        IList<IList<double>> GetChromatograms(int[] indices);
        IList<double> GetMaximumIntensityChromatogram();
        IList<double> GetAverageSpectrum(int startIndex, int endIndex);
        void Reset();
    }
}