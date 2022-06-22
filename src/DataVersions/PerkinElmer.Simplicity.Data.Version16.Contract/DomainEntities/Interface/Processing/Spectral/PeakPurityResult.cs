using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral
{
	public class PeakPurityResult
	{
        public PeakPurityError Error { get; set; }
		public double PurityValue { get; set; }
        public int NumberOfValidPoints { get; set; }
	    public double UpslopeTime { get; set; }
	    public double DownslopeTime { get; set; }
        public IList<(double Wavelength, double Response)> UpslopeSpectrum { get; set; }
	    public IList<(double Wavelength, double Response)> DownslopeSpectrum { get; set; }
	    public IList<(double Wavelength, double Response)> BaselineSpectrum { get; set; }
	    public IList<(double Wavelength, double Response)> RatioSpectrum { get; set; }
    }
}