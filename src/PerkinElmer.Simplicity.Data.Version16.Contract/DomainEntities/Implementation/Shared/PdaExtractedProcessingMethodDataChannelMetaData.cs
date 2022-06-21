using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    internal sealed class PdaExtractedProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<PdaExtractedProcessingMethodDataChannelMetaData>
    {
	    public readonly double WavelengthInNanometers;
	    public readonly bool Programmed;
        public PdaExtractedProcessingMethodDataChannelMetaData(double wavelengthInNanometers, bool programmed)
        {
            WavelengthInNanometers = wavelengthInNanometers;
            Programmed = programmed;
           
        }
        
        public bool Equals(PdaExtractedProcessingMethodDataChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return WavelengthInNanometers.Equals(other.WavelengthInNanometers) && Programmed == other.Programmed;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PdaExtractedProcessingMethodDataChannelMetaData other && Equals(other);
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