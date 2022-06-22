namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IStreamData : IStreamDataInfo
    {
        byte[] Data { get; set; }
    }
}