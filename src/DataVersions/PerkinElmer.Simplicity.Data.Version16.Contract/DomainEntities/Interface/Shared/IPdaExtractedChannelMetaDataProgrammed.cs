using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
	public interface IPdaExtractedChannelMetaDataProgrammed : IExtractedChannelMetaData, IEquatable<IPdaExtractedChannelMetaDataProgrammed>, ICloneable
	{
		IList<IPdaExtractionSegment> ExtractionSegments { get; set; }
	}
}