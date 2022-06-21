using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail
{
    internal class EntityAssociatedAuditTrailTarget
    {
        public static void CreateAuditTrailLogs(string auditTrailConnection, IList<AuditTrailLogEntry> auditTrailLogs)
        {
            if (auditTrailLogs == null) return;
            using (var dbConnection = new NpgsqlConnection(auditTrailConnection))
            {
                var auditTrailDataDao = new AuditTrailDao();
                var entityVersionDao = new EntityVersionLogDao();
                foreach (var auditTrailLog in auditTrailLogs)
                {
                    if (auditTrailLog.VersionDiffEntry != null)
                        auditTrailLog.ItemVersionId = entityVersionDao.Insert(dbConnection, auditTrailLog.VersionDiffEntry);
                    auditTrailDataDao.CreateLog(dbConnection, auditTrailLog);
                }
            }
        }
    }
}
