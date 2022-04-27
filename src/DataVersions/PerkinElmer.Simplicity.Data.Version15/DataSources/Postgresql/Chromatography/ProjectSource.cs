using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
	public class ProjectSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version15;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion15;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateProjectSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                var projectDao = new ProjectDao();
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var project = projectDao.GetProject(connection, projectGuid);
                    var projectData = new ProjectMigrationData { Project = project };
                    //TODO : Audit Trail logs logic for project
                    if (project != null) bufferBlock.Post(projectData);

                    connection.Close();
                }

            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get project source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateEntitiesSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                var projectGuids = parameters.Item2;
                var projectDao = new ProjectDao();
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    foreach (var projectGuid in projectGuids)
                    {
                        var project = projectDao.GetProject(connection, projectGuid);
                        var projectData = new ProjectMigrationData { Project = project };
                            //TODO : Audit Trail logs logic for project
                            if (project != null) bufferBlock.Post(projectData);
                    }

                    connection.Close();
                }

            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get project source by project guid finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
