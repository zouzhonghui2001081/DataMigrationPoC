﻿using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class InstrumentModifiable : IInstrumentModifiable
    {
        public InstrumentCompleteId Id { get; private set; }
        public string Name { get; private set; }

        public void Set(InstrumentCompleteId id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}