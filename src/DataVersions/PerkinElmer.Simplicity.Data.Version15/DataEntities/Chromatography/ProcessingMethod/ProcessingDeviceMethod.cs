namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod
{
    public class ProcessingDeviceMethod
	{
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public string DeviceClass { get; set; }
		public int DeviceIndex { get; set; }
		public string MetaData { get; set; }
	}
}
