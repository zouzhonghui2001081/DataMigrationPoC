using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using DeviceModuleCompleteId = PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition.DeviceModuleCompleteId;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    internal class DeviceModuleDetailsModifiable : IDeviceModuleDetailsModifiable
    {
        public DeviceModuleCompleteId Id { get; private set; }
        public string Name { get; private set; }
        private DeviceType _deviceType;
        public DeviceType DeviceType => _deviceType;
        public bool SettingsUserInterfaceSupported { get; private set; }
        public bool Simulation { get; private set; }
        public bool CommunicationTestedSuccessfully { get; set; }
        public IDeviceInformation DeviceInformation { get; set; }

        protected bool Equals(DeviceModuleDetailsModifiable other)
        {
            return Id.Equals(other.Id) && string.Equals(Name, other.Name) && DeviceType == other.DeviceType &&
                   SettingsUserInterfaceSupported == other.SettingsUserInterfaceSupported &&
                   Simulation == other.Simulation &&
                   CommunicationTestedSuccessfully == other.CommunicationTestedSuccessfully &&
                   Equals(DeviceInformation, other.DeviceInformation);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceModuleDetailsModifiable) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DeviceType.GetHashCode();
                hashCode = (hashCode * 397) ^ SettingsUserInterfaceSupported.GetHashCode();
                hashCode = (hashCode * 397) ^ Simulation.GetHashCode();
                hashCode = (hashCode * 397) ^ CommunicationTestedSuccessfully.GetHashCode();
                hashCode = (hashCode * 397) ^ (DeviceInformation != null ? DeviceInformation.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(IDeviceModule other)
        {
            return Equals((object) other);
        }

        public void Set(IDeviceModuleDetails deviceModuleDetails)
        {
            Id = deviceModuleDetails.Id;
            Name = deviceModuleDetails.Name;
            _deviceType = deviceModuleDetails.DeviceType;
            SettingsUserInterfaceSupported = deviceModuleDetails.SettingsUserInterfaceSupported;
            CommunicationTestedSuccessfully = deviceModuleDetails.CommunicationTestedSuccessfully;
            Simulation = deviceModuleDetails.Simulation;
            if (deviceModuleDetails.DeviceInformation == null) DeviceInformation = null;
            else
            {
                DeviceInformation = new DeviceInformation()
                {
                    FirmwareVersion = deviceModuleDetails.DeviceInformation.FirmwareVersion,
                    UniqueIdentifier = deviceModuleDetails.DeviceInformation.UniqueIdentifier,
                    ModelName = deviceModuleDetails.DeviceInformation.ModelName,
                    SerialNumber = deviceModuleDetails.DeviceInformation.SerialNumber,
                    InterfaceAddress = deviceModuleDetails.DeviceInformation.InterfaceAddress
                };
            }

        }

        public void Set(IDeviceModule deviceModule)
        {
            Id = deviceModule.Id;
            Name = deviceModule.Name;
            _deviceType = deviceModule.DeviceType;
        }

        public void SetSettingsUserInterfaceSupported(bool isSupported)
        {
            SettingsUserInterfaceSupported = isSupported;
        }

        public void SetSimulation(bool simulation)
        {
            Simulation = simulation;
        }

        public void SetCommunicationTestedSuccessfully(bool testedSuccessfully, IDeviceInformation deviceInformation)
        {
            CommunicationTestedSuccessfully = testedSuccessfully;
            DeviceInformation = deviceInformation;
        }
    }
}