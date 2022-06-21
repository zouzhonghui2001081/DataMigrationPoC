using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
	public class PdaChannelMetaData : IPdaChannelMetaData, IEquatable<IPdaChannelMetaData>
	{
		public PdaChannelMetaData()
		{

		}
		public PdaChannelMetaData(double startWavelength, double endWavelength, bool lowResolution, string responseUnit)
		{
			StartWavelength = startWavelength;
			EndWavelength = endWavelength;
			LowResolution = lowResolution;
			ResponseUnit = responseUnit;
		}

		public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public double StartWavelength { get; set; }
		public double EndWavelength { get; set; }
		public bool LowResolution { get; set; }

		public bool Equals(IPdaChannelMetaData other)
		{
			return string.Equals(ResponseUnit, other.ResponseUnit) && 
			       StartWavelength.Equals(other.StartWavelength) &&
			       EndWavelength.Equals(other.EndWavelength) &&
			       LowResolution == other.LowResolution;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((IPdaChannelMetaData) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ StartWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ EndWavelength.GetHashCode();
				hashCode = (hashCode * 397) ^ LowResolution.GetHashCode();
				return hashCode;				
			}
		}
	}
}