namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunWithRawData : IBatchRunBase
    {
        IStreamData[] StreamData { get; set; }
    }
}