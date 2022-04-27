namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentMaster
    {
        Id InstrumentMasterId { get; }
        string Name { get;  }
    }
}