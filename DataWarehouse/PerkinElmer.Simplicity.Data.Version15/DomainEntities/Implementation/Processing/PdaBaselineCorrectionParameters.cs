using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    internal class PdaBaselineCorrectionParameters : IPdaBaselineCorrectionParameters
	{
        public BaselineCorrectionType CorrectionType { get; set; }
        public double? SelectedSpectrumTimeInSeconds { get; set; }
        public double? RangeStartInSeconds { get; set; }
        public double? RangeEndInSeconds { get; set; }

        public bool Equals(IPdaBaselineCorrectionParameters other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return CorrectionType.Equals(other.CorrectionType) && 
                   SelectedSpectrumTimeInSeconds.Equals(other.SelectedSpectrumTimeInSeconds) &&
                   RangeStartInSeconds.Equals(other.RangeStartInSeconds) &&
                   RangeEndInSeconds.Equals(other.RangeEndInSeconds);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((IPdaBaselineCorrectionParameters) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = CorrectionType.GetHashCode();
                hashCode = (hashCode * 397) ^ (SelectedSpectrumTimeInSeconds.HasValue ? SelectedSpectrumTimeInSeconds.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RangeStartInSeconds.HasValue ? RangeStartInSeconds.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (RangeEndInSeconds.HasValue ? RangeEndInSeconds.GetHashCode() : 0);
                return hashCode;
            }
        }

        public object Clone()
        {
            return (IPdaBaselineCorrectionParameters) this.MemberwiseClone();
        }
    }
}
