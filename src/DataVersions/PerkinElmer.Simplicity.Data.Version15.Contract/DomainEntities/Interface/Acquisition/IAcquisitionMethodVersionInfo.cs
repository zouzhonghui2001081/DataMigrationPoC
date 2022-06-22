using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
    public interface IAcquisitionMethodVersionInfo : IAcquisitionMethodInfo
    {
        ReviewApproveVersionState VersionState { get; set; }
    }
}