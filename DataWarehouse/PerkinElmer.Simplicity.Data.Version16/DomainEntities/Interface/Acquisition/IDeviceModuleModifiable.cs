﻿using PerkinElmer.Acquisition.Devices;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModuleModifiable : IDeviceModule
    {
        void Set(DeviceModuleCompleteId deviceModuleCompleteId, string name, DeviceType deviceType);
    }
}