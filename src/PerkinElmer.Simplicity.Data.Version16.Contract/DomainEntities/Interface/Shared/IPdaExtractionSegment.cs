using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public interface IPdaExtractionSegment : IEquatable<IPdaExtractionSegment>, ICloneable
	{
		double StartTime { get; set; } // In seconds
		double Wavelength { get; set; }
		double Bandwidth { get; set; }
		bool UseReference { get; set; }
		double ReferenceWavelength { get; set; }
		double ReferenceBandwidth { get; set; }
		bool AutoZero { get; set; }
	}
}