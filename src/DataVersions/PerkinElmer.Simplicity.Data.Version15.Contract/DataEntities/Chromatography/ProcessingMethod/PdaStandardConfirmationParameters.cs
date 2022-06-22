
using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaStandardConfirmationParameters
	{
	    public Guid PdaStandardConfirmationGuid { get; set; }
        public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public int MinimumDataPoints { get; set; }
		public double PassThreshold { get; set; }
		public bool ApplyBaselineCorrection { get; set; }
		public bool UseAutoAbsorbanceThresholdForSample { get; set; }
		public double ManualAbsorbanceThresholdForSample { get; set; }
		public bool UseAutoAbsorbanceThresholdForStandard { get; set; }
		public double ManualAbsorbanceThresholdForStandard { get; set; }
	    public short StandardType { get; set; }
    }
}
