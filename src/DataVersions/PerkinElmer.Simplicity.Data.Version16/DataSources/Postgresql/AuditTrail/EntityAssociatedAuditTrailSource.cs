using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Version;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail
{
    public class EntityAssociatedAuditTrailSource
    {
        public static IList<AuditTrailLogEntry> GetAuditTrail(string entityId,
            string entityType)
        {
            using (var dbConnection = new NpgsqlConnection(Version16Host.AuditTrailConnection))
            {
                var auditTrailDao = new AuditTrailDao();
                var entityVersionLogDao = new EntityVersionLogDao();
                var auditTrailEntities = auditTrailDao.GetAuditTrailByEntity(dbConnection, entityId, entityType);
                if (auditTrailEntities == null) return null;
                var auditTrailsData = new List<AuditTrailLogEntry>();
                foreach (var auditTrailEntity in auditTrailEntities)
                {
                    if (string.IsNullOrEmpty(auditTrailEntity.ItemVersionId))
                        auditTrailEntity.VersionDiffEntry = entityVersionLogDao.Get(dbConnection, auditTrailEntity.UniqueId);
                }
                return auditTrailsData;
            }
        }
    }
}
