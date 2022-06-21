using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public interface IData3d
	{
		IList<double> Times { get; set; }

		IList<double> Wavelengths { get; set; }

		IList<double> GetSpectrum(int timeIndex);

		IList<double> GetChromatogram(int wlIndex);

		IList<IList<double>> GetSpectra(int[] timeIndices);

		IList<IList<double>> GetChromatograms(int[] wlIndices);

        IList<double> GetAverageSpectrum(int startIndex, int endIndex);

		IList<double> GetMaximumIntensityChromatogram();
		bool IsValid();

		void InitWith(IList<double> times, IList<double> wavelengths, IList<IList<double>> intensities);

        void Reset();
    }
}