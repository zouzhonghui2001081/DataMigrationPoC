using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition
{
    internal class InstrumentMasterModifiable : IInstrumentMasterModifiable
    {
        public Id InstrumentMasterId { get; private set; }
        public string Name { get; private set; }

        public void Set(Id instrumentMasterId, string name)
        {
            InstrumentMasterId = instrumentMasterId;
            Name = name;
        }
    }
}