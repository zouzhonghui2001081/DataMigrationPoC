using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Version;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail
{
    internal class EntityAssociatedAuditTrailTarget
    {
        public static void CreateAuditTrailLogs( IList<AuditTrailLogEntry> auditTrailLogs)
        {
            if (auditTrailLogs == null) return;
            using (var dbConnection = new NpgsqlConnection(Version15Host.AuditTrailConnection))
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
