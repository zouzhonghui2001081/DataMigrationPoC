using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
	public interface IPdaExtractedChannelMetaDataApexOptimized : IExtractedChannelMetaData, IEquatable<IPdaExtractedChannelMetaDataApexOptimized>, ICloneable
	{
		Guid BaseBrChannelGuid { get; set; } // Refers to the extracted channel on which this Apex Optimized channel is based.
        double BaseBrChannelWavelength { get; set; }
        double WavelengthBandwidth { get; set; }
		bool UseReference { get; set; }
		double ReferenceWavelength { get; set; }
		double ReferenceWavelengthBandwidth { get; set; }
	}
}