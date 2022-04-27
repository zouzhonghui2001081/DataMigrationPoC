using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IDeviceChannelIdentifier : IEquatable<IDeviceChannelIdentifier>, ICloneable
    {
        IDeviceIdentifier DeviceIdentifier { get; set; }
        IChannelIdentifier1 ChannelIdentifier { get; set; }
    }
}