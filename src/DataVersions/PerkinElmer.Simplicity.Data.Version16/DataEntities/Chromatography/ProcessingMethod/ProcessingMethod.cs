using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.CalibrationMethod;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod
{
    public class ProcessingMethod
	{
		public ProcessingMethod()
		{
			ChannelMethods = new List<ChannelMethod>();
			Compounds = new List<Compound>();
			CompoundCalibrationResults = new List<CompoundCalibrationResults>();
			SpectrumMethods = new List<SpectrumMethod>();
			CalibrationBatchRunInfos = new List<CalibrationBatchRunInfo>();
		}
		public long Id { get; set; }
		public Guid Guid { get; set; }
		public bool IsDefault { get; set; }
		public string Name { get; set; }
        public int? VersionNumber { get; set; }
        public DateTime CreatedDate { get; set; }
		public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime ModifiedDate { get; set; }
		public string ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }
        public int NumberOfLevels { get; set; }
		public string AmountUnits { get; set; }
		public int UnidentifiedPeakCalibrationType { get; set; }
		public double UnidentifiedPeakCalibrationFactor { get; set; }
		public Guid UnidentifiedPeakReferenceCompoundGuid { get; set; }
		public bool ModifiedFromOriginal { get; set; }
		public Guid? OriginalReadOnlyMethodGuid { get; set; }
		public List<ChannelMethod> ChannelMethods { get; set; }
		public List<Compound> Compounds { get; set; }
		public List<CompoundCalibrationResults> CompoundCalibrationResults { get; set; }
		public List<ProcessingDeviceMethod> ProcessingDeviceMethods { get; set; }
		public List<SpectrumMethod> SpectrumMethods { get; set; }
		public string Description { get; set; }
		public PdaApexOptimizedParameters PdaApexOptimizedParameters { get; set; }
        public short ReviewApproveState { get; set; }
        public List<CalibrationBatchRunInfo> CalibrationBatchRunInfos { get; set; }
		public SuitabilityMethod SuitabilityMethod { get; set; }
	}
}
