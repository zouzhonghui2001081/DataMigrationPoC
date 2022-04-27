using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class RIChannelMetaData : IRIChannelMetaData, IEquatable<IRIChannelMetaData>
    {
        public string Name { get; set; }

        public RIChannelMetaData()
        {
            
        }
        public RIChannelMetaData(string responseUnit)
        {
            ResponseUnit = responseUnit;
        }

        public bool Equals(IRIChannelMetaData other)
        {
            return string.Equals(ResponseUnit, other.ResponseUnit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RIChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            return (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
    }
}