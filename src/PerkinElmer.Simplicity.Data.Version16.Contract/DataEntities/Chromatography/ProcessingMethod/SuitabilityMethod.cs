namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class SuitabilityMethod
	{
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public bool Enabled { get; set; }
		public short SelectedPharmacopeiaType { get; set; }
		public bool IsEfficiencyInPlates { get; set; }
		public double ColumnLength { get; set; }
		public double SignalToNoiseWindowStart { get; set; }
		public double SignalToNoiseWindowEnd { get; set; }
		public bool SignalToNoiseEnabled { get; set; }
        public bool PerformAdditionalSearchForNoiseWindow { get; set; }
        
        public bool AnalyzeAdjacentPeaks { get; set; }
		public string CompoundPharmacopeiaDefinitions { get; set; }
        public short VoidTimeType { get; set; }
        public double VoidTimeCustomValueInSeconds { get; set; }

    }
}
