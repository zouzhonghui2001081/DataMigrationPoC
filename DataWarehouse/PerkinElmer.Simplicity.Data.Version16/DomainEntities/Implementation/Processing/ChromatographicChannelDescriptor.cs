using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class ChromatographicChannelDescriptor : IChromatographicChannelDescriptor
    {
        public ExtractionType ExtractionType { get; set; }
        public IExtractedChannelMetaData ExtractionMetaData { get; set; } 
        public IDeviceChannelDescriptor DeviceChannelDescriptor { get; set; }
        public PostProcessingChannelType PostProcessingType { get; set; }
        public IPostProcessingMetaData PostProcessingMetaData { get; set; }

        public bool Equals(IChromatographicChannelDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return ExtractionType == other.ExtractionType
                   && Equals(ExtractionMetaData, other.ExtractionMetaData)
                   && Equals(DeviceChannelDescriptor, other.DeviceChannelDescriptor);
            //&& PostProcessingType == other.PostProcessingType; //post-processing (blankSubtracted or not) is ignored, it is transient detail.
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IChromatographicChannelDescriptor)obj);
        }

        public override int GetHashCode()
        {
	        unchecked
	        {
		        var hashCode = ExtractionType.GetHashCode();
		        hashCode += (ExtractionMetaData != null ? ExtractionMetaData.GetHashCode() : 0);
		        hashCode += (DeviceChannelDescriptor != null ? DeviceChannelDescriptor.GetHashCode() : 0);
		        hashCode += (int) PostProcessingType;
		        return hashCode;
	        }
        }

        public object Clone()
        {
            ChromatographicChannelDescriptor chromatographicChannelDescriptor = new ChromatographicChannelDescriptor
                {
                    DeviceChannelDescriptor = (IDeviceChannelDescriptor)DeviceChannelDescriptor.Clone(),
                    ExtractionType = ExtractionType,
                    PostProcessingType = PostProcessingType
                };
            return chromatographicChannelDescriptor;
        }
    }
}