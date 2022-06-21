
namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public interface IStreamData : IStreamDataInfo
    {
        byte[] Data { get; set; }
    }
}