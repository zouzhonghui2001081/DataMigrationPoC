using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface IProcessingDeviceMethod
    {
        IDeviceIdentifier DeviceIdentifier { get; set; }
        IProcessingDeviceMetaData MetaData { get; set; }
    }
}