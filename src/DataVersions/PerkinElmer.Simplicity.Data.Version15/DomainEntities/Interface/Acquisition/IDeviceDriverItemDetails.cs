using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItemDetails : IDeviceDriverItem
    {
        string Configuration { get; }
        DeviceType? DeviceType { get; }
    }
}