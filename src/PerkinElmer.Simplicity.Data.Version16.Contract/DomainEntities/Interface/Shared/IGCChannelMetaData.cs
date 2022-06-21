namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IGCChannelMetaData : IChannelMetaData
    {
        string DetectorType { get; set; }
    }
}