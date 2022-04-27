using PerkinElmer.Domain.Contracts.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentModifiable : IInstrument
    {
        void Set(InstrumentCompleteId instrumentCompleteId, string name);
    }
}
