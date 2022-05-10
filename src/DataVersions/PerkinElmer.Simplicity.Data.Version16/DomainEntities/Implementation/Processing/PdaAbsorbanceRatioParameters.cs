using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class PdaAbsorbanceRatioParameters : IPdaAbsorbanceRatioParameters
	{
        public double WavelengthA { get; set; }
        public double WavelengthB { get; set; }
        public bool ApplyBaselineCorrection { get; set; }
        public bool UseAutoAbsorbanceThreshold { get; set; }
        public double ManualAbsorbanceThreshold { get; set; }

        public bool Equals(IPdaAbsorbanceRatioParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return WavelengthA.Equals(other.WavelengthA) &&
                   WavelengthB.Equals(other.WavelengthB) && 
                   ApplyBaselineCorrection == other.ApplyBaselineCorrection &&
                   UseAutoAbsorbanceThreshold == other.UseAutoAbsorbanceThreshold && 
                   ManualAbsorbanceThreshold.Equals(other.ManualAbsorbanceThreshold);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IPdaAbsorbanceRatioParameters) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = WavelengthA.GetHashCode();
                hashCode = (hashCode * 397) ^ WavelengthB.GetHashCode();
                hashCode = (hashCode * 397) ^ ApplyBaselineCorrection.GetHashCode();
                hashCode = (hashCode * 397) ^ UseAutoAbsorbanceThreshold.GetHashCode();
                hashCode = (hashCode * 397) ^ ManualAbsorbanceThreshold.GetHashCode();
                return hashCode;
            }
        }

        public object Clone()
        {
            return (IPdaAbsorbanceRatioParameters) this.MemberwiseClone();
        }
    }
}
