using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IInstrumentDetailsModifiable : IInstrumentDetails
    {
        void Set(IInstrumentDetails instrumentDetails);
        void Set(IInstrument instrument);
        void SetName(string name);
        void SetId(InstrumentCompleteId id);
        void SetOnline(bool online);
        void SetRegulated(bool regulated);
        void Lock(bool forConfiguring, string user);
        void Unlock();
        void SetCreatedUserInfo(IUserInfo createdInfo);
        void SetModifiedUserInfo(IUserInfo modifiedInfo);
        void SetCreatedDateTime(DateTime? createdDateTime);
        void SetModifiedDateTime(DateTime? modifiedDateTime);
        IList<IDeviceModuleDetailsModifiable> DeviceModuleDetailsModifiableList { get; }
        IList<IDeviceDriverItemDetailsModifiable> DeviceDriverItemDetailsModifiableList { get; }
    }
}