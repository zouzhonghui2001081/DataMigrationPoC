using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
	public class PdaExtractedChannelMetaDataProgrammed : IPdaExtractedChannelMetaDataProgrammed, IEquatable<IPdaExtractedChannelMetaDataProgrammed>
	{
		public string ResponseUnit { get; set; }
		public double DefaultMinYScale { get; set; }
		public double DefaultMaxYScale { get; set; }
		public double MinValidYValue { get; set; }
		public double MaxValidYValue { get; set; }
		public double SamplingRateInMilliseconds { get; set; }

		public IList<IPdaExtractionSegment> ExtractionSegments { get; set; }


		public object Clone()
		{
			var clone = (PdaExtractedChannelMetaDataProgrammed) MemberwiseClone();
			clone.ExtractionSegments = this.ExtractionSegments.Select(s => (IPdaExtractionSegment) s.Clone()).ToList();
			return clone;
		}

		public bool Equals(IPdaExtractedChannelMetaDataProgrammed other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return ResponseUnit == other.ResponseUnit &&
			       DefaultMinYScale.Equals(other.DefaultMinYScale) &&
			       DefaultMaxYScale.Equals(other.DefaultMaxYScale) &&
			       MinValidYValue.Equals(other.MinValidYValue) &&
			       MaxValidYValue.Equals(other.MaxValidYValue) &&
			       ExtractionSegments.Count == other.ExtractionSegments.Count &&
			       ExtractionSegments.Zip(other.ExtractionSegments, (t, o) => t.Equals(o)).All(v => v);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((PdaExtractedChannelMetaDataProgrammed) obj);
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
                int extractionSegmentsHashCode = 0;
                if (ExtractionSegments != null)
                {
                    foreach (var extractionSegment in ExtractionSegments)
                    {
                        extractionSegmentsHashCode = extractionSegmentsHashCode + extractionSegment.GetHashCode();
                    }
                }
                hashCode = (hashCode * 397) ^ extractionSegmentsHashCode;
                return hashCode;
            }
        }
    }
}