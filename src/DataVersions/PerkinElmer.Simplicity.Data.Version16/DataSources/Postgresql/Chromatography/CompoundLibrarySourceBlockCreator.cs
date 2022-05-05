using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class CompoundLibrarySourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersion SourceVersion => MigrationVersion.Version16;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<ProjectCompoundLibraryMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var compoundLibraryDao = new ProjectCompoundLibraryDao();
                    var compoundLibraryItemDao = new CompoundLibraryItemDao();
                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var compounds = compoundLibraryDao.GetAllLibraries(connection, projectGuid);
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
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectEntitiesSourceParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var compoundLibraryIds = projectEntitiesParams.EntityGuids;
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
                            }

                            break;
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
