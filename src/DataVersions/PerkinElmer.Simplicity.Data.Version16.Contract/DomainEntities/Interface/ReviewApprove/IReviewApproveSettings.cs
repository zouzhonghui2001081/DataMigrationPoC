namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove
{
	public interface IReviewApproveSettings
	{
		bool IsHideApproved { get; set; }
		bool IsShowMyTasks { get; set; }
		int ReviewRound { get; set; }
		int ApproveRound { get; set; }
	}
}
