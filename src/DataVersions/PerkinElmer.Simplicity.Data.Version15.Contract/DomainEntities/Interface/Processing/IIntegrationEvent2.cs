namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing
{
	public interface IIntegrationEvent2 : IUniqueIntegrationEvent
	{
		double? EndTime { get; set; }

		double? Value { get; set; }
	}
}