using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    public class CompoundLibraryTargetBlockCreator : TargetBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetTypes TargetType => TargetTypes.Posgresql;

        public override MigrationVersions TargetVersion => MigrationVersions.Version16;

        public override ITargetBlock<MigrationDataBase> CreateTarget(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
            {
                if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.CompoundLibrary) return;
                if (versionData.MigrationVersion != MigrationVersions.Version15) return;
                if (!(versionData is ProjectCompoundLibraryMigrationData compoundLibraryData)) return;

                using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var projectDao = new ProjectDao();
                    var projectCompoundLibraryDao = new ProjectCompoundLibraryDao();
                    var compoundLibraryItemDao = new CompoundLibraryItemDao();
                    var projectCompoundLibraryToLibraryItemMapDao = new ProjectCompoundLibraryToLibraryItemMapDao();

                    var project = projectDao.GetProject(connection, compoundLibraryData.ProjectGuid);
                    if (project == null) throw new ArgumentNullException(nameof(project));
                    compoundLibraryData.ProjectCompoundLibrary.ProjectId = project.Id;

                    projectCompoundLibraryDao.Create(connection, compoundLibraryData.ProjectCompoundLibrary);
                    compoundLibraryItemDao.Create(connection, compoundLibraryData.ProjectCompoundLibrary.CompoundLibraryItems);


                    var compoundLibraryDao = new ProjectCompoundLibraryDao();
                    var compoundLibrary = compoundLibraryDao.GetCompoundLibraryByLibraryGuid(connection, project.Guid, compoundLibraryData.ProjectCompoundLibrary.LibraryGuid);

                    foreach (var compoundLibraryItem in compoundLibraryData.ProjectCompoundLibrary.CompoundLibraryItems)
                    {
                        var map = new ProjectCompoundLibraryToLibraryItemMap
                        {
                            CompoundLibraryItemId = compoundLibraryItem.Id,
                            ProjectCompoundLibraryId = compoundLibrary.Id
                        };
                        projectCompoundLibraryToLibraryItemMapDao.Create(connection, map);
                    }

                    connection.Close();
                }
            }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create compound library targets finished with status {_.Status}.");
            });

            return actionBlock;
        }
    }
}
