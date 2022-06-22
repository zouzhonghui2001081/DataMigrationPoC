using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ISuitabilityParameterCriteria : IEquatable<ISuitabilityParameterCriteria>, ICloneable
	{
		bool Enabled { get; set; }
		double LowerLimit { get; set; }
		double UpperLimit { get; set; }
		double RsdLimit { get; set; }
	}
}