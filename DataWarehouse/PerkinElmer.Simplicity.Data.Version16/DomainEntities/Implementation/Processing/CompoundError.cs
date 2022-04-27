using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    public class CompoundError : ICompoundError
    {
        public System.Guid CompoundGuid { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string CompoundName { get; set; }
        public double ExpectedRetentionTime { get ; set; }
    }
}
