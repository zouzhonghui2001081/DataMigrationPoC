using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface ICompoundGroup : ICompound, IEquatable<ICompoundGroup>
	{
		double StartTime { get; set; }     // double – units: seconds 

		double EndTime { get; set; }       //  double – units: seconds

		IList<Guid> CompoundGuids { get; set; }	// array of Compound Guids of compounds that belong to this group (for Named group case)
    }
}