using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    internal class ProcessingMethod : IProcessingMethod
    {
        public IProcessingMethodInfo Info { get; set; }
        public bool ForExternalMethod { get; set; }
        public IList<IProcessingDeviceMethod> ProcessingDeviceMethods { get; set; }
        public IList<IChannelMethod> ChannelMethods { get; set; } = new List<IChannelMethod>();
        public ICalibrationGlobalParameters CalibrationGlobalParameters { get; set; }
        public IPdaApexOptimizedParameters ApexOptimizedParameters { get; set; }
        public IList<ICompound> Compounds { get; set; } = new List<ICompound>();

        public IReadOnlyList<ICompoundGroup> CompoundGroups => Compounds.OfType<ICompoundGroup>().ToList();
        public IDictionary<Guid, ICompoundCalibrationResults> CompoundCalibrationResultsMap { get; set; } = new Dictionary<Guid, ICompoundCalibrationResults>();
        public IDictionary<Guid, ICalibrationBatchRunInfo> CalibrationBatchRunInfos { get; set; } = new Dictionary<Guid, ICalibrationBatchRunInfo>();
        public ISuitabilityMethod SuitabilityMethod { get; set; }
//        public ISuitabilityParameters SuitabilityParameters { get; set; }
        public IList<ISpectrumMethod> SpectrumMethods { get; set; } = new List<ISpectrumMethod>();

        public object Clone()
        {
            IProcessingMethod processingMethod = new ProcessingMethod
            {
                Info = (IProcessingMethodInfo)Info.Clone(),
                ForExternalMethod = ForExternalMethod,
            };
            processingMethod.ChannelMethods = new List<IChannelMethod>();
            foreach (var channelMethod in ChannelMethods)
            {
                processingMethod.ChannelMethods.Add((IChannelMethod)channelMethod.Clone());
            }

            processingMethod.CalibrationGlobalParameters = (ICalibrationGlobalParameters)CalibrationGlobalParameters?.Clone();
            processingMethod.ApexOptimizedParameters = (IPdaApexOptimizedParameters)ApexOptimizedParameters?.Clone();
            processingMethod.Compounds = new List<ICompound>();
            foreach (var compound in Compounds)
            {
                processingMethod.Compounds.Add((ICompound)compound.Clone());
            }

            processingMethod.SpectrumMethods = new List<ISpectrumMethod>();
            foreach (var spectrum in SpectrumMethods)
            {
                processingMethod.SpectrumMethods.Add((ISpectrumMethod)spectrum.Clone());
            }

            processingMethod.CompoundCalibrationResultsMap = new Dictionary<Guid, ICompoundCalibrationResults>();
            foreach (var obj in CompoundCalibrationResultsMap)
            {
                processingMethod.CompoundCalibrationResultsMap.Add(obj.Key, (ICompoundCalibrationResults)obj.Value.Clone());
            }

            processingMethod.CalibrationBatchRunInfos = new Dictionary<Guid, ICalibrationBatchRunInfo>();
            foreach (var calInfoKvp in CalibrationBatchRunInfos)
            {
                processingMethod.CalibrationBatchRunInfos.Add(calInfoKvp.Key, (ICalibrationBatchRunInfo)calInfoKvp.Value.Clone());
            }

            processingMethod.SuitabilityMethod = (ISuitabilityMethod) SuitabilityMethod?.Clone();

            return processingMethod;
        }

        public bool Equals(IProcessingMethod other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Info, other.Info)
                   && ForExternalMethod == other.ForExternalMethod
                   && Equals(ProcessingDeviceMethods, other.ProcessingDeviceMethods)
                   && Equals(ChannelMethods, other.ChannelMethods)
                   && Equals(CalibrationGlobalParameters, other.CalibrationGlobalParameters)
                   && Equals(ApexOptimizedParameters, other.ApexOptimizedParameters)
                   && Equals(Compounds, other.Compounds)
                   && Equals(CompoundCalibrationResultsMap, other.CompoundCalibrationResultsMap)
                   && Equals(CalibrationBatchRunInfos, other.CalibrationBatchRunInfos)
                   && Equals(SuitabilityMethod, other.SuitabilityMethod)
                   && Equals(SpectrumMethods, other.SpectrumMethods);
        }

        public bool IsEqual(IProcessingMethod other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            bool equal = CompareChannelLists(other.ChannelMethods)
                         && CalibrationGlobalParameters.IsEqual(other.CalibrationGlobalParameters)
                         && (ApexOptimizedParameters == null? other.ApexOptimizedParameters == null: ApexOptimizedParameters.IsEqual(other.ApexOptimizedParameters))
                         && CompareCompundLists(other.Compounds)
                         && CompareSpectrumMethodLists(other.SpectrumMethods)
                         && (SuitabilityMethod == null && other.SuitabilityMethod == null ||
                             SuitabilityMethod != null && other.SuitabilityMethod != null && 
                             SuitabilityMethod.Equals(other.SuitabilityMethod)
              );

            return equal;
        }

        private bool CompareSpectrumMethodLists(IList<ISpectrumMethod> otherSpectrumMethods)
        {
            bool isSpectrumMethodEqual = true;

            if (SpectrumMethods.Count != otherSpectrumMethods.Count)
                return false;

            for (int index = 0; index < SpectrumMethods.Count; index++)
            {
                isSpectrumMethodEqual = SpectrumMethods[index].IsEqual(otherSpectrumMethods[index]);
            }

            return isSpectrumMethodEqual;
        }
        private bool CompareChannelLists(IList<IChannelMethod> otherChannelMethods)
        {
            bool ischannelEqual = true;

            if (ChannelMethods.Count != otherChannelMethods.Count)
                return false;

            for (int index = 0; index < ChannelMethods.Count; index++)
            {
                var channelMethod = otherChannelMethods.FirstOrDefault(cm =>
                    cm.ChannelIdentifier.Equals(ChannelMethods[index].ChannelIdentifier));

                if (channelMethod == null)
                    return false;

                ischannelEqual = ChannelMethods[index].IsEqual(channelMethod);
                if (!ischannelEqual)
                    break;
            }
            
            return ischannelEqual;
        }
        private bool CompareCompundLists(IList<ICompound> otherCompounds)
        {
            bool isCompoundEqual = true;

            if (Compounds.Count != otherCompounds.Count)
                return false;

            for (int index = 0; index < Compounds.Count; index++)
            {
                isCompoundEqual = Compounds[index].IsEqual(otherCompounds[index]);
                if (!isCompoundEqual)
                {
                    break;
                }
            }
            return isCompoundEqual;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcessingMethod)obj);
        }
    }
}