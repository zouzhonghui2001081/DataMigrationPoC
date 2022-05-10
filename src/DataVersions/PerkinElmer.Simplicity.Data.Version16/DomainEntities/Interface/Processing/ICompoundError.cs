namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
    public interface ICompoundError : IUniqueCompound
    {
        ErrorCodes ErrorCode { get; set; }
    }
}
