
namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IStreamData : IStreamDataInfo
    {
        byte[] Data { get; set; }
    }
}