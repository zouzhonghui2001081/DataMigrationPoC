namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class IntegrationEventBase
	{
		public long Id { get; set; }
		public int EventType { get; set; }
		public int EventId { get; set; }
		public double StartTime { get; set; }
		public double? EndTime { get; set; }
		public double? Value { get; set; }
	}
}
