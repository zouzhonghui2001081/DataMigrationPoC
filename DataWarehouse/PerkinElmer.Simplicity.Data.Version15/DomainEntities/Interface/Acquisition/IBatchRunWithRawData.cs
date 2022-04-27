namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunWithRawData : IBatchRunBase
    {
        IStreamData[] StreamData { get; set; }
    }
}