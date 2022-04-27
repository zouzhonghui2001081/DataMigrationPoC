using PerkinElmer.Domain.Contracts.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IBatchRunWithRawData : IBatchRunBase
    {
        IStreamData[] StreamData { get; set; }
    }
}