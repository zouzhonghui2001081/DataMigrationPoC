namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IGCChannelMetaData : IChannelMetaData
    {
        string DetectorType { get; set; }
    }
}