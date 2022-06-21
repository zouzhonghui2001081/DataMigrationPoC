using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionMethodInfoWithVersionState : AcquisitionMethodInfo, IAcquisitionMethodVersionInfo
    {
        public ReviewApproveVersionState VersionState { get; set; } = ReviewApproveVersionState.Unknown;
    }
}
