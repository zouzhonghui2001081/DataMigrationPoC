using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IPdaExtractedChannelMetaDataSimple : IExtractedChannelMetaData, IEquatable<IPdaExtractedChannelMetaDataSimple>, ICloneable
    {
        double Wavelength { get; set; }
        double WavelengthBandwidth { get; set; }
        bool UseReference { get; set; }
        double ReferenceWavelength { get; set; }
        double ReferenceWavelengthBandwidth { get; set; }
		bool IsApexOptimized { get; set; }
    }
}