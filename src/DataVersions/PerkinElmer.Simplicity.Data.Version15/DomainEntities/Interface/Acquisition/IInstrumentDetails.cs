using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
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