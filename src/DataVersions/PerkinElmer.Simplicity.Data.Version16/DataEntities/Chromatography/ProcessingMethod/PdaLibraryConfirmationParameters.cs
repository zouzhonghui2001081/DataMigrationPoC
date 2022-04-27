
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class PdaLibraryConfirmationParameters
	{
		public long Id { get; set; }
		public long ChannelMethodId { get; set; }
		public double MinWavelength { get; set; }
		public double MaxWavelength { get; set; }
		public bool BaselineCorrectionEnabled { get; set; }
		public double HitDistanceThreshold { get; set; }
		public List<PdaLibraryConfirmationSelectedLibraries> SelectedLibraries { get; set; }


	}
}
