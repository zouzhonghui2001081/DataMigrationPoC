using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Common.Postgresql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ProjectSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateProjectSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            throw new NotImplementedException();
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateEntitiesSource(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            throw new NotImplementedException();
        }

        //public override IPropagatorBlock<IList<Guid>, DataVersionBase> CreateSourceByProjectIds(DbContext dbContext, ExecutionDataflowBlockOptions blockOption)
        //{
        //    var projectDao = new ProjectDao();

        //    var bufferBlock = new BufferBlock<ProjectData>();
        //    var actionBlock = new ActionBlock<IList<Guid>>(projectGuids =>
        //    {
        //        using (var connection = new NpgsqlConnection(dbContext.ChromatographyConnection))
        //        {
        //            if (connection.State != ConnectionState.Open) connection.Open();
        //            foreach (var projectGuid in projectGuids)
        //            {
        //                var project = projectDao.GetProject(connection, projectGuid);
        //                var projectData = new ProjectData { Project = project };
        //                //TODO : Audit Trail logs logic for project
        //                if (project != null)
        //                    bufferBlock.Post(projectData);
        //            }

        //            connection.Close();
        //        }
        //    }, blockOption);

        //    actionBlock.Completion.ContinueWith(_ =>
        //    {
        //        Log.Info($"Get project source by project guid finished with status {_.Status}.");
        //        bufferBlock.Complete();
        //    });
        //    return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        //}

    }
}
