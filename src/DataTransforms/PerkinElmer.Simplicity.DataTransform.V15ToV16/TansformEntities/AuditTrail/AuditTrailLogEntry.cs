using System;
using AuditTrailLogEntry15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail.AuditTrailLogEntry;
using AuditTrailLogEntry16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail.AuditTrailLogEntry;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail
{
    public class AuditTrailLogEntry
    {
        public static AuditTrailLogEntry16 Transform(AuditTrailLogEntry15 auditTrailLogEntry)
        {
            if (auditTrailLogEntry == null) return null;
            var auditTrailLogEntry16 = new AuditTrailLogEntry16
            {
                Id = auditTrailLogEntry.Id,
                UniqueId = Guid.NewGuid(),
                LogTime = auditTrailLogEntry.LogTime,
                ScopeType = auditTrailLogEntry.ScopeType,
                RecordType = auditTrailLogEntry.RecordType,
                EntityId = auditTrailLogEntry.EntityId,
                EntityType = auditTrailLogEntry.EntityType,
                ActionTypeId = auditTrailLogEntry.ActionTypeId,
                ActionType = auditTrailLogEntry.ActionType,
                ItemId = auditTrailLogEntry.ItemId,
                ItemName = auditTrailLogEntry.ItemName,
                ItemType = auditTrailLogEntry.ItemType,
                UserId = auditTrailLogEntry.UserId,
                UserLogin = auditTrailLogEntry.UserLogin,
                UserFullName = auditTrailLogEntry.UserFullName,
                UserRoleId = auditTrailLogEntry.UserRoleId,
                UserRole = auditTrailLogEntry.UserRole,
                ProjectId = auditTrailLogEntry.ProjectId,
                ProjectName = auditTrailLogEntry.ProjectName,
                WorkstationId = auditTrailLogEntry.WorkstationId,
                WorkstationName = auditTrailLogEntry.WorkstationName,
                InstrumentId = auditTrailLogEntry.InstrumentId,
                InstrumentName = auditTrailLogEntry.InstrumentName,
                Justification = auditTrailLogEntry.Justification,
                JustificationTimestamp = auditTrailLogEntry.JustificationTimestamp,
                Comment = auditTrailLogEntry.Comment,
                FullCount = auditTrailLogEntry.FullCount,
                VersionDiffEntry = EntityVersionLogEntry.Transform(auditTrailLogEntry.VersionDiffEntry)
            };
            if (auditTrailLogEntry16.VersionDiffEntry != null)
                auditTrailLogEntry16.ItemVersionId = auditTrailLogEntry16.VersionDiffEntry.UniqueId.ToString();
            return auditTrailLogEntry16;
        }
    }
}
