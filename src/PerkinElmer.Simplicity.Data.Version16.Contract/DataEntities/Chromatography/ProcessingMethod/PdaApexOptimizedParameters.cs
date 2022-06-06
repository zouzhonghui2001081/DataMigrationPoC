
namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaApexOptimizedParameters
	{
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public double WavelengthBandwidth { get; set; }
		public bool UseReference { get; set; }
		public double ReferenceWavelength { get; set; }
		public double ReferenceWavelengthBandwidth { get; set; }
		public bool ApplyBaselineCorrection { get; set; }
		public bool UseAutoAbsorbanceThreshold { get; set; }
		public double ManualAbsorbanceThreshold { get; set; }
	}
}
