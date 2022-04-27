using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class UVChannelMetaData : IUVChannelMetaData, IEquatable<IUVChannelMetaData>
    {
        public bool Equals(IUVChannelMetaData other)
        {
            return string.Equals(ResponseUnit, other.ResponseUnit) && WavelengthInNanometers.Equals(other.WavelengthInNanometers) && Programmed == other.Programmed;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UVChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ WavelengthInNanometers.GetHashCode();
                hashCode = (hashCode * 397) ^ Programmed.GetHashCode();
                return hashCode;
            }
        }

        public UVChannelMetaData()
        {

        }


        public UVChannelMetaData(double wavelengthInNanometers, bool programmed, string responseUnit)
        {
            WavelengthInNanometers = wavelengthInNanometers;
            Programmed = programmed;
            ResponseUnit = responseUnit;
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public double WavelengthInNanometers { get; set; }
        public bool Programmed { get; set; }
    }
}