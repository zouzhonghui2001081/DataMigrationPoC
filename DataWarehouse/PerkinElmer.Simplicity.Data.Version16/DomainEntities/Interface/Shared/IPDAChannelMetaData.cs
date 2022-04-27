namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IPdaChannelMetaData : IChannelMetaData
    {
        double StartWavelength { get; set; }
        double EndWavelength { get; set; }
        bool LowResolution { get; set; }
    }
}