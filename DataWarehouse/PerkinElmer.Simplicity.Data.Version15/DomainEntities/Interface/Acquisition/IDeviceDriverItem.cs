namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IDeviceDriverItem
    {
        DeviceDriverItemCompleteId Id { get; }
        string Name { get; } // Name from Driver interface
        bool IsDisplayDriver { get; }
    }
}