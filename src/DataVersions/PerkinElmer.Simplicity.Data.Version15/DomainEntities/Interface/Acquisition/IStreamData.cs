namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IStreamData : IStreamDataInfo
    {
        byte[] Data { get; set; }
    }
}