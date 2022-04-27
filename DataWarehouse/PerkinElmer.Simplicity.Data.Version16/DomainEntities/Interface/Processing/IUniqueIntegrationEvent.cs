namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing
{
	public interface IUniqueIntegrationEvent
	{
		IntegrationEventType EventType { get; set; }

        double StartTime { get; set; }

        int EventId { get; set; }
	}
}