using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
    internal class ModifiableProcessingMethod : ProcessingMethod, IModifiableProcessingMethod
	{
		public bool ModifiedFromOriginal { get; set; }
		public Guid OriginalReadOnlyMethodGuid { get; set; }
		public void CloneFromOriginal(IProcessingMethod originalMethod, bool cloneReviewApproveState = false)
	    {
	        Info = (IProcessingMethodInfo)originalMethod.Info.Clone();
            if (cloneReviewApproveState)
            {
                Info.ReviewApproveState = originalMethod.Info.ReviewApproveState;
            }

            Info.VersionNumber = originalMethod.Info.VersionNumber;
            ChannelMethods = originalMethod.ChannelMethods?.Select(cm => (IChannelMethod)cm.Clone()).ToList();
	        ForExternalMethod = originalMethod.ForExternalMethod;
            CalibrationGlobalParameters = (ICalibrationGlobalParameters)originalMethod.CalibrationGlobalParameters.Clone();
            ApexOptimizedParameters = (IPdaApexOptimizedParameters)originalMethod.ApexOptimizedParameters?.Clone();
            SuitabilityMethod = (ISuitabilityMethod)originalMethod.SuitabilityMethod?.Clone();

            foreach (var compound in originalMethod.Compounds)
            {
                Compounds.Add((ICompound)compound.Clone());
            }

            CompoundCalibrationResultsMap = new Dictionary<Guid, ICompoundCalibrationResults>();
            foreach (var obj in originalMethod.CompoundCalibrationResultsMap)
            {
                CompoundCalibrationResultsMap.Add(obj.Key, (ICompoundCalibrationResults)obj.Value.Clone());
            }

            CalibrationBatchRunInfos = new Dictionary<Guid, ICalibrationBatchRunInfo>();
            foreach (var obj in originalMethod.CalibrationBatchRunInfos)
            {
                CalibrationBatchRunInfos.Add(obj.Key, (ICalibrationBatchRunInfo)obj.Value.Clone());
            }

			SpectrumMethods = new List<ISpectrumMethod>();
			foreach (var spectrum in originalMethod.SpectrumMethods)
			{
				SpectrumMethods.Add((ISpectrumMethod)spectrum.Clone());
			}
        }
    }
}
