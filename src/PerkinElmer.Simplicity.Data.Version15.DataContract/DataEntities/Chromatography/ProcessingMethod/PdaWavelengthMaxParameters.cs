
namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaWavelengthMaxParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public bool ApplyBaselineCorrection { get; set; }
		public bool UseAutoAbsorbanceThreshold { get; set; }
		public double ManualAbsorbanceThreshold { get; set; }
	}
}
