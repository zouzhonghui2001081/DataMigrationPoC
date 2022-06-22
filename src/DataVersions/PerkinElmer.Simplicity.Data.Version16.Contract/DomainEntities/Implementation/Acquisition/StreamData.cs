using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    internal class StreamData : IStreamData
    {
        public string MetaData { get; set; }
        public string MetaDataType { get; set; }
        public int StreamIndex { get; set; }
        public byte[] Data { get; set; }
        public string DeviceDriverId { get; set; }
        public bool UseLargeObjectStream { get; set; }
        public IDeviceInformation DeviceInformation { get; set; }
    }
}