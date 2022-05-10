using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface ICalibrationParameters : IEquatable<ICalibrationParameters>, ICloneable
	{
		bool InternalStandard { get; set; } // This compound is Internal Standard if true

		Guid ReferenceInternalStandardGuid { get; set; }

        double? InternalStandardAmount { get; set; }

		double Purity { get; set; } // 0 to 100

		bool QuantifyUsingArea { get; set; }

		CompoundCalibrationType CalibrationType { get; set; }

		CalibrationWeightingType WeightingType { get; set; }

		CalibrationScaling Scaling { get; set; }

		OriginTreatment OriginTreatment { get; set; }

		double CalibrationFactor { get; set; }

		Guid ReferenceCompoundGuid { get; set; } // Compound used in Calibration By Reference

		IDictionary<int, double?> LevelAmounts { get; set; }

		bool IsEqual(ICalibrationParameters other);
	}
}