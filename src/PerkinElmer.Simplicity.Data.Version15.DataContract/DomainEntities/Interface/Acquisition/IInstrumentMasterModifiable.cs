namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentMasterModifiable : IInstrumentMaster
    {
        void Set(Id instrumentMasterId, string name);
    }
}