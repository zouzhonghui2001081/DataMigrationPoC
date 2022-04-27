namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IMultiUVChannelMetaData : IChannelMetaData
    {
        double WavelengthInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}