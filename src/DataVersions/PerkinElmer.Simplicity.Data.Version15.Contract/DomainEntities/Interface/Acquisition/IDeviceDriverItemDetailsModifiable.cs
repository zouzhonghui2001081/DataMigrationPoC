using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItemDetailsModifiable : IDeviceDriverItemDetails
    {
        void Set(DeviceDriverItemCompleteId deviceDriverItemCompleteId, string name);
        void SetConfiguration(string configuration);
        void SetDeviceType(DeviceType? deviceType, bool isDisplayDriver);
    }
}