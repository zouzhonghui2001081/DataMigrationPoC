using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IAcquisitionMethodVersionInfo : IAcquisitionMethodInfo
    {
        ReviewApproveVersionState VersionState { get; set; }
    }
}