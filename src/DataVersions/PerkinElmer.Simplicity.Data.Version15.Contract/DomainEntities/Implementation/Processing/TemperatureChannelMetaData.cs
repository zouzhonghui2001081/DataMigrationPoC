using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    public class TemperatureChannelMetaData : ITemperatureChannelMetaData, IEquatable<ITemperatureChannelMetaData>
    {
        public TemperatureChannelMetaData()
        {
        }

        public TemperatureChannelMetaData(string name, string responseUnit)
        {
            ResponseUnit = responseUnit;
            Name = name;
        }

        public bool Equals(ITemperatureChannelMetaData other)
        {
            return (string.Equals(ResponseUnit, other.ResponseUnit) && string.Equals(Name, other.Name));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TemperatureChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            return ResponseUnit.GetHashCode();
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public string Name { get; set; }
    }
}