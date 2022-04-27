using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    public class GCChannelMetaData : IGCChannelMetaData, IEquatable<GCChannelMetaData>
    {
        public GCChannelMetaData()
        {

        }

        public GCChannelMetaData(string detectorType, string responseUnit)
        {
            DetectorType = detectorType;
            ResponseUnit = responseUnit;
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public string DetectorType { get; set; }

      

        public static bool operator ==(GCChannelMetaData left, GCChannelMetaData right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GCChannelMetaData left, GCChannelMetaData right)
        {
            return !Equals(left, right);
        }

        public bool Equals(GCChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ResponseUnit, other.ResponseUnit) && string.Equals(DetectorType, other.DetectorType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GCChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ResponseUnit != null ? ResponseUnit.GetHashCode() : 0) * 397) ^ (DetectorType != null ? DetectorType.GetHashCode() : 0);
            }
        }
    }
}