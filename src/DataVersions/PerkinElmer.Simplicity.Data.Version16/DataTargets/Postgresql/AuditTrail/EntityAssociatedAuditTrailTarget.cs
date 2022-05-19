﻿using System.Collections.Generic;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Version;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail
{
    public class EntityAssociatedAuditTrailTarget
    {
        public static void CreateAuditTrailLogs(IList<AuditTrailLogEntry> auditTrailLogs)
        {
            if (auditTrailLogs == null) return;
            using (var dbConnection = new NpgsqlConnection(Version16Host.AuditTrailConnection))
            {
                var auditTrailDataDao = new AuditTrailDao();
                var entityVersionDao = new EntityVersionLogDao();
                foreach (var auditTrailLog in auditTrailLogs)
                {
                    if (auditTrailLog.VersionDiffEntry != null)
                        entityVersionDao.Insert(dbConnection, auditTrailLog.VersionDiffEntry);
                    auditTrailDataDao.CreateLog(dbConnection, auditTrailLog);
                }
            }
        }
    }
}
