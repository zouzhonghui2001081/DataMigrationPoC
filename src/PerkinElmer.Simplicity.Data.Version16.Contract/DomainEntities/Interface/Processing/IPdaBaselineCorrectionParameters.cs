using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IPdaBaselineCorrectionParameters:ICloneable
	{
		BaselineCorrectionType CorrectionType { get; set; }
		double? SelectedSpectrumTimeInSeconds { get; set; }
		double? RangeStartInSeconds { get; set; }
		double? RangeEndInSeconds { get; set; }
	}
}