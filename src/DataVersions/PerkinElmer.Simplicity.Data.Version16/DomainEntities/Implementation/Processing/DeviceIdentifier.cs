using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class DeviceIdentifier : IDeviceIdentifier
    {
        public DeviceIdentifier()
        {
        }

        public DeviceIdentifier(string deviceClass, int deviceIndex)
        {
            DeviceClass = deviceClass;
            DeviceIndex = deviceIndex;
        }

        public string DeviceClass { get; set; }
        public int DeviceIndex { get; set; }

        public bool Equals(IDeviceIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(DeviceClass, other.DeviceClass) && DeviceIndex == other.DeviceIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((DeviceIdentifier)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DeviceClass != null ? DeviceClass.GetHashCode() : 0) * 397) ^ DeviceIndex;
            }
        }

        public object Clone()
        {
            DeviceIdentifier deviceIdentifier = new DeviceIdentifier
            {
                DeviceClass = DeviceClass, DeviceIndex = DeviceIndex
            };
            return deviceIdentifier;
        }
    }
}