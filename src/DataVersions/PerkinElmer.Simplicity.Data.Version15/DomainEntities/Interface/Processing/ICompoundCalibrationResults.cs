using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface ICompoundCalibrationResults : ICloneable
    {
		Guid CompoundGuid { get; set; }

		IDictionary<int, List<ICalibrationPointResponse>> LevelResponses { get; set; }  // If compound is using InternalStandard we store ratios

		bool NotEnoughLevelsFoundError { get; set; }

		bool InvalidAmountError { get; set; } // To prevent division by zero

        List<double> InvalidAmounts { get; set; }

        int ConfLimitTestResult { get; set; }

        ICalibrationRegressionEquation RegressionEquation { get; set; }
        
    }
}