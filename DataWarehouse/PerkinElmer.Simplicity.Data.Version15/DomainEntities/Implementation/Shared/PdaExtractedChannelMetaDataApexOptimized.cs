using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
	public class PdaExtractedChannelMetaDataApexOptimized : IPdaExtractedChannelMetaDataApexOptimized, IEquatable<IPdaExtractedChannelMetaDataApexOptimized>
	{
		public string ResponseUnit { get; set; }
		public double DefaultMinYScale { get; set; }
		public double DefaultMaxYScale { get; set; }
		public double MinValidYValue { get; set; }
		public double MaxValidYValue { get; set; }
		public double SamplingRateInMilliseconds { get; set; }

		public Guid BaseBrChannelGuid { get; set; }
        public double BaseBrChannelWavelength { get; set; }
        public double WavelengthBandwidth { get; set; }
		public bool UseReference { get; set; }
		public double ReferenceWavelength { get; set; }
		public double ReferenceWavelengthBandwidth { get; set; }


		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool Equals(IPdaExtractedChannelMetaDataApexOptimized other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ResponseUnit == other.ResponseUnit && 
			       DefaultMinYScale.Equals(other.DefaultMinYScale) && 
			       DefaultMaxYScale.Equals(other.DefaultMaxYScale) && 
			       MinValidYValue.Equals(other.MinValidYValue) && 
			       MaxValidYValue.Equals(other.MaxValidYValue) && 
			       BaseBrChannelGuid.Equals(other.BaseBrChannelGuid) &&
                   BaseBrChannelWavelength.Equals(other.BaseBrChannelWavelength) &&
                   WavelengthBandwidth.Equals(other.WavelengthBandwidth) && 
			       UseReference == other.UseReference && 
			       ReferenceWavelength.Equals(other.ReferenceWavelength) && 
			       ReferenceWavelengthBandwidth.Equals(other.ReferenceWavelengthBandwidth);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			
			if (obj.GetType() != this.GetType()) return false;
			
			return Equals((PdaExtractedChannelMetaDataApexOptimized) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ DefaultMinYScale.GetHashCode();
				hashCode = (hashCode * 397) ^ DefaultMaxYScale.GetHashCode();
				hashCode = (hashCode * 397) ^ MinValidYValue.GetHashCode();
				hashCode = (hashCode * 397) ^ MaxValidYValue.GetHashCode();
				hashCode = (hashCode * 397) ^ BaseBrChannelGuid.GetHashCode();
                hashCode = (hashCode * 397) ^ BaseBrChannelWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ WavelengthBandwidth.GetHashCode();
				hashCode = (hashCode * 397) ^ UseReference.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ ReferenceWavelengthBandwidth.GetHashCode();
				return hashCode;
			}
		}
	}
}