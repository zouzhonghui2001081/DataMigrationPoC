using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral
{
	public abstract class SpectralParametersBase
	{
	    public bool AutoThreshold { get; set; }
	    public double MinimumThreshold { get; set; }
	    public bool UseWavelengthLimits { get; set; }
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }
	    public bool BaselineCorrected { get; set; }
	    public IList<(double StartTime, double EndTime)> BaselineCorrectionTimes { get; set; }
    }
}