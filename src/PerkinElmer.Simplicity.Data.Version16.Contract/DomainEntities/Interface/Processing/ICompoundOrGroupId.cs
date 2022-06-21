using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface ICompoundOrGroupId
	{
		Guid Guid { get; set; } // Used for ReferenceCompoundGuid in parameters

		int CompoundNumber { get; set; }

		string Name { get; set; }

		[Obsolete]int ChannelIndex { get; set; } //deprecated
		Guid ProcessingMethodChannelGuid { get; set; } // to be used instead of ChannelIndex
	}
}