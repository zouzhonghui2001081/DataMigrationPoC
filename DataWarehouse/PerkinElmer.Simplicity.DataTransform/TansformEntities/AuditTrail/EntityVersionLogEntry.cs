using System;
using EntityVersionLogEntry15 = PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail.EntityVersionLogEntry;
using EntityVersionLogEntry16 = PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail.EntityVersionLogEntry;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail
{
    public class EntityVersionLogEntry
    {
        public static EntityVersionLogEntry16 Transform(EntityVersionLogEntry15 entityVersionLogEntry)
        {
            if (entityVersionLogEntry == null) return null;
            return new EntityVersionLogEntry16
            {
                Id = entityVersionLogEntry.Id,
                UniqueId = Guid.NewGuid(),
                EntityId = entityVersionLogEntry.EntityId,
                EntityType = entityVersionLogEntry.EntityType,
                AfterChangeVersionNumber = entityVersionLogEntry.AfterChangeVersionNumber,
                VersionData = entityVersionLogEntry.VersionData,
                Description = entityVersionLogEntry.Description,
                CreationTimestamp = entityVersionLogEntry.CreationTimestamp,
                BeforeChangeVersionNumber = entityVersionLogEntry.BeforeChangeVersionNumber,
                BeforeChangeEntityId = entityVersionLogEntry.BeforeChangeEntityId
            };
        }
    }
}
