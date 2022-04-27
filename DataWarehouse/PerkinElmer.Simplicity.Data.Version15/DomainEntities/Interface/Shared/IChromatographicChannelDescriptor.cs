using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared
{
    public interface IChromatographicChannelDescriptor : IEquatable<IChromatographicChannelDescriptor>, ICloneable
    {
        IDeviceChannelDescriptor DeviceChannelDescriptor { get; set; } // descriptor of physical channel-origin of data
        ExtractionType ExtractionType { get; set; }
        IExtractedChannelMetaData ExtractionMetaData { get; set; }
        PostProcessingChannelType PostProcessingType { get; set; }
        IPostProcessingMetaData PostProcessingMetaData { get; set; }
    }
}