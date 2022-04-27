namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IInstrument
    {
        InstrumentCompleteId Id { get; }
        string Name { get; }
    }
}