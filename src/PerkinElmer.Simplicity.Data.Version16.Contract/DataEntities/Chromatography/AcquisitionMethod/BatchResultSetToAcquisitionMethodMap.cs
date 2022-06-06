namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.AcquisitionMethod
{
	public class BatchResultSetToAcquisitionMethodMap
	{
		public long Id { get; set; }
		public long BatchResultSetId { get; set; }
		public long AcquisitionMethodId { get; set; }
	}
}
