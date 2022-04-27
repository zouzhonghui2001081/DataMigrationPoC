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
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    public class SequenceSource : SourceBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override SourceTypes SourceType => SourceTypes.Posgresql;

        public override ReleaseVersions SourceReleaseVersion => ReleaseVersions.Version16;

        public override Version SchemaVersion => SchemaVersions.ChromatographySchemaVersion16;

        public override IPropagatorBlock<Guid, MigrationDataBase> CreateSourceByProjectId(SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<SequenceMigrationData>();
            var actionBlock = new ActionBlock<Guid>(projectId =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    var sequenceDao = new SequenceDao();
                    var sequenceSampleDao = new SequenceSampleDao();

                    var sequences = sequenceDao.GetSequenceInfos(connection, projectId);
                    foreach (var sequence in sequences)
                    {
                        sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                        var sequenceSource = new SequenceMigrationData
                        {
                            ProjectGuid = projectId,
                            Sequence = sequence,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, sequence.Guid.ToString(), EntityTypeConstants.Sequence)
                        };
                        bufferBlock.Post(sequenceSource);
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

        public override IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase> CreateSourceByIds(
            SourceContextBase sourceContext)
        {
            if (!(sourceContext is PostgresqlSourceContext posgresqlContext))
                throw new ArgumentException(nameof(sourceContext));

            var bufferBlock = new BufferBlock<SequenceMigrationData>();
            var actionBlock = new ActionBlock<Tuple<Guid, IList<Guid>>>(parameters =>
            {
                using (var connection = new NpgsqlConnection(posgresqlContext.ChromatographyConnection))
                {
                    var projectId = parameters.Item1;
                    var sequenceIds = parameters.Item2;
                    var sequenceDao = new SequenceDao();
                    var sequenceSampleDao = new SequenceSampleDao();
                    foreach (var sequenceId in sequenceIds)
                    {
                        var sequence = sequenceDao.GetSequenceInfo(connection, projectId, sequenceId);
                        sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                        var sequenceSource = new SequenceMigrationData
                        {
                            ProjectGuid = projectId,
                            Sequence = sequence,
                            AuditTrailLogs = AuditTrailSource.GetAuditTrail(posgresqlContext, sequence.Guid.ToString(), EntityTypeConstants.Sequence)
                        };
                        bufferBlock.Post(sequenceSource);
                    }
                }
            }, posgresqlContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Get sequence by project id and report template id finished with status {_.Status}.");
                bufferBlock.Complete();
            });
            return DataflowBlock.Encapsulate(actionBlock, bufferBlock);
        }
    }
}
