using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModuleModifiable : IDeviceModule
    {
        void Set(DeviceModuleCompleteId deviceModuleCompleteId, string name, DeviceType deviceType);
    }
}