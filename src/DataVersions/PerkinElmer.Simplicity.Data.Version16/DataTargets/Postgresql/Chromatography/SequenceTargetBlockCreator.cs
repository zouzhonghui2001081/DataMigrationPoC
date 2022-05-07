﻿using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    public class SequenceTargetBlockCreator : TargetBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetType TargetType => TargetType.Posgresql;

        public override MigrationVersion TargetVersion => MigrationVersion.Version16;

        public override ITargetBlock<MigrationDataBase> CreateTargetBlock(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
            {
                if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.Sequence) return;
                if (versionData.MigrationVersion != MigrationVersion.Version16) return;
                if (!(versionData is SequenceMigrationData sequenceData)) return;

                using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectDao = new ProjectDao();
                    var sequenceDao = new SequenceDao();
                    var project = projectDao.GetProject(connection, sequenceData.ProjectGuid);
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    if (sequenceDao.IsExists(connection, project.Guid, sequenceData.Sequence.Name))
                        return;
                    sequenceData.Sequence.ProjectId = project.Id;
                    sequenceDao.CreateSequence(connection, sequenceData.ProjectGuid, sequenceData.Sequence);
                    EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext, sequenceData.AuditTrailLogs);
                    connection.Close();
                }
            }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create sequence targets finished with status {_.Status}.");
            });

            return actionBlock;
        }
    }
}
