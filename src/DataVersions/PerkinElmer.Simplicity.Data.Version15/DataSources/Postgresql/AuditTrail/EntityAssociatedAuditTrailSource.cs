using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail
{
    internal class EntityAssociatedAuditTrailSource
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
                    if (auditTrailEntity.ItemVersionId > 0)
                        auditTrailEntity.VersionDiffEntry = entityVersionLogDao.Get(dbConnection, auditTrailEntity.ItemVersionId);
                    auditTrailsData.Add(auditTrailEntity);
                }
                return auditTrailsData;
            }
        }
    }
}
