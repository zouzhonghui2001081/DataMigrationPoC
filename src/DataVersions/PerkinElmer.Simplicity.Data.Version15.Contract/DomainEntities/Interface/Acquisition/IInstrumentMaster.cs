namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentMaster
    {
        Id InstrumentMasterId { get; }
        string Name { get;  }
    }
}