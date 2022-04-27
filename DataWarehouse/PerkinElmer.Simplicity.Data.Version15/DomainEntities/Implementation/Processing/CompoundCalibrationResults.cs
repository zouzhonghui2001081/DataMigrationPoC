using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
	internal class CompoundCalibrationResults : ICompoundCalibrationResults
	{
		public CompoundCalibrationResults()
		{
			LevelResponses = new Dictionary<int, List<ICalibrationPointResponse>>();
			RegressionEquation = new CalibrationRegressionEquation();
		}

		public Guid CompoundGuid { get; set; }

		public bool NotEnoughLevelsFoundError { get; set; }

		public bool InvalidAmountError { get; set; }

        public List<double> InvalidAmounts { get; set; }

		public ICalibrationRegressionEquation RegressionEquation { get; set; }

		public IDictionary<int, List<ICalibrationPointResponse>> LevelResponses { get; set; }

        public int ConfLimitTestResult { get; set; }

        public object Clone()
		{
			var clonedCompoundCalibResults = (CompoundCalibrationResults)this.MemberwiseClone();
			clonedCompoundCalibResults.LevelResponses = new Dictionary<int, List<ICalibrationPointResponse>>();
			foreach (var kvp in this.LevelResponses)
			{
				var listOfPoints = new List<ICalibrationPointResponse>(kvp.Value.Select(response=>(ICalibrationPointResponse)response.Clone()));
				clonedCompoundCalibResults.LevelResponses.Add(kvp.Key, listOfPoints);
			}

            clonedCompoundCalibResults.InvalidAmounts = new List<double>();
			if (InvalidAmounts != null)
				foreach (var amt in InvalidAmounts)
				{
					clonedCompoundCalibResults.InvalidAmounts.Add(amt);
				}

			clonedCompoundCalibResults.RegressionEquation = (ICalibrationRegressionEquation)this.RegressionEquation.Clone();

			return clonedCompoundCalibResults;
		}

	}
}