using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
	internal class CompoundSuitabilitySummaryResults : ICompoundSuitabilitySummaryResults
	{
		public Guid CompoundGuid { get; set; }
		public SummaryResult RetentionTime { get; set; }
		public SummaryResult Area { get; set; }
		public SummaryResult Height { get; set; }
		public SummaryResult TheoreticalPlatesNTan { get; set; }
		public SummaryResult TheoreticalPlatesN { get; set; }
		public SummaryResult TailingFactorSymmetry { get; set; }
		public SummaryResult RelativeRetention { get; set; }
		public SummaryResult RelativeRetentionTime { get; set; }
		public SummaryResult CapacityFactorKPrime { get; set; }
		public SummaryResult Resolution { get; set; }
		public SummaryResult UspResolution { get; set; }
		public SummaryResult SignalToNoise { get; set; }
		public SummaryResult PeakWidthAtBase { get; set; }
		public SummaryResult PeakWidthAt5Pct { get; set; }
		public SummaryResult PeakWidthAt10Pct { get; set; }
		public SummaryResult PeakWidthAt50Pct { get; set; }
        public bool? SstFlag { get; set; }

        public object Clone()
		{
			var suitabilitySummaryResults = new CompoundSuitabilitySummaryResults()
			{
				CompoundGuid = CompoundGuid,
				RetentionTime = RetentionTime,
				Area = Area,
				Height = Height,
				TheoreticalPlatesNTan = TheoreticalPlatesNTan,
				TheoreticalPlatesN = TheoreticalPlatesN,
				TailingFactorSymmetry = TailingFactorSymmetry,
				RelativeRetention = RelativeRetention,
				RelativeRetentionTime = RelativeRetentionTime,
				CapacityFactorKPrime = CapacityFactorKPrime,
				Resolution = Resolution,
				UspResolution = UspResolution,
				SignalToNoise = SignalToNoise,
				PeakWidthAtBase = PeakWidthAtBase,
				PeakWidthAt5Pct = PeakWidthAt5Pct,
				PeakWidthAt10Pct = PeakWidthAt10Pct,
				PeakWidthAt50Pct = PeakWidthAt50Pct,
                SstFlag = SstFlag,
            };

			return suitabilitySummaryResults;
		}
	}
}
