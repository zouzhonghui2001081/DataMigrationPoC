
namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IStreamDataInfo
    {
        string MetaData { get; set; }
        string MetaDataType { get; set; }
        int StreamIndex { get; set; }
		string DeviceDriverId { get; set; }
		bool UseLargeObjectStream { get; set; }
        IDeviceInformation DeviceInformation { get; set; }
	}
}