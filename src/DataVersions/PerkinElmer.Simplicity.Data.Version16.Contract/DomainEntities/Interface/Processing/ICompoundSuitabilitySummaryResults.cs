using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ICompoundSuitabilitySummaryResults : ICloneable
	{
		Guid CompoundGuid { get; set; }
		SummaryResult RetentionTime { get; set; }
		SummaryResult Area { get; set; }
		SummaryResult Height { get; set; }
		SummaryResult TheoreticalPlatesN { get; set; }
		SummaryResult TheoreticalPlatesNTan { get; set; }
        SummaryResult TheoreticalPlatesNFoleyDorsey { get; set; }
		SummaryResult TailingFactorSymmetry { get; set; }
		SummaryResult RelativeRetention { get; set; }
		SummaryResult RelativeRetentionTime { get; set; }
		SummaryResult CapacityFactorKPrime { get; set; }
		SummaryResult Resolution { get; set; }
		SummaryResult UspResolution { get; set; }
		SummaryResult SignalToNoise { get; set; }
		SummaryResult PeakWidthAtBase { get; set; }
		SummaryResult PeakWidthAt5Pct { get; set; }
		SummaryResult PeakWidthAt10Pct { get; set; }
		SummaryResult PeakWidthAt50Pct { get; set; }

        bool? SstFlag { get; set; }
    }
}