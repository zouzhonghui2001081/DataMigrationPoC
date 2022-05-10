using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface IPdaStandardConfirmationParameters:ICloneable
	{
		Guid PdaStandardConfirmationGuid { get; set; }
		double MinWavelength { get; set; }
		double MaxWavelength { get; set; }
		int MinimumDataPoints { get; set; }
		double PassThreshold { get; set; }
		bool ApplyBaselineCorrection { get; set; }
		bool UseAutoAbsorbanceThresholdForSample { get; set; }
		double ManualAbsorbanceThresholdForSample { get; set; }
		bool UseAutoAbsorbanceThresholdForStandard { get; set; }
		double ManualAbsorbanceThresholdForStandard { get; set; }
	    StandardType StandardType { get; set; }
    }
}