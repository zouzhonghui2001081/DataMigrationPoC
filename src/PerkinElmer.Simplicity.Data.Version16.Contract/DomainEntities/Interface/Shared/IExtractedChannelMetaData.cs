using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public interface IExtractedChannelMetaData : IChannelMetaData, ICloneable
	{
		// All Channel MetaDatas related to Extraction are derived from this class
		// They do not have common members for now, so the interface is empty for now
	}
}