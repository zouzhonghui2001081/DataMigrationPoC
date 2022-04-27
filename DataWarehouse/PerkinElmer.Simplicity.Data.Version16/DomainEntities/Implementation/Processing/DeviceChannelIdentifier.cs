using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class DeviceChannelIdentifier : IDeviceChannelIdentifier
    {
	    public DeviceChannelIdentifier()
	    {
	    }

	    public DeviceChannelIdentifier(IDeviceIdentifier deviceIdentifier, IChannelIdentifier1 channelIdentifier)
        {
            DeviceIdentifier = deviceIdentifier;
            ChannelIdentifier = channelIdentifier;
        }

	    public IDeviceIdentifier DeviceIdentifier { get; set; }
        public IChannelIdentifier1 ChannelIdentifier { get; set; }

        public bool Equals(IDeviceChannelIdentifier other)
        {
	        if (ReferenceEquals(null, other)) return false;
	        if (ReferenceEquals(this, other)) return true;

	        return Equals(DeviceIdentifier, other.DeviceIdentifier) && Equals(ChannelIdentifier, other.ChannelIdentifier);
        }

        public override bool Equals(object obj)
        {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;

	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((IDeviceChannelIdentifier) obj);
        }

        public override int GetHashCode()
        {
	        unchecked
	        {
		        return ((DeviceIdentifier != null ? DeviceIdentifier.GetHashCode() : 0) * 397) ^ (ChannelIdentifier != null ? ChannelIdentifier.GetHashCode() : 0);
	        }
        }

        public object Clone()
        {
            DeviceChannelIdentifier deviceChannelIdentifier = new DeviceChannelIdentifier
            {
                ChannelIdentifier = (IChannelIdentifier1) ChannelIdentifier.Clone(),
                DeviceIdentifier = (IDeviceIdentifier) DeviceIdentifier.Clone()
            };
            return deviceChannelIdentifier;
        }
    }
}