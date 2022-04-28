using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
	public class SequenceSourceBlockCreator : SourceBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override MigrationVersions SourceVersion => MigrationVersions.Version15;

        public override IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<SequenceMigrationData>();
            var actionBlock = new ActionBlock<SourceParamBase>(sourceParamBase =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var sequenceDao = new SequenceDao();
                    var sequenceSampleDao = new SequenceSampleDao();
                    switch (sourceParamBase.SourceKeyType)
                    {
                        case SourceParamTypes.ProjectGuid:
                            if (sourceParamBase is ProjectSourceParams projectParams)
                            {
                                var projectGuid = projectParams.ProjectGuid;
                                var sequences = sequenceDao.GetSequenceInfos(connection, projectGuid);
                                foreach (var sequence in sequences)
                                {
                                    sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                                    var sequenceSource = new SequenceMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        Sequence = sequence,
                                        AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, sequence.Guid.ToString(), EntityTypeConstants.Sequence)
                                    };
                                    bufferBlock.Post(sequenceSource);
                                }
                            }

                            break;
                        case SourceParamTypes.ProjectAndEntitiesGuid:
                            if (sourceParamBase is ProjectSourceEntitiesParams projectEntitiesParams)
                            {
                                var projectGuid = projectEntitiesParams.ProjectGuid;
                                var sequenceIds = projectEntitiesParams.EntityGuids;
                                foreach (var sequenceId in sequenceIds)
                                {
                                    var sequence = sequenceDao.GetSequenceInfo(connection, projectGuid, sequenceId);
                                    sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                                    var sequenceSource = new SequenceMigrationData
                                    {
                                        ProjectGuid = projectGuid,
                                        Sequence = sequence,
                                        AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, sequence.Guid.ToString(), EntityTypeConstants.Sequence)
                                    };
                                    bufferBlock.Post(sequenceSource);
                                }
                            }
                            break;
                    }

                    connection.Close();
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get sequence source by project id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
