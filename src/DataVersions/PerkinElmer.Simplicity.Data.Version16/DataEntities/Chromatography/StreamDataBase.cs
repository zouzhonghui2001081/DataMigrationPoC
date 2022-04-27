
namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public abstract class StreamDataBase
	{
		public long Id { get; set; }
		public int StreamIndex { get; set; }
		public string MetaData { get; set; }
		public string MetaDataType { get; set; }
		public byte[] YData { get; set; }
		public string DeviceId { get; set; }
		public long? LargeObjectOid { get; set; }
		public bool UseLargeObjectStream { get; set; }
		public string FirmwareVersion { get; set; }
		public string SerialNumber { get; set; }
		public string ModelName { get; set; }
		public string UniqueIdentifier { get; set; }
		public string InterfaceAddress { get; set; }
	}
}
