namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public interface IUniqueCompound
    {
        System.Guid CompoundGuid { get; set; }
        string CompoundName { get; set; }

        double ExpectedRetentionTime { get; set; }
    }
}
