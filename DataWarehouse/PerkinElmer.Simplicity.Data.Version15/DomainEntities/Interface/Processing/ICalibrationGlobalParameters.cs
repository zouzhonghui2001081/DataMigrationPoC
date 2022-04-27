using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface ICalibrationGlobalParameters : ICloneable, IEquatable<ICalibrationGlobalParameters>
    {
		int NumberOfLevels { get; set; }

		string AmountUnits { get; set; }

		UnidentifiedPeakCalibrationType UnidentifiedPeakCalibrationType { get; set; }

		double UnidentifiedPeakCalibrationFactor { get; set; }

		Guid UnidentifiedPeakReferenceCompoundGuid { get; set; }
        bool IsEqual(ICalibrationGlobalParameters other);
    }
}