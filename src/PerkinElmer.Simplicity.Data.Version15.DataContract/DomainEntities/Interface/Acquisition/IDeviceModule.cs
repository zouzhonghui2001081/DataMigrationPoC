using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IDeviceModule : IEquatable<IDeviceModule>
    {
        DeviceModuleCompleteId Id { get; }
        string Name { get; }
        DeviceType DeviceType { get;}

    }
}