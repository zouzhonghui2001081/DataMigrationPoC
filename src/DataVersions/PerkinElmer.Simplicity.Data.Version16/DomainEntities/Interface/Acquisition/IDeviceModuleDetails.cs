

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModuleDetails : IDeviceModule
    {
        bool SettingsUserInterfaceSupported { get; }
        bool Simulation { get; }
        bool CommunicationTestedSuccessfully { get; }
        IDeviceInformation DeviceInformation { get; }
    }
}