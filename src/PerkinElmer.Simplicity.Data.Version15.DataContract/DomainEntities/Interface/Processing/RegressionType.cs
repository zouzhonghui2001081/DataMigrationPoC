namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public enum RegressionType
	{
        AverageCalibrationFactor,
        Linear,
		Quadratic,
		Cubic,
		CalibrationByReference,
		CalibrationFactor,
		PointToPoint,

		NotSupportedYetForDrawingOnUi
	}
}