using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItemDetails : IDeviceDriverItem
    {
        string Configuration { get; }
        DeviceType? DeviceType { get; }
    }
}