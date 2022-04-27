namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IGCChannelMetaData : IChannelMetaData
    {
        string DetectorType { get; set; }
    }
}