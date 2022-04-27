using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    public class DeviceChannelDescriptor : IDeviceChannelDescriptor
    {
        //making this class public to support dummy data in Realtime class of livedatachromatogram

        public DeviceChannelDescriptor()
        {
        }

        public DeviceChannelDescriptor(IDeviceChannelIdentifier deviceChannelIdentifier,
            IChannelMetaData channelMetaData, DeviceChannelType deviceChannelType)
        {
            DeviceChannelIdentifier = deviceChannelIdentifier;
            DeviceChannelType = deviceChannelType;
            MetaData = channelMetaData;
        }

        public IDeviceChannelIdentifier DeviceChannelIdentifier { get; set; }
        public DeviceChannelType DeviceChannelType { get; set; }
        public IChannelMetaData MetaData { get; set; }

        public bool Equals(IDeviceChannelDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return DeviceChannelIdentifier.Equals(other.DeviceChannelIdentifier) && DeviceChannelType.Equals(other.DeviceChannelType) &&
                   MetaData.Equals(other.MetaData);
            // we require to add comparison for metadata, because metadata has information for which type of detector is present for GC
            // if dont compare then it consider FID1 same as AtoD1 detector.
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((IDeviceChannelDescriptor)obj);
        }

        public override int GetHashCode()
        {
            return (DeviceChannelIdentifier != null ? DeviceChannelIdentifier.GetHashCode() : 0);
        }

        public object Clone()
        {
            DeviceChannelDescriptor deviceChannelDescriptor = new DeviceChannelDescriptor
            {
                DeviceChannelIdentifier = (IDeviceChannelIdentifier) DeviceChannelIdentifier.Clone(),
                DeviceChannelType = DeviceChannelType,
                MetaData = MetaData //To Do Perform Deep Copy
            };
            return deviceChannelDescriptor;
        }
    }
}