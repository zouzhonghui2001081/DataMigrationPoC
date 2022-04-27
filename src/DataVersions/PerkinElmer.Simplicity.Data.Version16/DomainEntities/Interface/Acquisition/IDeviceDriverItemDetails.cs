using PerkinElmer.Acquisition.Devices;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItemDetails : IDeviceDriverItem
    {
        string Configuration { get; }
        DeviceType? DeviceType { get; }
    }
}