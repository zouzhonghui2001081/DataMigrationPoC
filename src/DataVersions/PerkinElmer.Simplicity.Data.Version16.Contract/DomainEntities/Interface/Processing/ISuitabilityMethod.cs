using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ISuitabilityMethod : ICloneable, IEquatable<ISuitabilityMethod>
	{
		bool Enabled { get; set; }

		PharmacopeiaType SelectedPharmacopeiaType { get; set; }

		bool IsEfficiencyInPlates { get; set; }

		double ColumnLength { get; set; }

		bool SignalToNoiseWindowEnabled { get; set; }

        bool PerformAdditionalSearchForNoiseWindow { get; set; } 
        
        double SignalToNoiseWindowStart { get; set; }
		double SignalToNoiseWindowEnd { get; set; }

		bool AnalyzeAdjacentPeaks { get; set; }

		VoidTimeType VoidTimeType { get; set; }
		double VoidTimeCustomValueInSeconds { get; set; }

		IDictionary<Guid /*CompoundGuid*/, 
			IDictionary<PharmacopeiaType, 
				IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> CompoundPharmacopeiaDefinitions { get; set; }
	}
}