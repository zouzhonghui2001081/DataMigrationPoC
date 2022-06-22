using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ICalibrationPointResponse : ICloneable
	{
		bool QuantifyUsingArea { get; set; }

		bool UseInternalStandard { get; set; }

		double Area { get; set; }

		double? AreaRatio { get; set; }

		double Height { get; set; }

		double? HeightRatio { get; set; }

		bool Excluded { get; set; }

        bool PeakNotFoundError { get; set; }

        bool InternalStandardPeakNotFoundError { get; set; }

		Guid BatchRunGuid { get; set; } // Pointer to Standard BatchRun corresponding to this point
										// It is important to fill it even for Outside calibration. Used for Calibration point identification.
		bool External { get; set; }

		double PeakAreaPercentage { get; set; }

		int Level { get; set; }

		double StandardAmountAdjustmentCoeff { get; set; }

		double InternalStandardAmountAdjustmentCoeff { get; set; }

        double? PointCalibrationFactor { get; set; } // used in Average Calibration Factor calculation and display 

        bool InvalidAmountError { get; set; }

        bool OutlierTestFailed { get; set; }

        double OutlierTestResult { get; set; }

    }
}