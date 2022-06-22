using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface IPdaAbsorbanceRatioParameters:ICloneable
	{
		double WavelengthA { get; set; }
		double WavelengthB { get; set; }
		bool ApplyBaselineCorrection { get; set; }
		bool UseAutoAbsorbanceThreshold { get; set; }
		double ManualAbsorbanceThreshold { get; set; }
	}
}