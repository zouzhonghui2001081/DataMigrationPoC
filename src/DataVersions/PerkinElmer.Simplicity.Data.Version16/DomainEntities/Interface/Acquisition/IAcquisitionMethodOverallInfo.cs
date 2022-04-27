using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
    public interface IAcquisitionMethodOverallInfo : IAcquisitionMethodInfo
    {
        OverallReviewApproveState OverallState { get; set; }
    }
}