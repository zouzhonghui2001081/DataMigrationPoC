using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    internal class StreamDataInfo : IStreamDataInfo
    {
        public string DeviceDriverId { get; set; }
        public int StreamIndex { get; set; }
        public string MetaData { get; set; }
        public string MetaDataType { get; set; }
        public bool UseLargeObjectStream { get; set; }
        public IDeviceInformation DeviceInformation { get; set; }
    }
}