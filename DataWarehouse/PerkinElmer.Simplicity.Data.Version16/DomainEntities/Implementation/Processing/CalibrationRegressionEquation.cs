using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class CalibrationRegressionEquation : ICalibrationRegressionEquation
	{
		public RegressionType RegressionType { get; set; }

		public double[] Coefficients { get; set; }

		public double RSquare { get; set; }

		public double RelativeStandardErrorValue { get; set; }

		public double RelativeStandardDeviationPercent { get; set; }

        public double CorrelationCoefficient { get; set; }

        public object Clone()
		{
			var clonedEquation = (CalibrationRegressionEquation)this.MemberwiseClone();
			clonedEquation.Coefficients = (double[]) this.Coefficients?.Clone();

			return clonedEquation;
		}

	}
}