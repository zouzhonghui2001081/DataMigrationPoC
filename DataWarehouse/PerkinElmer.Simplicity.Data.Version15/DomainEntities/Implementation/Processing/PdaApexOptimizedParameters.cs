using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    internal class PdaApexOptimizedParameters:IPdaApexOptimizedParameters
    {
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }
        public double WavelengthBandwidth { get; set; }
        public bool UseReference { get; set; }
        public double ReferenceWavelength { get; set; }
        public double ReferenceWavelengthBandwidth { get; set; }
        public bool ApplyBaselineCorrection { get; set; }
        public bool UseAutoAbsorbanceThreshold { get; set; }
        public double ManualAbsorbanceThreshold { get; set; }
        public bool Equals(IPdaApexOptimizedParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MinWavelength.Equals(other.MinWavelength) &&
                   MaxWavelength.Equals(other.MaxWavelength) &&
                   WavelengthBandwidth.Equals(other.WavelengthBandwidth) &&
                   UseReference== other.UseReference &&
                   ReferenceWavelength.Equals(other.ReferenceWavelength) &&
                   ReferenceWavelengthBandwidth.Equals(other.ReferenceWavelengthBandwidth) &&
                   ApplyBaselineCorrection == other.ApplyBaselineCorrection &&
                   UseAutoAbsorbanceThreshold == other.UseAutoAbsorbanceThreshold &&
                   ManualAbsorbanceThreshold.Equals(other.ManualAbsorbanceThreshold);
        }

        public bool IsEqual(IPdaApexOptimizedParameters other)
        {
            if (other == null) return false;

                        return MinWavelength.Equals(other.MinWavelength) &&
                   MaxWavelength.Equals(other.MaxWavelength) &&
                   WavelengthBandwidth.Equals(other.WavelengthBandwidth) &&
                   UseReference == other.UseReference &&
                   ReferenceWavelength.Equals(other.ReferenceWavelength) &&
                   ReferenceWavelengthBandwidth.Equals(other.ReferenceWavelengthBandwidth) &&
                   ApplyBaselineCorrection == other.ApplyBaselineCorrection &&
                   UseAutoAbsorbanceThreshold == other.UseAutoAbsorbanceThreshold &&
                   ManualAbsorbanceThreshold.Equals(other.ManualAbsorbanceThreshold);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IPdaApexOptimizedParameters)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = MinWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ MaxWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ ApplyBaselineCorrection.GetHashCode();
                hashCode = (hashCode * 397) ^ UseAutoAbsorbanceThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ ManualAbsorbanceThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ WavelengthBandwidth.GetHashCode();
                hashCode = (hashCode * 397) ^ UseReference.GetHashCode();
                hashCode = (hashCode * 397) ^ ReferenceWavelength.GetHashCode();
                hashCode = (hashCode * 397) ^ ReferenceWavelengthBandwidth.GetHashCode();
                return hashCode;
            }
        }

        public object Clone()
        {
            return (IPdaApexOptimizedParameters)MemberwiseClone();
        }
    }
}
