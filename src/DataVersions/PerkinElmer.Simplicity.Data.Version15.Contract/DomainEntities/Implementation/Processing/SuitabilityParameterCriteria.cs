using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    public class SuitabilityParameterCriteria : ISuitabilityParameterCriteria
    {
        public bool Enabled { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public double RsdLimit { get; set; }

        public object Clone()
        {
            var cloned = new SuitabilityParameterCriteria();
            cloned.Enabled = Enabled;
            cloned.LowerLimit = LowerLimit;
            cloned.UpperLimit = UpperLimit;
            cloned.RsdLimit = RsdLimit;

            return cloned;
        }

        public bool Equals(ISuitabilityParameterCriteria other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Enabled == other.Enabled
                   && LowerLimit.Equals(other.LowerLimit)
                   && UpperLimit.Equals(other.UpperLimit)
                   && RsdLimit.Equals(other.RsdLimit);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((ISuitabilityParameterCriteria) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Enabled.GetHashCode();
                hashCode = (hashCode * 397) ^ LowerLimit.GetHashCode();
                hashCode = (hashCode * 397) ^ UpperLimit.GetHashCode();
                hashCode = (hashCode * 397) ^ RsdLimit.GetHashCode();
                return hashCode;
            }
        }
    }
}