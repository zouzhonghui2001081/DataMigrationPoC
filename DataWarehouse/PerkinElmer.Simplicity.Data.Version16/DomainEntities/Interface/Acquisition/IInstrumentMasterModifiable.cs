namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentMasterModifiable : IInstrumentMaster
    {
        void Set(Id instrumentMasterId, string name);
    }
}