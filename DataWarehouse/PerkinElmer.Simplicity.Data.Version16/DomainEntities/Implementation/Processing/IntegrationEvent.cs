using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
    internal class IntegrationEvent : IIntegrationEvent
    {
        public IntegrationEventType EventType { get; set; }
        public double StartTime { get; set; }
        public double? EndTime { get; set; }
        public double? Value { get; set; }
        public int EventId { get; set; }

        public bool Equals(IIntegrationEvent other)
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
            return Equals((IntegrationEvent) obj);
        }
    }
}
