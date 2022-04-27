using System;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared
{
    public interface IProcessingMethodChannelIdentifier : IEquatable<IProcessingMethodChannelIdentifier>, ICloneable
    {
        string DeviceClass { get; } //PerkinElmer.Common.Contracts.DeviceClass 
        int DeviceIndex { get; }
        IProcessingMethodDataChannelDescriptor ProcessingMethodDataChannelDescriptor { get; } 
        int ProcessingMethodChannelIndex { get; } // if ProcessingMethodDataChannelDescriptor are equal, use it to differentiate 
        string GetDisplayName(); //UV-254P, UV2-254P, UV2-254P(2) //<DataType><DeviceIndex> - <MetaData> (<ChannelIndex>)
    }
}
