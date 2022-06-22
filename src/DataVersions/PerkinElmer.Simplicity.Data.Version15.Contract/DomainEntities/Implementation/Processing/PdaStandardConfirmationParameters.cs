using System;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    internal class PdaStandardConfirmationParameters : IPdaStandardConfirmationParameters
	{
	    public Guid PdaStandardConfirmationGuid { get; set; }
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }
        public int MinimumDataPoints { get; set; }
        public double PassThreshold { get; set; }
        public bool ApplyBaselineCorrection { get; set; }
        public bool UseAutoAbsorbanceThresholdForSample { get; set; }
        public double ManualAbsorbanceThresholdForSample { get; set; }
        public bool UseAutoAbsorbanceThresholdForStandard { get; set; }
        public double ManualAbsorbanceThresholdForStandard { get; set; }
	    public StandardType StandardType { get; set; }

        public bool Equals(IPdaStandardConfirmationParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PdaStandardConfirmationGuid.Equals(other.PdaStandardConfirmationGuid) &&
                   StandardType.Equals(other.StandardType) &&
                   MinWavelength.Equals(other.MinWavelength) &&
                   MaxWavelength.Equals(other.MaxWavelength) &&
                   MinimumDataPoints == other.MinimumDataPoints &&
                   PassThreshold.Equals(other.PassThreshold) &&
                   ApplyBaselineCorrection == other.ApplyBaselineCorrection &&
                   UseAutoAbsorbanceThresholdForSample == other.UseAutoAbsorbanceThresholdForSample &&
                   ManualAbsorbanceThresholdForSample.Equals(other.ManualAbsorbanceThresholdForSample) &&
                   UseAutoAbsorbanceThresholdForStandard == other.UseAutoAbsorbanceThresholdForStandard &&
                   ManualAbsorbanceThresholdForStandard.Equals(other.ManualAbsorbanceThresholdForStandard);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IPdaStandardConfirmationParameters) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = PdaStandardConfirmationGuid.GetHashCode();
                hashCode = (hashCode * 397) ^ StandardType.GetHashCode();
                hashCode = (hashCode * 397) ^ MinWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ MaxWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ MinimumDataPoints;
                hashCode = (hashCode * 397) ^ PassThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ ApplyBaselineCorrection.GetHashCode();
                hashCode = (hashCode * 397) ^ UseAutoAbsorbanceThresholdForSample.GetHashCode();
                hashCode = (hashCode * 397) ^ ManualAbsorbanceThresholdForSample.GetHashCode();
                hashCode = (hashCode * 397) ^ UseAutoAbsorbanceThresholdForStandard.GetHashCode();
                hashCode = (hashCode * 397) ^ ManualAbsorbanceThresholdForStandard.GetHashCode();
                return hashCode;
            }
        }

        public object Clone()
        {
            return (IPdaStandardConfirmationParameters) this.MemberwiseClone();
        }
    }
}
