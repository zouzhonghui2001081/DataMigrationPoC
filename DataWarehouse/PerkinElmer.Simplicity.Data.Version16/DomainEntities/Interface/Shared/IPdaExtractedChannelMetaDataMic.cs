using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
	public interface IPdaExtractedChannelMetaDataMic : IExtractedChannelMetaData, IEquatable<IPdaExtractedChannelMetaDataMic>, ICloneable
	{
		//Maximum Intensity Chromatogram has no parameters except inherited from IChannelMetaData
	}
}