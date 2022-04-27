namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IFLChannelMetaData : IChannelMetaData
    {
        double ExcitationInNanometers { get; set; }
        double EmissionInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}