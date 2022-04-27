namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentModifiable : IInstrument
    {
        void Set(InstrumentCompleteId instrumentCompleteId, string name);
    }
}
