using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql.Chromatography
{
    internal class SequenceSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version16DataBase> GetSequence(Guid projectGuid, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var sequenceDao = new SequenceDao();
            var sequenceSampleDao = new SequenceSampleDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var sequences = sequenceDao.GetSequenceInfos(connection, projectGuid);
                foreach (var sequence in sequences)
                {
                    sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                    var sequenceSource = new SequenceData
                    {
                        ProjectGuid = projectGuid,
                        Sequence = sequence
                    };
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        sequenceSource.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString, sequence.Guid.ToString(), EntityTypeConstants.Sequence);
                    migrationEntities.Add(sequenceSource);
                }
                connection.Close();
            }

            return migrationEntities;

        }

        public static IList<Version16DataBase> GetSequence(Guid projectGuid, IList<Guid> sequenceGuids,
            PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version16DataBase>();
            var sequenceDao = new SequenceDao();
            var sequenceSampleDao = new SequenceSampleDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();

                foreach (var sequenceGuid in sequenceGuids)
                {
                    var sequence = sequenceDao.GetSequenceInfo(connection, projectGuid, sequenceGuid);
                    sequence.SequenceSampleInfos = sequenceSampleDao.GetSequenceSampleInfoBySequenceId(connection, sequence.Id);
                    var sequenceSource = new SequenceData
                    {
                        ProjectGuid = projectGuid,
                        Sequence = sequence
                    };
                    if (postgresqlSourceContext.IsIncludeAuditTrail)
                        sequenceSource.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(postgresqlSourceContext.AuditTrailConnectionString, sequence.Guid.ToString(), EntityTypeConstants.Sequence);

                    migrationEntities.Add(sequenceSource);
                }
                connection.Close();
            }

            return migrationEntities;
        }
    }
}
