using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail
{
    public class AuditTrailTarget
    {
        public static void CreateAuditTrailLogs(PostgresqlTargetContext targetContext, IList<AuditTrailLogEntry> auditTrailLogs)
        {
            if (auditTrailLogs == null) return;
            using (var dbConnection = new NpgsqlConnection(targetContext.AuditTrailConnection))
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
