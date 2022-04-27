using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionMethodInfoWithVersionState : AcquisitionMethodInfo, IAcquisitionMethodVersionInfo
    {
        public ReviewApproveVersionState VersionState { get; set; } = ReviewApproveVersionState.Unknown;
    }
}
