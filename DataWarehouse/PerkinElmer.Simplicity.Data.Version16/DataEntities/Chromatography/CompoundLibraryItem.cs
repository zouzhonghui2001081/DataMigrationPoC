
using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class CompoundLibraryItem
	{
		public long Id { get; set; }
		public string CompoundName { get; set; }
        public Guid CompoundGuid { get; set; }
		public DateTime CreatedDate { get; set; }
		public double RetentionTime { get; set; }
		public double[] SpectrumAbsorbances { get; set; }
		public double[] BaselineAbsorbances { get; set; }
		public double StartWavelength { get; set; }
		public double EndWavelength { get; set; }
		public double Step { get; set; }
        public bool IsBaselineCorrected { get; set; }
	}
}
