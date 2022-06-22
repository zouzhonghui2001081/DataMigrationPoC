namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IMultiUVChannelMetaData : IChannelMetaData
    {
        double WavelengthInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}