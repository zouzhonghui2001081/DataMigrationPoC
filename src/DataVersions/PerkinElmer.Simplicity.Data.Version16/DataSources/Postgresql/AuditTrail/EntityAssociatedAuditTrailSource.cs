using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail
{
    public class EntityAssociatedAuditTrailSource
    {
        public static IList<AuditTrailLogEntry> GetAuditTrail(string auditTrailConnection, string entityId,
            string entityType)
        {
            using (var dbConnection = new NpgsqlConnection(auditTrailConnection))
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
