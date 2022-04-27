using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionMethodOverallInfo : AcquisitionMethodInfo, IAcquisitionMethodOverallInfo
    {
        public OverallReviewApproveState OverallState { get; set; } = OverallReviewApproveState.Unknown;
    }
}
