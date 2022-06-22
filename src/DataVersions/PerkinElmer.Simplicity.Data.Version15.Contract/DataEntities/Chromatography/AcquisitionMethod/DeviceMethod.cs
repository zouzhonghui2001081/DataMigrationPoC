namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod
{
	public class DeviceMethod
	{
		public long Id { get; set; }
		public long AcquisitionMethodId { get; set; }
		public string Name { get; set; }
		public byte[] Content { get; set; }
		public byte[] Configuration { get; set; }
		public short? DeviceType { get; set; }
		public string InstrumentMasterId { get; set; }
		public string InstrumentId { get; set; }
		public string DeviceDriverItemId { get; set; }
		public DeviceModuleDetails[] DeviceModules { get; set; }
		public ExpectedDeviceChannelDescriptor[] ExpectedDeviceChannelDescriptors { get; set; }
	}
}
