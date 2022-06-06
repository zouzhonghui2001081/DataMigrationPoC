using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.CalibrationMethod
{
    public class CalibrationPointResponse
	{
		public long Id { get; set; }
        public long CompoundCalibrationResultsId { get; set; }
		public int Level { get; set; }
		public bool QuantifyUsingArea { get; set; }
		public bool UseInternalStandard { get; set; }
		public double Area { get; set; }
		public double? AreaRatio { get; set; }
		public double Height { get; set; }
        public bool PeakNotFoundError { get; set; }
        public bool InternalStandardPeakNotFoundError { get; set; }
        public double? HeightRatio { get; set; }
		public bool Excluded { get; set; }
		public Guid BatchRunGuid { get; set; }
		public bool External { get; set; }
		public double PeakAreaPercentage { get; set; }
		public double? PointCalibrationFactor { get; set; }
		public bool InvalidAmountError { get; set; }
		public bool OutlierTestFailed { get; set; }
		public double OutlierTestResult { get; set; }
		public double StandardAmountAdjustmentCoeff { get; set; }
		public double InternalStandardAmountAdjustmentCoeff { get; set; }
	}
}
