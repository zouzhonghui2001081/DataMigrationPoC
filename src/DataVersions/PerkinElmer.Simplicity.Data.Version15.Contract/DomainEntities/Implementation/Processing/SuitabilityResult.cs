using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    internal class SuitabilityResult : ISuitabilityResult
    {
        public Guid PeakGuid { get; set; }
        public Guid CompoundGuid { get; set; }
        public string PeakName { get; set; }
        public double PeakRetentionTime { get; set; }
        public SuitabilityParameterResult RetentionTime { get; set; }
        public SuitabilityParameterResult Area { get; set; }
        public SuitabilityParameterResult Height { get; set; }
        public SuitabilityParameterResult TheoreticalPlatesN { get; set; }
        public SuitabilityParameterResult TheoreticalPlatesNTan { get; set; }
        public SuitabilityParameterResult TailingFactorSymmetry { get; set; }
        public SuitabilityParameterResult RelativeRetention { get; set; }
        public SuitabilityParameterResult CapacityFactorKPrime { get; set; }
        public SuitabilityParameterResult Resolution { get; set; }
        public SuitabilityParameterResult UspResolution { get; set; }
        public SuitabilityParameterResult SignalToNoise { get; set; }
        public SuitabilityParameterResult PeakWidthAtBase { get; set; }
        public SuitabilityParameterResult PeakWidthAt5Pct { get; set; }
        public SuitabilityParameterResult PeakWidthAt10Pct { get; set; }
        public SuitabilityParameterResult PeakWidthAt50Pct { get; set; }
        public SuitabilityParameterResult RelativeRetentionTime { get; set; }
        
        public double? Noise { get; set; }
        public double? NoiseStart { get; set; }
        public double? NoiseGapStart { get; set; }
        public double? NoiseGapEnd { get; set; }
        public double? NoiseEnd { get; set; }
        public bool? SstFlag { get; set; }

        public object Clone()
        {
            return new SuitabilityResult
            {
                CompoundGuid = CompoundGuid,
                PeakRetentionTime = PeakRetentionTime,
                PeakName = PeakName,
                PeakGuid= PeakGuid,
                RetentionTime= RetentionTime,
                Area = Area,
                Height = Height,
                CapacityFactorKPrime = CapacityFactorKPrime,
                TheoreticalPlatesN = TheoreticalPlatesN,
                TheoreticalPlatesNTan = TheoreticalPlatesNTan,
                TailingFactorSymmetry = TailingFactorSymmetry,
                Resolution = Resolution,
                PeakWidthAtBase = PeakWidthAtBase,
                PeakWidthAt5Pct = PeakWidthAt5Pct,
                PeakWidthAt10Pct = PeakWidthAt10Pct,
                PeakWidthAt50Pct = PeakWidthAt50Pct,
                RelativeRetention = RelativeRetention,
                RelativeRetentionTime = RelativeRetentionTime,
                SignalToNoise = SignalToNoise,
                Noise = Noise,
                NoiseStart = NoiseStart,
                NoiseGapStart = NoiseGapStart,
                NoiseGapEnd = NoiseGapEnd, 
                NoiseEnd = NoiseEnd,
                UspResolution=UspResolution,
                SstFlag= SstFlag
            };
        }
    }
}
