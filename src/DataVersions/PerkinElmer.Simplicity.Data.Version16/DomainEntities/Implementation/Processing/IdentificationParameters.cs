using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class IdentificationParameters : IIdentificationParameters
    {
        public double ExpectedRetentionTime { get; set; }
        public double RetentionTimeWindowAbsolute { get; set; }
        public double RetentionTimeWindowInPercents { get; set; }
        public double RetTimeWindowStart { get; set; }
        public double RetTimeWindowEnd { get; set; }
        public bool IsRetTimeReferencePeak { get; set; }
        public Guid RetTimeReferencePeakGuid { get; set; }
        public int RetentionIndex { get; set; }
        public bool UseClosestPeak { get; set; }
        public int Index { get; set; }
        public bool? IsIntStdReferencePeak { get; set; }
        public Guid IntStdReferenceGuid { get; set; }
        public bool IsRrtReferencePeak { get; set; }

        public object Clone()
        {
            IIdentificationParameters clonedIdentificationParameters = (IIdentificationParameters)this.MemberwiseClone();
            return clonedIdentificationParameters;
        }

        public bool Equals(IIdentificationParameters other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return EqualsForCalculatedDoubles(ExpectedRetentionTime, other.ExpectedRetentionTime)
                   && RetentionTimeWindowAbsolute.Equals(other.RetentionTimeWindowAbsolute)
                   && RetentionTimeWindowInPercents.Equals(other.RetentionTimeWindowInPercents)
                   && IsRetTimeReferencePeak == other.IsRetTimeReferencePeak
                   && RetentionIndex == other.RetentionIndex
                   && UseClosestPeak == other.UseClosestPeak
                   && IsIntStdReferencePeak == other.IsIntStdReferencePeak
                   && IsRrtReferencePeak == other.IsRrtReferencePeak
                   && IntStdReferenceGuid.Equals(other.IntStdReferenceGuid)
                   && RetTimeReferencePeakGuid.Equals(other.RetTimeReferencePeakGuid);

            //&& IntStdReferenceGuid.Equals(other.IntStdReferenceGuid)
            //&& RetTimeReferencePeakGuid.Equals(other.RetTimeReferencePeakGuid)
        }

        private bool EqualsForCalculatedDoubles(double d1, double d2)
        {
            const double maxAcceptableRoundingError = 1e-7;
            return Math.Abs(d1 - d2) < maxAcceptableRoundingError;
        }

        public bool IsEqual(IIdentificationParameters other)
        {
            if (other == null)
                 return false;

            return EqualsForCalculatedDoubles(ExpectedRetentionTime, other.ExpectedRetentionTime)
                && RetentionTimeWindowAbsolute.Equals(other.RetentionTimeWindowAbsolute)
                && RetentionTimeWindowInPercents.Equals(other.RetentionTimeWindowInPercents)
                && IsRetTimeReferencePeak == other.IsRetTimeReferencePeak
                && RetentionIndex == other.RetentionIndex
                && UseClosestPeak == other.UseClosestPeak
                && IsIntStdReferencePeak == other.IsIntStdReferencePeak
                && IsRrtReferencePeak == other.IsRrtReferencePeak;
        }

    }
}
