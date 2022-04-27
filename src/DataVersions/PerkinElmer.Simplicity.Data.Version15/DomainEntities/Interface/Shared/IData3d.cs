using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared
{
	public interface IData3d
	{
		IList<double> Times { get; set; } // Time points for spectra
		IList<double> Wavelengths { get; set; } // Wavelengths points - common for all spectra
		IList<IList<double>> Intensities { get; set; } // List of spectra. Each spectrum is represented by list of its intensities.

	}
}