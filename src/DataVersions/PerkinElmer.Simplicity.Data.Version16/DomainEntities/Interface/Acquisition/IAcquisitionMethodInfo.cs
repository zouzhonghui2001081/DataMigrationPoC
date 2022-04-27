using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IAcquisitionMethodInfo : IPersistable
	{
		string[] Devices { get; set; }
		ReviewApproveState ReviewApproveState { get; set; }
        bool IsModifiedAfterSubmission { get; set; }
        int VersionNumber { get; set; }
    }
}