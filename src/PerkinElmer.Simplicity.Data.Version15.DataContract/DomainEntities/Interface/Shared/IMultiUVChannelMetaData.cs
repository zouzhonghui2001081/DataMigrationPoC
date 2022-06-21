namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IMultiUVChannelMetaData : IChannelMetaData
    {
        double WavelengthInNanometers { get; set; }
        bool Programmed { get; set; }
    }
}