using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
	public class PdaExtractedChannelMetaDataSimple : IPdaExtractedChannelMetaDataSimple
	{
		public string ResponseUnit { get; set;  }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public double Wavelength { get; set; }
		public double WavelengthBandwidth { get; set; }
		public bool UseReference { get; set; }
		public double ReferenceWavelength { get; set; }
		public double ReferenceWavelengthBandwidth { get; set; }
		public bool IsApexOptimized { get; set; }

		public bool Equals(IPdaExtractedChannelMetaDataSimple other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return string.Equals(ResponseUnit, other.ResponseUnit) 
			       && Wavelength.Equals(other.Wavelength) 
			       && WavelengthBandwidth.Equals(other.WavelengthBandwidth) 
			       && UseReference == other.UseReference 
			       && ReferenceWavelength.Equals(other.ReferenceWavelength) 
			       && ReferenceWavelengthBandwidth.Equals(other.ReferenceWavelengthBandwidth)
			       && IsApexOptimized.Equals(other.IsApexOptimized);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;

			return Equals((PdaExtractedChannelMetaDataSimple) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Wavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ WavelengthBandwidth.GetHashCode();
				hashCode = (hashCode * 397) ^ UseReference.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceWavelengthBandwidth.GetHashCode();
				hashCode = (hashCode * 397) ^ IsApexOptimized.GetHashCode();
				return hashCode;				
			}
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}
	}
}
