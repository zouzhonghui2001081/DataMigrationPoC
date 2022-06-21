using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
    internal sealed class UVProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<UVProcessingMethodDataChannelMetaData>
    {
	    public readonly double WavelengthInNanometers;
	    public readonly bool Programmed;
		public UVProcessingMethodDataChannelMetaData(double wavelengthInNanometers, bool programmed)
        {
            WavelengthInNanometers = wavelengthInNanometers;
            Programmed = programmed;
		}
        public bool Equals(UVProcessingMethodDataChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return WavelengthInNanometers.Equals(other.WavelengthInNanometers) && Programmed == other.Programmed;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UVProcessingMethodDataChannelMetaData other && Equals(other);
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
    }
}