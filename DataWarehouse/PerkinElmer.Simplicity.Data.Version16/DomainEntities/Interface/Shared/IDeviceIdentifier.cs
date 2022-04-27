using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IDeviceIdentifier : IEquatable<IDeviceIdentifier>, ICloneable
    {
        string DeviceClass { get; set; } // GC, UV, PDA, FL, RI  
        int DeviceIndex { get; set; } // Within Device Class.. If we have 2 GC (even from two different vendors or same vendor), we will use this index to differentiate it
    }
}