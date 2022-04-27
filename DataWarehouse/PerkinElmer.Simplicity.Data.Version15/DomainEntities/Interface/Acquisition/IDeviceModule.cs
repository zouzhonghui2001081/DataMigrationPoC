using System;
using PerkinElmer.Acquisition.Devices;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModule : IEquatable<IDeviceModule>
    {
        DeviceModuleCompleteId Id { get; }
        string Name { get; }
        DeviceType DeviceType { get;}

    }
}