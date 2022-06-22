using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    internal class ProcessingMethodDataChannelDescriptor : IProcessingMethodDataChannelDescriptor, IEquatable<ProcessingMethodDataChannelDescriptor>
    {
        internal ProcessingMethodDataChannelDescriptor(ProcessingMethodDataChannelType dataChannelType, IProcessingMethodDataChannelMetaData dataChannelMetaData)
        {
            DataChannelType = dataChannelType;
            DataChannelMetaData = dataChannelMetaData;
        }

        public bool IsExtracted =>
            DataChannelType == ProcessingMethodDataChannelType.PdaExtracted ||
            DataChannelType == ProcessingMethodDataChannelType.PdaApexOptimized;

        public ProcessingMethodDataChannelType DataChannelType { get;  }
        public IProcessingMethodDataChannelMetaData DataChannelMetaData { get; } 

        public (string DataTypeDisplayName, string MetaDataDisplayName) GetDisplayName()
        {
            return (GetChannelTypeName(DataChannelType), DataChannelMetaData.GetDisplayName());
        }

        private string GetChannelTypeName(ProcessingMethodDataChannelType dataChannelType)
        {
            switch (dataChannelType)
            {
                case ProcessingMethodDataChannelType.GC:
                    return "GC";
                case ProcessingMethodDataChannelType.UV:
                    return "UV";
                case ProcessingMethodDataChannelType.MultiUV:
                    return "MUV";
                case ProcessingMethodDataChannelType.FL:
                    return "FL";
                case ProcessingMethodDataChannelType.RI:
                    return "RI";
                case ProcessingMethodDataChannelType.AToD:
                    return "AtoD";
                case ProcessingMethodDataChannelType.PdaExtracted:
                case ProcessingMethodDataChannelType.PdaApexOptimized:
                    return "PDA";
                case ProcessingMethodDataChannelType.PdaMic:
                    return "PDA-MIC";
                default:
                    throw new ArgumentOutOfRangeException(nameof(dataChannelType), dataChannelType, null);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcessingMethodDataChannelDescriptor) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) DataChannelType * 397) ^ (DataChannelMetaData != null ? DataChannelMetaData.GetHashCode() : 0);
            }
        }

        public bool Equals(ProcessingMethodDataChannelDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DataChannelType == other.DataChannelType && Equals(DataChannelMetaData, other.DataChannelMetaData);
        }
    }
}