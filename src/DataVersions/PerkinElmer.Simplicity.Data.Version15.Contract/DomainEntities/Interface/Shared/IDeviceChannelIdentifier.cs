using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
    public interface IDeviceChannelIdentifier : IEquatable<IDeviceChannelIdentifier>, ICloneable
    {
        IDeviceIdentifier DeviceIdentifier { get; set; }
        IChannelIdentifier1 ChannelIdentifier { get; set; }
    }
}