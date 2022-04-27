using System;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared
{
    //TODO: After removing the old channel Identifier, rename this to IChannelIdentifier
    public interface IChannelIdentifier1 : IEquatable<IChannelIdentifier1>, ICloneable
    {
        int ChannelIndex { get; set; } // Zero Based index (For E.g, if we have 2 physical channels in Clarus GC, we will use 0 or 1 in this field). If it is a auxiliary channel, It starts from 0 as a separate list for auxiliary channels
        bool Auxiliary { get; set; } // Is it a auxiliary channel. 
    }
}