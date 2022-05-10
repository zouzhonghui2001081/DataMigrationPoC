using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface ISuitabilityLimits : ICloneable, IEquatable<ISuitabilityLimits>
    {
		SuitabilityLimit Area { get; set; }
		SuitabilityLimit Height { get; set; }
		SuitabilityLimit NTan { get; set; }
		SuitabilityLimit TheoreticalPlatesN { get; set; } // renamed NFoley to TheoreticalPlatesN
        SuitabilityLimit TheoreticalPlatesNFoleyDorsey { get; set; } //Added TheoreticalPlatesNFoleyDorsey Property
        SuitabilityLimit TailingFactorSymmetry { get; set; }
		SuitabilityLimit UspResolution { get; set; }
		SuitabilityLimit KPrime { get; set; }
		SuitabilityLimit Resolution { get; set; }
		SuitabilityLimit Alpha { get; set; }
		SuitabilityLimit SignalToNoise { get; set; }
		SuitabilityLimit PeakWidth { get; set; }
	}
}