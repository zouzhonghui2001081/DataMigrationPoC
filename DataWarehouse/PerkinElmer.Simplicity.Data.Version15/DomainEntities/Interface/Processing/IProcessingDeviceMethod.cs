using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
    public interface IProcessingDeviceMethod
    {
        IDeviceIdentifier DeviceIdentifier { get; set; }
        IProcessingDeviceMetaData MetaData { get; set; }
    }
}