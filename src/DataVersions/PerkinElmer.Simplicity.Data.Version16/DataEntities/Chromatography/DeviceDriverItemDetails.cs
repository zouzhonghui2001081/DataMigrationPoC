namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class DeviceDriverItemDetails
	{
		public long Id { get; set; }
		public long BatchResultSetId { get; set; }
        public string Configuration { get; set; }
        public short? DeviceType { get; set; }
        public string Name { get; set; }
        public bool IsDisplayDriver { get; set; }
        public string InstrumentMasterId { get; set; }
        public string InstrumentId { get; set; }
        public string DeviceDriverItemId { get; set; }
	}
}
