using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
    internal sealed class PdaApexOptimizedProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<PdaApexOptimizedProcessingMethodDataChannelMetaData>
    {
        private readonly double _wavelengthInNanometers;
        public double WavelengthInNanometers { get; }
        public readonly bool Programmed;
        public PdaApexOptimizedProcessingMethodDataChannelMetaData(double wavelengthInNanometers, bool programmed)
        {
            _wavelengthInNanometers = wavelengthInNanometers;
            WavelengthInNanometers = _wavelengthInNanometers;
            Programmed = programmed;
        }

        public bool Equals(PdaApexOptimizedProcessingMethodDataChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _wavelengthInNanometers.Equals(other._wavelengthInNanometers);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is PdaApexOptimizedProcessingMethodDataChannelMetaData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _wavelengthInNanometers.GetHashCode();
        }

        public string GetDisplayName()
        {
            var displayName = $"{_wavelengthInNanometers}";
            return displayName + (Programmed ? "-P" : "") + "-ApexOptimized";
        }
    }
}