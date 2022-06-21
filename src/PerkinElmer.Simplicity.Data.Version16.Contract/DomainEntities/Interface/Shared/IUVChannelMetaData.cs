namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared
{
    public interface IUVChannelMetaData : IChannelMetaData
    {
        double WavelengthInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}