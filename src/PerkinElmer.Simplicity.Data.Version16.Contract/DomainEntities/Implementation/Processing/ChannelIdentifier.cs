using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
    public class ChannelIdentifier : IChannelIdentifier1
    {
        public ChannelIdentifier()
        {
        }

        public ChannelIdentifier(int channelIndex, bool auxiliary)
        {
            ChannelIndex = channelIndex;
            Auxiliary = auxiliary;
        }

        public int ChannelIndex { get; set; }
        public bool Auxiliary { get; set; }

        public bool Equals(IChannelIdentifier1 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return ChannelIndex == other.ChannelIndex && Auxiliary == other.Auxiliary;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((IChannelIdentifier1)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ChannelIndex * 397) ^ Auxiliary.GetHashCode();
            }
        }

        public object Clone()
        {
            ChannelIdentifier channelIdentifier = new ChannelIdentifier
            {
                Auxiliary = Auxiliary, ChannelIndex = ChannelIndex
            };
            return channelIdentifier;
        }
    }
}