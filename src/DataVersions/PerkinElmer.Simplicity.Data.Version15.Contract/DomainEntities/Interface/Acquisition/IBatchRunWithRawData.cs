namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunWithRawData : IBatchRunBase
    {
        IStreamData[] StreamData { get; set; }
    }
}