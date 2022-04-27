using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.CalibrationMethod
{
    public class CompoundCalibrationResults
	{
		public CompoundCalibrationResults()
		{
			CalibrationPointResponses = new List<CalibrationPointResponse>();
			CompCalibResultCoefficients = new List<CompCalibResultCoefficient>();
			InvalidAmounts = new List<InvalidAmounts>();
		}
		public long Id { get; set; }
		public long ProcessingMethodId { get; set; }
		public bool NotEnoughLevelsFoundError { get; set; }
		public bool InvalidAmountError { get; set; }
		public int RegressionType { get; set; }
		public double[] Coefficients { get; set; }
		public double RSquare { get; set; }
		public double RelativeStandardErrorValue { get; set; }
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public int ChannelIndex { get; set; }
		public int ConfLimitTestResult { get; set; }
		//public double[] InvalidAmounts { get; set; }
		public double RelativeStandardDeviationPercent { get; set; }
		public double CorrelationCoefficient { get; set; }
		public List<CalibrationPointResponse> CalibrationPointResponses { get; set; }
		public List<CompCalibResultCoefficient> CompCalibResultCoefficients { get; set; }
		public List<InvalidAmounts> InvalidAmounts { get; set; }
	}
}
