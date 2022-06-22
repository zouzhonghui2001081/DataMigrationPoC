
namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod
{
	public abstract class DeviceModuleDetailsBase
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public bool IsDisplayDriver { get; set; }
		public short DeviceType { get; set; }
		public string DeviceModuleId { get; set; }
		public string InstrumentMasterId { get; set; }
		public string InstrumentId { get; set; }
		public string DeviceDriverItemId { get; set; }
		public bool SettingsUserInterfaceSupported { get; set; }
		public bool Simulation { get; set; }
		public bool CommunicationTestedSuccessfully { get; set; }
		public string FirmwareVersion { get; set; }
		public string SerialNumber { get; set; }
		public string ModelName { get; set; }
		public string UniqueIdentifier { get; set; }
		public string InterfaceAddress { get; set; }
	}
}
