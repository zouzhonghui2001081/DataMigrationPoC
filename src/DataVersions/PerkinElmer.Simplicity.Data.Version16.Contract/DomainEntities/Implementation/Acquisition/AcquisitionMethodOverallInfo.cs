using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    internal class AcquisitionMethodOverallInfo : AcquisitionMethodInfo, IAcquisitionMethodOverallInfo
    {
        public OverallReviewApproveState OverallState { get; set; } = OverallReviewApproveState.Unknown;
    }
}
