using System;
using System.Collections.Generic;
using System.Linq;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class SuitabilityMethod : ISuitabilityMethod
    {
        public bool Enabled { get; set; }
        public PharmacopeiaType SelectedPharmacopeiaType { get; set; }
        public bool IsEfficiencyInPlates { get; set; }
        public double ColumnLength { get; set; }
        public double SignalToNoiseWindowStart { get; set; }
        public double SignalToNoiseWindowEnd { get; set; }
        public bool SignalToNoiseWindowEnabled { get; set; }
        public bool PerformAdditionalSearchForNoiseWindow { get; set; } = true;
        public bool AnalyzeAdjacentPeaks { get; set; }
        public VoidTimeType VoidTimeType { get; set; } = VoidTimeType.UseFirstPeak;
        public double VoidTimeCustomValueInSeconds { get; set; }

        public 
            IDictionary<Guid /*CompoundGuid*/, 
                IDictionary<PharmacopeiaType, 
                    IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> CompoundPharmacopeiaDefinitions { get; set; }
        public bool Equals(ISuitabilityMethod other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
        
            return Enabled == other.Enabled 
                   && SelectedPharmacopeiaType == other.SelectedPharmacopeiaType 
                   && IsEfficiencyInPlates == other.IsEfficiencyInPlates 
                   && ColumnLength.Equals(other.ColumnLength) 
                   && SignalToNoiseWindowStart.Equals(other.SignalToNoiseWindowStart) 
                   && SignalToNoiseWindowEnd.Equals(other.SignalToNoiseWindowEnd) 
                   && SignalToNoiseWindowEnabled == other.SignalToNoiseWindowEnabled
                   && PerformAdditionalSearchForNoiseWindow == other.PerformAdditionalSearchForNoiseWindow
                   && AnalyzeAdjacentPeaks == other.AnalyzeAdjacentPeaks 
                   && VoidTimeType == other.VoidTimeType
                   && VoidTimeCustomValueInSeconds == other.VoidTimeCustomValueInSeconds
                   && CompoundPharmacopeiaDefinitionsAreEquals(CompoundPharmacopeiaDefinitions, other.CompoundPharmacopeiaDefinitions);
        }

        private bool CompoundPharmacopeiaDefinitionsAreEquals(IDictionary<Guid, IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>> left, 
            IDictionary<Guid,IDictionary<PharmacopeiaType,IDictionary<SuitabilityParameter,ISuitabilityParameterCriteria>>> right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
        
            if (left.Count != right.Count)
                return false;

            // Compounds -> PharmaType -> SuitParam -> SuitParamCriteria
            return left.All(kvp => right.ContainsKey(kvp.Key) && PharmasAreEqual(kvp.Value, right[kvp.Key]));
        }

        private bool PharmasAreEqual(IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>> left, 
            IDictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>> right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
        
            if (left.Count != right.Count)
                return false;

            // PharmaType -> SuitParam -> SuitParamCriteria
            return left.All(kvp => right.ContainsKey(kvp.Key) && SuitParamsAreEqual(kvp.Value, right[kvp.Key]));
        }

        private bool SuitParamsAreEqual(IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria> left, 
            IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria> right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
        
            // SuitParam -> SuitParamCriteria
            if (left.Count != right.Count)
                return false;

            return left.All(kvp => right.ContainsKey(kvp.Key) && kvp.Value.Equals(right[kvp.Key]));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ISuitabilityMethod) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Enabled.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) SelectedPharmacopeiaType;
                hashCode = (hashCode * 397) ^ IsEfficiencyInPlates.GetHashCode();
                hashCode = (hashCode * 397) ^ ColumnLength.GetHashCode();
                hashCode = (hashCode * 397) ^ SignalToNoiseWindowStart.GetHashCode();
                hashCode = (hashCode * 397) ^ SignalToNoiseWindowEnd.GetHashCode();
                hashCode = (hashCode * 397) ^ SignalToNoiseWindowEnabled.GetHashCode();
                hashCode = (hashCode * 397) ^ PerformAdditionalSearchForNoiseWindow.GetHashCode();
                hashCode = (hashCode * 397) ^ AnalyzeAdjacentPeaks.GetHashCode();
                hashCode = (hashCode * 397) ^ (CompoundPharmacopeiaDefinitions != null ? CompoundPharmacopeiaDefinitions.GetHashCode() : 0);
                return hashCode;
            }
        }

        public object Clone()
        {
            var cloned = new SuitabilityMethod();
            cloned.Enabled = Enabled;
            cloned.SelectedPharmacopeiaType = SelectedPharmacopeiaType;
            cloned.IsEfficiencyInPlates = IsEfficiencyInPlates;
            cloned.ColumnLength = ColumnLength;
            cloned.SignalToNoiseWindowStart = SignalToNoiseWindowStart;
            cloned.SignalToNoiseWindowEnd = SignalToNoiseWindowEnd;
            cloned.SignalToNoiseWindowEnabled = SignalToNoiseWindowEnabled;
            cloned.PerformAdditionalSearchForNoiseWindow = PerformAdditionalSearchForNoiseWindow;
            cloned.AnalyzeAdjacentPeaks = AnalyzeAdjacentPeaks;
            cloned.VoidTimeType = VoidTimeType;
            cloned.VoidTimeCustomValueInSeconds = VoidTimeCustomValueInSeconds;

            if (CompoundPharmacopeiaDefinitions != null)
            {
                cloned.CompoundPharmacopeiaDefinitions = new
                    Dictionary<Guid,
                        IDictionary<PharmacopeiaType,
                            IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>>();
            
                // Compounds -> PharmaType -> SuitParam -> SuitParamCriteria
                foreach (var compGuid in CompoundPharmacopeiaDefinitions.Keys)
                {
                    var pharmas = CompoundPharmacopeiaDefinitions[compGuid];
                    var clonedPharmas = new Dictionary<PharmacopeiaType, IDictionary<SuitabilityParameter, ISuitabilityParameterCriteria>>();
                    cloned.CompoundPharmacopeiaDefinitions[compGuid] = clonedPharmas;

                    foreach (var pharmaType in pharmas.Keys)
                    {
                        var suitParams = pharmas[pharmaType];
                        var clonedSuitParams = new Dictionary<SuitabilityParameter, ISuitabilityParameterCriteria>();
                        clonedPharmas[pharmaType] = clonedSuitParams;

                        foreach (var suitParamType in suitParams.Keys)
                        {
                            clonedSuitParams[suitParamType] = (ISuitabilityParameterCriteria) suitParams[suitParamType].Clone();
                        }
                    }
                }
            }

            return cloned;
        }
    }
}