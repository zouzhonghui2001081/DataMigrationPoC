using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface ISuitabilityLimits : ICloneable, IEquatable<ISuitabilityLimits>
    {
		SuitabilityLimit Area { get; set; }
		SuitabilityLimit Height { get; set; }
		SuitabilityLimit NTan { get; set; }
		SuitabilityLimit NFoley { get; set; }
		SuitabilityLimit TailingFactorSymmetry { get; set; }
		SuitabilityLimit UspResolution { get; set; }
		SuitabilityLimit KPrime { get; set; }
		SuitabilityLimit Resolution { get; set; }
		SuitabilityLimit Alpha { get; set; }
		SuitabilityLimit SignalToNoise { get; set; }
		SuitabilityLimit PeakWidth { get; set; }
	}
}