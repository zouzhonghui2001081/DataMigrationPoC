using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
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