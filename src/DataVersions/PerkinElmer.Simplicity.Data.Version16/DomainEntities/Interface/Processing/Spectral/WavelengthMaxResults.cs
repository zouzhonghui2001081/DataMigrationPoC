using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing.Spectral
{
	public class WavelengthMaxResult
	{
        public WavelengthMaxResult(WavelengthMaxError error, double wavelength, double absorbance)
        {
	        Error = error;
	        Wavelength = wavelength;
            Absorbance = absorbance;
        }

        public WavelengthMaxError Error { get; }
        public double Wavelength { get; }
	    public double Absorbance { get; }
	    public IList<(double Wavelength, double Response)> ApexSpectrum { get; set; }
	    public IList<(double Wavelength, double Response)> BaselineSpectrum { get; set; }
    }
}