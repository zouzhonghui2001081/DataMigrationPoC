using System;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModule : IEquatable<IDeviceModule>
    {
        DeviceModuleCompleteId Id { get; }
        string Name { get; }
        DeviceType DeviceType { get;}

    }
}