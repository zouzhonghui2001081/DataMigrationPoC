using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
	public interface IIntegrationEventError : IIntegrationEvent
	{
		string ErrorMessage { get; set; }
	    ErrorCodes ErrorCode { get; set; }
	    bool ManualEvent { get; set; }
	    Guid BatchRunChannelGuid { get; set; }
	    IIntegrationEventError ConflictingEvent { get; set; }
	}
}