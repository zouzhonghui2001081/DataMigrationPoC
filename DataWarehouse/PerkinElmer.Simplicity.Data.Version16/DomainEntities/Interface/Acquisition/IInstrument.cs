using PerkinElmer.Domain.Contracts.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrument
    {
        InstrumentCompleteId Id { get; }
        string Name { get; }
    }
}