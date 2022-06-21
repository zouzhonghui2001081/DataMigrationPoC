using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    internal sealed class GCProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<GCProcessingMethodDataChannelMetaData>
    {
	    
		public readonly string DetectorType;
        public GCProcessingMethodDataChannelMetaData(string detectorType)
        {
            DetectorType = detectorType;

        }
        public bool Equals(GCProcessingMethodDataChannelMetaData other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DetectorType == other.DetectorType;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GCProcessingMethodDataChannelMetaData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (DetectorType != null ? DetectorType.GetHashCode() : 0);
        }

        public string GetDisplayName()
        {
            return DetectorType;
        }
    }
}