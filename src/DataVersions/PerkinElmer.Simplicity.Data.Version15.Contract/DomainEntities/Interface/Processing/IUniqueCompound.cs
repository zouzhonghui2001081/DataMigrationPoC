namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
    public interface IUniqueCompound
    {
        System.Guid CompoundGuid { get; set; }
        string CompoundName { get; set; }

        double ExpectedRetentionTime { get; set; }
    }
}
