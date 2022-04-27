namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing
{
    public interface ICompoundError : IUniqueCompound
    {
        ErrorCodes ErrorCode { get; set; }
    }
}
