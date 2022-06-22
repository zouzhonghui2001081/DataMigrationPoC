namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class BatchResultSetToProcessingMethodMap
	{
		public long Id { get; set; }
		public long BatchResultSetId { get; set; }
		public long ProcessingMethodId { get; set; }
	}
}
