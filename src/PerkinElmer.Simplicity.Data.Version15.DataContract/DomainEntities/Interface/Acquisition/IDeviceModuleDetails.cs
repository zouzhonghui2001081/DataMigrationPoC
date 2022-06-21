
namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModuleDetails : IDeviceModule
    {
        bool SettingsUserInterfaceSupported { get; }
        bool Simulation { get; }
        bool CommunicationTestedSuccessfully { get; }
        IDeviceInformation DeviceInformation { get; }
    }
}