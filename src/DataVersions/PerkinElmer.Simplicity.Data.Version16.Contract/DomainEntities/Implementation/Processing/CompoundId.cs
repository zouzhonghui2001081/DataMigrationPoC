using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
	public class CompoundId : ICompoundId
	{
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public int ChannelIndex { get; set; }
	}
}
