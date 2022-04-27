using PerkinElmer.Acquisition.Devices;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using DeviceDriverItemCompleteId = PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition.DeviceDriverItemCompleteId;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    internal class DeviceDriverItemDetailsModifiable : IDeviceDriverItemDetailsModifiable
    {
        public DeviceDriverItemCompleteId Id { get; private set; }

        public string Name { get; private set; }
        public bool IsDisplayDriver { get; private set; }

        public string Configuration { get; private set; }
        public DeviceType? DeviceType { get; private set; }

        public void Set(DeviceDriverItemCompleteId deviceDriverItemCompleteId, string name)
        {
            Id = deviceDriverItemCompleteId;
            Name = name;
        }

        public void SetConfiguration(string configuration)
        {
            Configuration = configuration;
        }

        public void SetDeviceType(DeviceType? deviceType, bool isDisplayDriver)
        {
            DeviceType = deviceType;
            IsDisplayDriver = isDisplayDriver;
        }

        public void Set(IDeviceDriverItemDetails deviceDriverItemDetails)
        {
            Id = deviceDriverItemDetails.Id;
            Name = deviceDriverItemDetails.Name;
            Configuration = deviceDriverItemDetails.Configuration;
            IsDisplayDriver = deviceDriverItemDetails.IsDisplayDriver;
            DeviceType = deviceDriverItemDetails.DeviceType;
        }
    }
}