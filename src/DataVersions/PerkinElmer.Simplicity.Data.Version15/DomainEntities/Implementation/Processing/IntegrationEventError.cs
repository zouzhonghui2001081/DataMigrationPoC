using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Processing
{
    internal class IntegrationEventError : IIntegrationEventError
    {
        public IntegrationEventType EventType { get; set; }
        public double StartTime { get; set; }
        public double? EndTime { get; set; }
        public double? Value { get; set; }
        public int EventId { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorCodes ErrorCode { get; set; }
        public bool ManualEvent { get; set; }
        public Guid BatchRunChannelGuid { get; set; }
        public IIntegrationEventError ConflictingEvent { get; set; }

        public virtual bool Equals(IIntegrationEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return EventType == other.EventType 
                   && StartTime.Equals(other.StartTime) 
                   && EndTime.Equals(other.EndTime) 
                   && Value.Equals(other.Value) 
                   && EventId == other.EventId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IntegrationEventError) obj);
        }
    }
}
