using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
    internal sealed class MultiUvProcessingMethodDataChannelMetaData: IProcessingMethodDataChannelMetaData, IEquatable<MultiUvProcessingMethodDataChannelMetaData>
    {
        public readonly double WavelengthInNanometers;
        public readonly bool Programmed;

        public MultiUvProcessingMethodDataChannelMetaData(double wavelengthInNanometers, bool programmed)
        {
            WavelengthInNanometers = wavelengthInNanometers;
            Programmed = programmed;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is MultiUvProcessingMethodDataChannelMetaData other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (WavelengthInNanometers.GetHashCode() * 397) ^ Programmed.GetHashCode();
            }
        }

        public string GetDisplayName()
        {
            var displayName = $"{WavelengthInNanometers}";
            return Programmed ? $"{displayName}-P" : displayName;
        }

        public bool Equals(MultiUvProcessingMethodDataChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return WavelengthInNanometers.Equals(other.WavelengthInNanometers) && Programmed == other.Programmed;
        }
    }
}
