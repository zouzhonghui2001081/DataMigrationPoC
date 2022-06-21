using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface ISuitabilityResult : ICloneable
    {
	    Guid PeakGuid { get; set; }
	    Guid CompoundGuid { get; set; }
	    string PeakName { get; set; }
	    double PeakRetentionTime { get; set; } // This is 'technical' field provided for sorting of the rows on UI
	    SuitabilityParameterResult RetentionTime { get; set; }
	    SuitabilityParameterResult Area { get; set; }
	    SuitabilityParameterResult Height { get; set; }
	    SuitabilityParameterResult TheoreticalPlatesN { get; set; }
	    SuitabilityParameterResult TheoreticalPlatesNTan { get; set; }
	    SuitabilityParameterResult TailingFactorSymmetry { get; set; }
	    SuitabilityParameterResult RelativeRetention { get; set; } // aka Alpha
	    SuitabilityParameterResult RelativeRetentionTime { get; set; } // aka RRT
	    SuitabilityParameterResult CapacityFactorKPrime { get; set; }
	    SuitabilityParameterResult Resolution { get; set; }
	    SuitabilityParameterResult UspResolution { get; set; }
	    
	    SuitabilityParameterResult SignalToNoise { get; set; }
	    double? Noise { get; set; }
	    double? NoiseStart { get; set; }
	    double? NoiseGapStart { get; set; }
	    double? NoiseGapEnd { get; set; }
	    double? NoiseEnd { get; set; }

	    SuitabilityParameterResult PeakWidthAtBase { get; set; }
	    SuitabilityParameterResult PeakWidthAt5Pct { get; set; }
	    SuitabilityParameterResult PeakWidthAt10Pct { get; set; }
	    SuitabilityParameterResult PeakWidthAt50Pct { get; set; }

        bool? SstFlag { get; set; }
	}
}
