using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Processing
{
    public class CompoundError : ICompoundError
    {
        public System.Guid CompoundGuid { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public string CompoundName { get; set; }
        public double ExpectedRetentionTime { get ; set; }
    }
}
