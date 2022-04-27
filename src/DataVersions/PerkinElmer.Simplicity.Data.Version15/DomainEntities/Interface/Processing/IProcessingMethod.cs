using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
	public interface IProcessingMethod : ICloneable, IEquatable<IProcessingMethod>
    {
		IProcessingMethodInfo Info { get; set; }
	    bool ForExternalMethod { get; set; }
		IList<IProcessingDeviceMethod> ProcessingDeviceMethods { get; set; }
	    IList<IChannelMethod> ChannelMethods { get; set; }
		ICalibrationGlobalParameters CalibrationGlobalParameters { get; set; }
        IPdaApexOptimizedParameters ApexOptimizedParameters { get; set; }
        IList<ICompound> Compounds { get; set; }
        IReadOnlyList<ICompoundGroup> CompoundGroups { get; }
        IList<ISpectrumMethod> SpectrumMethods { get; set; }
        IDictionary<Guid, ICompoundCalibrationResults> CompoundCalibrationResultsMap { get; set; } //uses calibrationCompoundGuid to map CalibrationCompound to its results
		IDictionary<Guid, ICalibrationBatchRunInfo> CalibrationBatchRunInfos { get; set; } //uses BatchRunGuid as Key
		ISuitabilityMethod SuitabilityMethod { get; set; }
        bool IsEqual(IProcessingMethod other);
    }
}