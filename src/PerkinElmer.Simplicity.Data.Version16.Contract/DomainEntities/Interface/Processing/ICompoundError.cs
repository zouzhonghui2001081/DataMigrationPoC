namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public interface ICompoundError : IUniqueCompound
    {
        ErrorCodes ErrorCode { get; set; }
    }
}
