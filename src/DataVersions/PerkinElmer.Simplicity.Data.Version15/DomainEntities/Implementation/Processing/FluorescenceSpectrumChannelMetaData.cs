using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    public class FluorescenceSpectrumChannelMetaData : IFluorescenceSpectrumChannelMetaData, IEquatable<IFluorescenceSpectrumChannelMetaData>
    {
        public FluorescenceSpectrumChannelMetaData()
        {
        }

        public FluorescenceSpectrumChannelMetaData(string name)
        {
            ResponseUnit = "nm";
            Name = name;
        }

        public bool Equals(IFluorescenceSpectrumChannelMetaData other)
        {
            return string.Equals(ResponseUnit, other.ResponseUnit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FluorescenceSpectrumChannelMetaData) obj);
        }

        public override int GetHashCode()
        {
            return ResponseUnit.GetHashCode();
        }

        public string ResponseUnit { get; set; }
        public double DefaultMinYScale { get; set; }
        public double DefaultMaxYScale { get; set; }
        public double MinValidYValue { get; set; }
        public double MaxValidYValue { get; set; }
        public double SamplingRateInMilliseconds { get; set; }
        public string Name { get; set; }
        public int EMWavelengthIntervalInNanometer { get; set; }
        public int EXWaveLengthInNanometer { get; set; }
        public int EMWaveLengthStartInNanometer { get; set; }
        public int EMWaveLengthEndInNanometer { get; set; }
        public double SignalNoiseRatio { get; set; }
        public double PeakIntensityOfRamanScattering { get; set; }
        public double BaselineIntensityOfRamanScattering { get; set; }
        public int PeakWavelengthOfRamanScattering { get; set; }
        public string FlowCellType { get; set; }
        public double SignalToNoiseRatioSpecification { get; set; }
    }
}