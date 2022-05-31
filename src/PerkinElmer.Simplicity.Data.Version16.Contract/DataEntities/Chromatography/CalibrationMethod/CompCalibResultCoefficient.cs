namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.CalibrationMethod
{
    public class CompCalibResultCoefficient
	{
		public long Id { get; set; }
		public long CompoundCalibrationResultsId { get; set; }
		public double Coefficients { get; set; }
	}
}
