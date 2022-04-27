using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public interface IAcquisitionMethodVersionInfo : IAcquisitionMethodInfo
    {
        ReviewApproveVersionState VersionState { get; set; }
    }
}