using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared
{
	public class PdaExtractedChannelMetaDataMic : IPdaExtractedChannelMetaDataMic, IEquatable<IPdaExtractedChannelMetaDataMic>
	{
		public string ResponseUnit { get; set; }
		public double DefaultMinYScale { get; set; }
		public double DefaultMaxYScale { get; set; }
		public double MinValidYValue { get; set; }
		public double MaxValidYValue { get; set; }
		public double SamplingRateInMilliseconds { get; set; }

		public object Clone()
		{
			return MemberwiseClone();
		}

		public bool Equals(IPdaExtractedChannelMetaDataMic other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ResponseUnit == other.ResponseUnit && 
			       DefaultMinYScale.Equals(other.DefaultMinYScale) && 
			       DefaultMaxYScale.Equals(other.DefaultMaxYScale) && 
			       MinValidYValue.Equals(other.MinValidYValue) && 
			       MaxValidYValue.Equals(other.MaxValidYValue);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			
			if (obj.GetType() != this.GetType()) return false;
			
			return Equals((PdaExtractedChannelMetaDataMic) obj);
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
				return hashCode;
			}
		}
	}
}