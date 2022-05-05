using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class ProjectSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersion SourceVersion => MigrationVersion.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {

                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();

                    var projectDao = new ProjectDao();
                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var project = projectDao.GetProject(connection, projectGuid);
                                var projectData = new ProjectMigrationData { Project = project };
                                //TODO : Audit Trail logs logic for project
                                if (project != null) bufferBlock.Post(projectData);
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectEntitiesSourceParams projectEntitiesParams)
                            {
                                var projectGuids = projectEntitiesParams.EntityGuids;
                                foreach (var projectGuid in projectGuids)
                                {
                                    var project = projectDao.GetProject(connection, projectGuid);
                                    var projectData = new ProjectMigrationData { Project = project };
                                    //TODO : Audit Trail logs logic for project
                                    if (project != null) bufferBlock.Post(projectData);
                                }
                            }

                            break;
                    }


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
        
    }
}
