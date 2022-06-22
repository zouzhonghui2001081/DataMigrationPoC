using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod
{
    public class Compound
	{
		public Compound()
		{
			LevelAmounts = new List<LevelAmount>();
			CompoundGuids = new List<CompoundGuids>();
		}
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public Guid ProcessingMethodChannelGuid { get; set; }
		public int CompoundType { get; set; }

		// IIdentificationParameters members
		public double ExpectedRetentionTime { get; set; }
		public double RetentionTimeWindowAbsolute { get; set; }
		public double RetentionTimeWindowInPercents { get; set; }
		public double RetTimeWindowStart { get; set; }
		public double RetTimeWindowEnd { get; set; }
		public bool IsRetTimeReferencePeak { get; set; }
		public Guid RetTimeReferencePeakGuid { get; set; }
		public int RetentionIndex { get; set; }
		public bool UseClosestPeak { get; set; }
		public bool? IsIntStdReferencePeak { get; set; }
		public Guid IntStdReferenceGuid { get; set; }
		public int Index { get; set; }
		public bool IsRrtReferencePeak { get; set; }

		// ICalibrationParameters members
		public bool InternalStandard { get; set; }
		public Guid ReferenceInternalStandardGuid { get; set; }
		public double Purity { get; set; }
		public bool QuantifyUsingArea { get; set; }
		public int CalibrationType { get; set; }
		public int WeightingType { get; set; }
		public int Scaling { get; set; }
		public int OriginTreatment { get; set; }
		public double CalibrationFactor { get; set; }
		public Guid ReferenceCompoundGuid { get; set; }
		public double? InternalStandardAmount { get; set; }
		public List<LevelAmount> LevelAmounts { get; set; }
		public int CompoundNumber { get; set; }

		// CompoundGroup
		public bool IsCompoundGroup { get; set; }
		public double? StartTime { get; set; }
		public double? EndTime { get; set; }
		public bool UsedForSuitability { get; set; }
		public List<CompoundGuids> CompoundGuids { get; set; }
        
    }
}
