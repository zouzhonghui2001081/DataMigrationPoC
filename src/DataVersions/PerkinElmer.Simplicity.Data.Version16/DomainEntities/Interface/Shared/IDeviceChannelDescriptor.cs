using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IDeviceChannelDescriptor : IEquatable<IDeviceChannelDescriptor>, ICloneable
    {
        IDeviceChannelIdentifier DeviceChannelIdentifier { get; set; }
        DeviceChannelType DeviceChannelType { get; set; }
        IChannelMetaData MetaData { get; set; } // Meta data depends on Device Channel Type. Will define separate classes for it.
    }
}

