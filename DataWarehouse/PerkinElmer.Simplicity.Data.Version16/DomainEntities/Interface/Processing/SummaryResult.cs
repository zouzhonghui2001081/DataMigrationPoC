namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public class SummaryResult
	{
		public double? Average { get; set; }
		public double? StDev { get; set; }
		public double? RelativeStDevPercent { get; set; }
		public bool? RelativeStDevPassed { get; set; }
        public SuitabilityParameterFailureReason FailureReason { get; set; }
    }
}