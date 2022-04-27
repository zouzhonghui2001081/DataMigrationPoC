using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface IInternalChannelMappingItem : IChannelMappingItem
	{
		(IList<double> TimeInSeconds, IList<double> Response) XyData { get; set; }
	}
}