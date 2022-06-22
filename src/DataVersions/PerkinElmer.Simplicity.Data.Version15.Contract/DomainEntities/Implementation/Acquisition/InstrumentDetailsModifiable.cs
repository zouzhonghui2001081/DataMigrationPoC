using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition
{
    internal class InstrumentDetailsModifiable : IInstrumentDetailsModifiable
    {
        public InstrumentCompleteId Id { get; private set; }
        public string Name { get; private set; }
        public bool Online { get; private set; }
        public bool Regulated { get; private set; }
        public InstrumentLockState InstrumentLockState { get; private set; }
        public string LockedUser { get; private set; }
        public IReadOnlyList<IDeviceModuleDetails> DeviceModules => (IReadOnlyList<IDeviceModuleDetails>) DeviceModuleDetailsModifiableList;
        public IReadOnlyList<IDeviceDriverItemDetails> DeviceDriverItems => (IReadOnlyList<IDeviceDriverItemDetails>) DeviceDriverItemDetailsModifiableList;
        public IUserInfo CreatedBy { get; private set; }
        public IUserInfo ModifiedBy { get; private set; }
        public DateTime? CreatedDateUtc { get; private set; }
        public DateTime? ModifiedDateUtc { get; private set; }
        public void Set(IInstrumentDetails instrumentDetails)
        {
            Id = instrumentDetails.Id;
            Name = instrumentDetails.Name;
            Online = instrumentDetails.Online;
            Regulated = instrumentDetails.Regulated;
            InstrumentLockState = instrumentDetails.InstrumentLockState;
            LockedUser = instrumentDetails.LockedUser;
            if (instrumentDetails.DeviceDriverItems != null)
            {
                foreach (var deviceDriverItemDetails in instrumentDetails.DeviceDriverItems)
                {
                    var deviceDriverItemDetailsModifiable = new DeviceDriverItemDetailsModifiable();
                    deviceDriverItemDetailsModifiable.Set(deviceDriverItemDetails);
                    DeviceDriverItemDetailsModifiableList.Add(deviceDriverItemDetailsModifiable);
                }
            }

            if (instrumentDetails.DeviceModules != null)
            {
                foreach (var deviceModuleDetails in instrumentDetails.DeviceModules)
                {
                    var deviceModuleDetailsModifiable = new DeviceModuleDetailsModifiable();
                    deviceModuleDetailsModifiable.Set(deviceModuleDetails);
                    DeviceModuleDetailsModifiableList.Add(deviceModuleDetailsModifiable);
                }
            }
            
            CreatedBy = instrumentDetails.CreatedBy;
            CreatedDateUtc = instrumentDetails.CreatedDateUtc;
            ModifiedBy = instrumentDetails.ModifiedBy;
            ModifiedDateUtc = instrumentDetails.ModifiedDateUtc;
        }

        public void Set(IInstrument instrument)
        {
            Id = instrument.Id;
            Name = instrument.Name;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetId(InstrumentCompleteId id)
        {
            Id = id;
        }

        public void SetOnline(bool online)
        {
            Online = online;
        }

        public void SetRegulated(bool regulated)
        {
            Regulated = regulated;
        }

        public void Lock(bool forConfiguring, string user)
        {
            InstrumentLockState =
                forConfiguring ? InstrumentLockState.LockedForConfiguring : InstrumentLockState.Locked;
            LockedUser = user;
        }

        public void Unlock()
        {
            InstrumentLockState = InstrumentLockState.Unlocked;
            LockedUser = String.Empty;
        }

        public void SetCreatedUserInfo(IUserInfo createdInfo)
        {
           CreatedBy = createdInfo;
         }
        public void SetModifiedUserInfo(IUserInfo modifiedInfo)
        {
            ModifiedBy = modifiedInfo;    
        }
        public void SetCreatedDateTime(DateTime? createdDateTime)
        {
            CreatedDateUtc = createdDateTime;
        }
        public void SetModifiedDateTime(DateTime? modifiedDateTime)
        {
            ModifiedDateUtc = modifiedDateTime;
        }


        public IList<IDeviceModuleDetailsModifiable> DeviceModuleDetailsModifiableList { get; } = new List<IDeviceModuleDetailsModifiable>();
        public IList<IDeviceDriverItemDetailsModifiable> DeviceDriverItemDetailsModifiableList { get; } = new List<IDeviceDriverItemDetailsModifiable>();
    }
}