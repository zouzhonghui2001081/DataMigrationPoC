using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Domain.Contracts.Processing
{
	public interface IIntegrationEvent2 : IUniqueIntegrationEvent
	{
		double? EndTime { get; set; }

		double? Value { get; set; }
	}
}