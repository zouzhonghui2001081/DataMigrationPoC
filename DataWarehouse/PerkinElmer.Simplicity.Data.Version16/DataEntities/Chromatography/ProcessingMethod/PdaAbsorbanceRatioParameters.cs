
namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaAbsorbanceRatioParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double WavelengthA { get; set; }
		public double WavelengthB { get; set; }
		public bool ApplyBaselineCorrection { get; set; }
		public bool UseAutoAbsorbanceThreshold { get; set; }
		public double ManualAbsorbanceThreshold { get; set; }
	}
}
