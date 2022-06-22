using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
	public class PdaExtractionSegment : IPdaExtractionSegment, IEquatable<IPdaExtractionSegment>
	{
		public double StartTime { get; set; }
		public double Wavelength { get; set; }
		public double Bandwidth { get; set; }
		public bool UseReference { get; set; }
		public double ReferenceWavelength { get; set; }
		public double ReferenceBandwidth { get; set; }
		public bool AutoZero { get; set; }

		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool Equals(IPdaExtractionSegment other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			
			return StartTime.Equals(other.StartTime) && 
			       Wavelength.Equals(other.Wavelength) && 
			       Bandwidth.Equals(other.Bandwidth) && 
			       UseReference == other.UseReference && 
			       ReferenceWavelength.Equals(other.ReferenceWavelength) && 
			       ReferenceBandwidth.Equals(other.ReferenceBandwidth) && 
			       AutoZero == other.AutoZero;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			
			if (obj.GetType() != this.GetType()) return false;
			
			return Equals((PdaExtractionSegment) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = StartTime.GetHashCode();
				hashCode = (hashCode * 397) ^ Wavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ Bandwidth.GetHashCode();
				hashCode = (hashCode * 397) ^ UseReference.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceBandwidth.GetHashCode();
				hashCode = (hashCode * 397) ^ AutoZero.GetHashCode();
				return hashCode;
			}
		}
	}
}