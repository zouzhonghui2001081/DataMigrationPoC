using System;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    internal class SequenceTarget
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveSequence(SequenceData sequenceData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            var projectDao = new ProjectDao();
            var sequenceDao = new SequenceDao();
            var project = projectDao.GetProject(connection, sequenceData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));

            if (sequenceDao.IsExists(connection, project.Guid, sequenceData.Sequence.Name))
                return;
            sequenceData.Sequence.ProjectId = project.Id;
            sequenceDao.CreateSequence(connection, sequenceData.ProjectGuid, sequenceData.Sequence);
            EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext.AuditTrailConnectionString,sequenceData.AuditTrailLogs);
            connection.Close();
        }
    }
}
