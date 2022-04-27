
namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaPeakPurityParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public int MinimumDataPoints { get; set; }
		public bool ApplyBaselineCorrection { get; set; }
		public double PurityLimit { get; set; }
		public double PercentOfPeakHeightForSpectra { get; set; }
		public bool UseAutoAbsorbanceThreshold { get; set; }
		public double ManualAbsorbanceThreshold { get; set; }
	}
}
