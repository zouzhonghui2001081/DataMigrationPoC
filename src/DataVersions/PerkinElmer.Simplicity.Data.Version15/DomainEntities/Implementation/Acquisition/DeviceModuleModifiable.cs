using PerkinElmer.Acquisition.Devices;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using DeviceModuleCompleteId = PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition.DeviceModuleCompleteId;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    internal class DeviceModuleModifiable : IDeviceModuleModifiable
    {
        public DeviceModuleCompleteId Id { get; private set; }
        public string Name { get; private set; }

        public DeviceType DeviceType => _deviceType;

        private DeviceType _deviceType;
        public void Set(DeviceModuleCompleteId deviceModuleCompleteId, string name, DeviceType deviceType)
        {
            Id = deviceModuleCompleteId;
            Name = name;
            _deviceType = deviceType;
        }

        protected bool Equals(DeviceModuleModifiable other)
        {
            return Id.Equals(other.Id) && string.Equals(Name, other.Name) && DeviceType.Equals(other.DeviceType);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceModuleModifiable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0) ^ (DeviceType.GetHashCode());
            }
        }

        public bool Equals(IDeviceModule other)
        {
            return Equals((object) other);
        }
    }
}