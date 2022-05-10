using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class PdaWavelengthMaxParameters: IPdaWavelengthMaxParameters
    {
        public double MinWavelength { get; set; }
        public double MaxWavelength { get; set; }
        public bool ApplyBaselineCorrection { get; set; }
        public bool UseAutoAbsorbanceThreshold { get; set; }
        public double ManualAbsorbanceThreshold { get; set; }

        public bool Equals(IPdaWavelengthMaxParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return MinWavelength.Equals(other.MinWavelength) && MaxWavelength.Equals(other.MaxWavelength) && ApplyBaselineCorrection == other.ApplyBaselineCorrection && UseAutoAbsorbanceThreshold == other.UseAutoAbsorbanceThreshold && ManualAbsorbanceThreshold.Equals(other.ManualAbsorbanceThreshold);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IPdaWavelengthMaxParameters) obj);
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
                return hashCode;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
