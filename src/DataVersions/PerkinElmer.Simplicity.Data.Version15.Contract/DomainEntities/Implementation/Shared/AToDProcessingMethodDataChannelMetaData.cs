using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
    internal sealed class AToDProcessingMethodDataChannelMetaData : IProcessingMethodDataChannelMetaData, IEquatable<AToDProcessingMethodDataChannelMetaData>
    {
        public bool Equals(AToDProcessingMethodDataChannelMetaData other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is AToDProcessingMethodDataChannelMetaData other && Equals(other);
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