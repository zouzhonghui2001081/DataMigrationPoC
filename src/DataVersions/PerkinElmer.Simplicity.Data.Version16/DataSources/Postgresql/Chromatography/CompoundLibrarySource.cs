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
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class CompoundLibrarySource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectCompoundLibraryMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectGuid =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var compoundLibraryDao = new ProjectCompoundLibraryDao();
                    var compounds = compoundLibraryDao.GetAllLibraries(connection, projectGuid);
                    var compoundLibraryItemDao = new CompoundLibraryItemDao();

                    foreach (var compound in compounds)
                    {
                        compound.CompoundLibraryItems = compoundLibraryItemDao.GetProjectCompoundLibraryItems(connection, projectGuid, compound.LibraryGuid);
                        var compoundLibraryData = new ProjectCompoundLibraryMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ProjectCompoundLibrary = compound,
                        };
                        bufferBlock.Post(compoundLibraryData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get compound library source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectCompoundLibraryMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                var projectGuid = parameters.Item1;
                var compoundLibraryIds = parameters.Item2;
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var compoundLibraryDao = new ProjectCompoundLibraryDao();
                    var compoundLibraryItemDao = new CompoundLibraryItemDao();
                    foreach (var compoundLibraryId in compoundLibraryIds)
                    {
                        var compound = compoundLibraryDao.GetCompoundLibraryByLibraryGuid(connection, projectGuid, compoundLibraryId);
                        compound.CompoundLibraryItems = compoundLibraryItemDao.GetProjectCompoundLibraryItems(connection, projectGuid, compound.LibraryGuid);
                        var compoundLibraryData = new ProjectCompoundLibraryMigrationData
                        {
                            ProjectGuid = projectGuid,
                            ProjectCompoundLibrary = compound
                        };
                        bufferBlock.Post(compoundLibraryData);
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get compound library by project id and compound library id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }

        internal static IList<SnapshotCompoundLibraryData> CreateCompoundLibraryData(IDbConnection connection, Guid projectGuid, Guid analysisResultSetGuid)
        {
            var compoundLibraryData = new List<SnapshotCompoundLibraryData>();
            var snapshotCompoundLibraryDao = new SnapshotCompoundLibraryDao();
            var compoundLibraryItemDao = new CompoundLibraryItemDao();

            var snapshotCompoundLibraries = snapshotCompoundLibraryDao.GetAllLibraries(connection, projectGuid, analysisResultSetGuid);
            foreach (var snapshotCompoundLibrary in snapshotCompoundLibraries)
            {
                var compoundLibraryItems = compoundLibraryItemDao.GetAnalysisResultSetCompoundLibraryItems(connection, projectGuid, analysisResultSetGuid, snapshotCompoundLibrary.LibraryGuid);
                compoundLibraryData.Add(new SnapshotCompoundLibraryData
                {
                    SnapshotCompoundLibrary = snapshotCompoundLibrary,
                    CompoundLibraryItems = compoundLibraryItems
                });
            }

            return compoundLibraryData;
        }
    }
}
