using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{ //making this class public to support dummy data in Realtime class of livedatachromatogram
    public class FLChannelMetaData : IFLChannelMetaData, IEquatable<IFLChannelMetaData>
    {

        public FLChannelMetaData()
        {

        }

        public FLChannelMetaData(double excitationInNanometers, double emissionInNanometers, bool programmed,
            string responseUnit)
        {
            ExcitationInNanometers = excitationInNanometers;
            EmissionInNanometers = emissionInNanometers;
            Programmed = programmed;
            ResponseUnit = responseUnit;
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public double ExcitationInNanometers { get; set; }
        public double EmissionInNanometers { get; set; }
        public bool Programmed { get; set; }

        public bool Equals(IFLChannelMetaData other)
        {
            return string.Equals(ResponseUnit, other.ResponseUnit) && ExcitationInNanometers.Equals(other.ExcitationInNanometers) && EmissionInNanometers.Equals(other.EmissionInNanometers) && Programmed == other.Programmed;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FLChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ResponseUnit != null ? ResponseUnit.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ExcitationInNanometers.GetHashCode();
                hashCode = (hashCode * 397) ^ EmissionInNanometers.GetHashCode();
                hashCode = (hashCode * 397) ^ Programmed.GetHashCode();
                return hashCode;
            }
        }
    }
}