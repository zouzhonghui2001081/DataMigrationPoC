using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing.Spectral
{
	public class ApexOptimizedParameters : SpectralParametersBase
	{
        public double AnalyticalBandwidth { get; set; }
        public double ReferenceWavelength { get; set; }
        public double ReferenceBandwidth { get; set; }
	    public bool UseReference { get; set; }

        public IList<(double StartTime, double ApexTime, double EndTime)> Peaks { get; set; }
	}
}