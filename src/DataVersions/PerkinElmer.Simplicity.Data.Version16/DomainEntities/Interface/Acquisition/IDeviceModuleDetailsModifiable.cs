using PerkinElmer.Acquisition.Devices;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModuleDetailsModifiable : IDeviceModuleDetails
    {
        void Set(IDeviceModule deviceModule);
        void SetSettingsUserInterfaceSupported(bool isSupported);
        void SetSimulation(bool simulation);
        void SetCommunicationTestedSuccessfully(bool testedSuccessfully, IDeviceInformation deviceInformation);
    }
}