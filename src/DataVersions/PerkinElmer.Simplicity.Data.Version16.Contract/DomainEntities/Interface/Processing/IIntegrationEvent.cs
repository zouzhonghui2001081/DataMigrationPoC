
using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public interface IIntegrationEvent : IUniqueIntegrationEvent, IEquatable<IIntegrationEvent>
    {
        double? EndTime { get; set; }

        double? Value { get; set; }
    }
}
