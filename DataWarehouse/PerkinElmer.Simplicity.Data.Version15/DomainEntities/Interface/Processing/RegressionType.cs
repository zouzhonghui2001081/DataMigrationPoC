﻿namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
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