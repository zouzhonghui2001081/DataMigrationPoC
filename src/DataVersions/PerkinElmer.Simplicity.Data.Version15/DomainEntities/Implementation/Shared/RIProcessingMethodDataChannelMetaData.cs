using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared
{
    internal sealed class RiProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<RiProcessingMethodDataChannelMetaData>
    {
        public bool Equals(RiProcessingMethodDataChannelMetaData other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is RiProcessingMethodDataChannelMetaData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public string GetDisplayName()
        {
            return string.Empty;
        }
    }
}