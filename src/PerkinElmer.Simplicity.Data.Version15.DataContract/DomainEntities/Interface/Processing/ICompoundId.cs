using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface ICompoundId
	{
		Guid Guid { get; set; } // Used for ReferenceCompoundGuid in parameters

		string Name { get; set; }

		int ChannelIndex { get; set; }
	}
}