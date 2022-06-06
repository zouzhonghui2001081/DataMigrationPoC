using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItemDetailsModifiable : IDeviceDriverItemDetails
    {
        void Set(DeviceDriverItemCompleteId deviceDriverItemCompleteId, string name);
        void SetConfiguration(string configuration);
        void SetDeviceType(DeviceType? deviceType, bool isDisplayDriver);
    }
}