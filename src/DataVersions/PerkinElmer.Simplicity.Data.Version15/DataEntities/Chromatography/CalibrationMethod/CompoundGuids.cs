using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod
{
    public class CompoundGuids
	{
		public long Id { get; set; }
		public long CompoundId { get; set; }
		public Guid CompoundGuid { get; set; }
	}
}
