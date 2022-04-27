using System;
using System.Collections.Generic;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentDetails : IInstrument
    {
        bool Online { get; }
        bool Regulated { get; }
        InstrumentLockState InstrumentLockState { get; }
        string LockedUser { get; }
        IUserInfo CreatedBy { get; }
        IUserInfo ModifiedBy { get; }
        DateTime? CreatedDateUtc { get; }
        DateTime? ModifiedDateUtc { get; }
        IReadOnlyList<IDeviceModuleDetails> DeviceModules { get; } 
        IReadOnlyList<IDeviceDriverItemDetails> DeviceDriverItems { get; }
    }
}