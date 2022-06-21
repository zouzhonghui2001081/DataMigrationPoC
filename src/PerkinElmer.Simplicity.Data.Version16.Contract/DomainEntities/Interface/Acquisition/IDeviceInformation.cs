namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface IDeviceInformation
    {
        /// <summary>
        /// 
        /// </summary>
        string FirmwareVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        string SerialNumber { get; }

        /// <summary>
        /// 
        /// </summary>
        string ModelName { get; }

        /// <summary>
        /// MAC Address of the connected device.
        /// </summary>
        string UniqueIdentifier { get; }

        /// <summary>
        /// IP Address or communication interface of the device
        /// </summary>
        string InterfaceAddress { get; }
    }
}
