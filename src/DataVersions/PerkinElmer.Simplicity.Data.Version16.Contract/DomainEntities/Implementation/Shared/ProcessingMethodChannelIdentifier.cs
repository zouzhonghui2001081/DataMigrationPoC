using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared
{
    internal class ProcessingMethodChannelIdentifier : IProcessingMethodChannelIdentifier
    {
        public ProcessingMethodChannelIdentifier(string deviceClass, int deviceIndex, IProcessingMethodDataChannelDescriptor processingMethodDataChannelDescriptor, int processingMethodChannelIndex)
        {
            DeviceClass = deviceClass;
            DeviceIndex = deviceIndex;
            ProcessingMethodDataChannelDescriptor = processingMethodDataChannelDescriptor;
            ProcessingMethodChannelIndex = processingMethodChannelIndex;
        }

        public string DeviceClass { get; }
        public int DeviceIndex { get; }
        public IProcessingMethodDataChannelDescriptor ProcessingMethodDataChannelDescriptor { get; }
        public int ProcessingMethodChannelIndex { get; }
        public string GetDisplayName()
        {
            var (dataTypeDisplayName, metaDataDisplayName) = ProcessingMethodDataChannelDescriptor.GetDisplayName();
            
            // Should the ProcessingMethodDataChannelDescriptor.DataChannelType exposed on interface for selecting device
            // specific format?
            if (dataTypeDisplayName.Equals("GC", StringComparison.OrdinalIgnoreCase))
            {
                return FormatGCDisplayName(dataTypeDisplayName, DeviceIndex, metaDataDisplayName, ProcessingMethodChannelIndex);
            }
            if (dataTypeDisplayName.Equals("MUV", StringComparison.OrdinalIgnoreCase))
            {
                return FormatMUVDisplayName(dataTypeDisplayName, DeviceIndex, metaDataDisplayName, ProcessingMethodChannelIndex);
            }
            return FormatDisplayName(dataTypeDisplayName, DeviceIndex, metaDataDisplayName, ProcessingMethodChannelIndex);
        }

        private static string FormatDisplayName(string dataChannelType, int deviceIndex, string metaData, int similarityIndex)
        {
            var name = dataChannelType;
            if (deviceIndex != 0)
                name += (deviceIndex + 1);
            if (!string.IsNullOrEmpty(metaData))
                name += $"-{metaData}";
            if (similarityIndex != 0)
                name += $"({similarityIndex + 1})";
            return name;
        }

        private static string FormatGCDisplayName(string dataChannelType, int deviceIndex, string metaData, int similarityIndex)
        {
            string name = string.Empty;
            // DataChannelType and Device Index will be used in future
            //if (deviceIndex != 0)
            //{
            //    name = dataChannelType;
            //    name += $"{(deviceIndex + 1)}-";
            //}

            name += metaData;
            name += $"{similarityIndex + 1}";
            return name;
        }

        private static string FormatMUVDisplayName(string dataChannelType, int deviceIndex, string metaData, int similarityIndex)
        {
            var name = dataChannelType;
            if (similarityIndex != 0)
                name += $"{similarityIndex + 1}";
            if (!string.IsNullOrEmpty(metaData))
                name += $"-{metaData}";

            return name;
        }

        public bool Equals(IProcessingMethodChannelIdentifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return DeviceClass == other.DeviceClass && DeviceIndex == other.DeviceIndex && Equals(ProcessingMethodDataChannelDescriptor, other.ProcessingMethodDataChannelDescriptor) && ProcessingMethodChannelIndex == other.ProcessingMethodChannelIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcessingMethodChannelIdentifier) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (DeviceClass != null ? DeviceClass.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ DeviceIndex;
                hashCode = (hashCode * 397) ^ (ProcessingMethodDataChannelDescriptor != null ? ProcessingMethodDataChannelDescriptor.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ProcessingMethodChannelIndex;
                return hashCode;
            }
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }
    }

    //public class ProcessingMethodChannelIdentifier : IProcessingMethodChannelIdentifier
    //{
    //    public string DeviceClass { get; set ; }
    //    public int DeviceIndex { get; set; }
    //    public DeviceChannelType DeviceDataType { get ; set ; }
    //    public IChannelMetaData DeviceMetaData { get; set; }
    //    public ExtractionType ExtractionType { get; set; }
    //    public IExtractedChannelMetaData ExtractionMetaData { get; set; }
    //    public PostProcessingChannelType PostProcessingType { get ; set ; }
    //    public IPostProcessingMetaData PostProcessingMetaData { get; set ; }
    //    public int ProcessingChannelIndex { get; set; }
    //    public object Clone()
    //    {
    //        ProcessingMethodChannelIdentifier processingMethodChannelIdentifier =
    //            new ProcessingMethodChannelIdentifier()
    //            {
    //                DeviceClass = DeviceClass,
    //                DeviceIndex = DeviceIndex,
    //                DeviceDataType = DeviceDataType,
    //                DeviceMetaData = DeviceMetaData,
    //                ExtractionType = ExtractionType,
    //                ExtractionMetaData = ExtractionMetaData,
    //                ProcessingChannelIndex = ProcessingChannelIndex
    //            };

    //        return processingMethodChannelIdentifier;
    //    }
        
    //    public bool Equals(IProcessingMethodChannelIdentifier other)
    //    {
	   //     return false;
	   //     // if (ReferenceEquals(null, other)) return false;
	   //     // if (ReferenceEquals(this, other)) return true;
	   //     //
	   //     // return DeviceClass.Equals(other.DeviceClass) &&
	   //     //        DeviceIndex.Equals(other.DeviceIndex) &&
	   //     //        DeviceDataType == other.DeviceDataType &&
	   //     //        DeviceMetaData.Equals(other.DeviceMetaData) &&
	   //     //        ExtractionType == other.ExtractionType
	   //     //        && Equals(ExtractionMetaData, other.ExtractionMetaData) &&
	   //     //        ProcessingChannelIndex == other.ProcessingChannelIndex;
    //    }
        
    //    public string GetDisplayName()
    //    {
    //        if (DeviceMetaData is IGCChannelMetaData
    //            gcChannelMetaData)
    //        {
    //            return gcChannelMetaData.DetectorType + ProcessingChannelIndex;
    //        }

    //        if (DeviceMetaData is IUVChannelMetaData uvChannelMetaData)
    //        {
    //            if (DeviceClass != null && DeviceClass.Contains("MULTI"))
    //            {
    //               // deviceClass = "MUV";
    //            }
    //            return DeviceClass + " - " + uvChannelMetaData.WavelengthInNanometers + (uvChannelMetaData.Programmed ? " (P)" : "");
    //        }

    //        if (DeviceMetaData is IPdaChannelMetaData)
    //        {
               
    //            if (ExtractionMetaData is IPdaExtractedChannelMetaDataSimple
    //                extractedChannelMetaData)
    //            {
    //                if (ExtractionType == ExtractionType.PdaSimple)
    //                    return $"{DeviceClass}-{extractedChannelMetaData.Wavelength:F0}";

    //                if (ExtractionType == ExtractionType.PdaApexOptimized)
    //                    return $"{DeviceClass}-{extractedChannelMetaData.Wavelength:F0}-Apex Optimized";
    //            }

    //            if (ExtractionMetaData is IPdaExtractedChannelMetaDataMic)
    //            {
    //                if (ExtractionType == ExtractionType.PdaMic)
    //                    return $"{DeviceClass}-MIC";
    //            }
    //        }

    //        if (DeviceMetaData is IFLChannelMetaData
    //            flChannelMetaData)
    //        {
    //            return "FL - " + flChannelMetaData.ExcitationInNanometers + "/" +
    //                   flChannelMetaData.EmissionInNanometers + (flChannelMetaData.Programmed ? " (P)" : "");
    //        }

    //        if (DeviceMetaData is IRIChannelMetaData
    //            riChannelMetaData)
    //        {
    //            return "RI";
    //        }

    //        if (DeviceMetaData is ITemperatureChannelMetaData
    //            tempChannelMetaData)
    //        {
    //            return tempChannelMetaData.Name;
    //        }

    //        if (DeviceMetaData is IPressureChannelMetaData
    //            pressChannelMetaData)
    //        {
    //            return pressChannelMetaData.Name;
    //        }

    //        return DeviceClass + ProcessingChannelIndex;
    //    }
    //}
}
