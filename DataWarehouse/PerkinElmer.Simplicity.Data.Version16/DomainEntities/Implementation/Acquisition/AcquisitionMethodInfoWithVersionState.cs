using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionMethodInfoWithVersionState : AcquisitionMethodInfo, IAcquisitionMethodVersionInfo
    {
        public ReviewApproveVersionState VersionState { get; set; } = ReviewApproveVersionState.Unknown;
    }
}
