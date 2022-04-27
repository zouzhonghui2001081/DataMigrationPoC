
using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail
{
    public class EntityVersionLogEntry
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public string EntityId { get; set; }

        public string EntityType { get; set; }

        public long AfterChangeVersionNumber { get; set; }

        public string VersionData { get; set; }

        public string Description { get; set; }

        public DateTime CreationTimestamp { get; set; }

        public long BeforeChangeVersionNumber { get; set; }

        public string BeforeChangeEntityId { get; set; }

    }
}
