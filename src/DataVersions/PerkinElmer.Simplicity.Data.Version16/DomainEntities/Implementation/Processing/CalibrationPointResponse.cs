using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class CalibrationPointResponse : ICalibrationPointResponse
	{
		public bool QuantifyUsingArea { get; set; }

		public bool UseInternalStandard { get; set; }

		public double Area { get; set; }

		public double? AreaRatio { get; set; }

		public double Height { get; set; }

		public double? HeightRatio { get; set; }

		public bool Excluded { get; set; }

		public bool PeakNotFoundError { get; set; }

		public bool InternalStandardPeakNotFoundError { get; set; }

		public Guid BatchRunGuid { get; set; }

		public bool External { get; set; }

		public double PeakAreaPercentage { get; set; }

		public int Level { get; set; }

	    public double StandardAmountAdjustmentCoeff { get; set; } = 1;

	    public double InternalStandardAmountAdjustmentCoeff { get; set; } = 1;

		public double? PointCalibrationFactor { get; set; }

        public bool InvalidAmountError { get; set; } // To prevent division by zero

        public bool OutlierTestFailed { get; set; }

        public double OutlierTestResult { get; set; }

        public object Clone()
		{
			return (CalibrationPointResponse) this.MemberwiseClone();
		}
	}
}