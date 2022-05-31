
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaLibrarySearchParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public double MatchRetentionTimeWindow { get; set; }
		public bool MatchRetentionTimeWindowEnabled { get; set; }
		public bool BaselineCorrectionEnabled { get; set; }
		public double HitDistanceThreshold { get; set; }
		public bool PeakLibrarySearch { get; set; }
		public bool UseWavelengthLimits { get; set; }
		public int MaxNumberOfResults { get; set; }
		public List<PdaLibrarySearchSelectedLibraries> SelectedLibraries { get; set; }
	}
}
