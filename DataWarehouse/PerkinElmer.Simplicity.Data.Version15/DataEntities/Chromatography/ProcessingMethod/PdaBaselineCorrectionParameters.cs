
namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaBaselineCorrectionParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public short CorrectionType { get; set; }
		public double? SelectedSpectrumTime { get; set; }
		public double? RangeStart { get; set; }
		public double? RangeEnd { get; set; }
	}
}
