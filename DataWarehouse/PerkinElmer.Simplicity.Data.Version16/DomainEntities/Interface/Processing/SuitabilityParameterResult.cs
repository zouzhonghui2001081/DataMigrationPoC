namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public class SuitabilityParameterResult
	{
		public SuitabilityParameterResult(double? value, SuitabilityParameterFailureReason failureReason, bool passed)
		{
			Value = value;
			FailureReason = failureReason;
			Passed = passed;
		}
        
        public static implicit operator SuitabilityParameterResult(double value) => new SuitabilityParameterResult(value, 
	        SuitabilityParameterFailureReason.None, true);

		public double? Value { get; }
		public SuitabilityParameterFailureReason FailureReason { get; set; } 
		public bool Passed { get; set; }
	}
}