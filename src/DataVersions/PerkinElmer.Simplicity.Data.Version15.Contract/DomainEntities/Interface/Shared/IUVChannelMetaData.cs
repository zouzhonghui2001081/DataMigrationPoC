namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IUVChannelMetaData : IChannelMetaData
    {
        double WavelengthInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}