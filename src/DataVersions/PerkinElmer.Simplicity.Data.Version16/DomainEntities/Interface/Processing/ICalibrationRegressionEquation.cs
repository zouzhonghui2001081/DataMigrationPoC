﻿using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface ICalibrationRegressionEquation : ICloneable
    {
		RegressionType RegressionType { get; set; }

		double[] Coefficients { get; set; }

		double RSquare { get; set; }

		double RelativeStandardErrorValue { get; set; }

        double RelativeStandardDeviationPercent { get; set; }

        double CorrelationCoefficient { get; set; }
	}
}