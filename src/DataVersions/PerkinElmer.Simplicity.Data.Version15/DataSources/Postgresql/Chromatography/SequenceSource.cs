using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    internal class SequenceSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version15DataBase> GetSequence(Guid projectGuid, bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var sequenceDao = new SequenceDao();
            var sequenceSampleDao = new SequenceSampleDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
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
                    if (isIncludeAuditTrail)
                        sequenceSource.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(sequence.Guid.ToString(), EntityTypeConstants.Sequence);
                    migrationEntities.Add(sequenceSource);
                }
                connection.Close();
            }

            return migrationEntities;

        }

        public static IList<Version15DataBase> GetSequence(Guid projectGuid, IList<Guid> sequenceGuids,
            bool isIncludeAuditTrail)
        {
            var migrationEntities = new List<Version15DataBase>();
            var sequenceDao = new SequenceDao();
            var sequenceSampleDao = new SequenceSampleDao();
            using (var connection = new NpgsqlConnection(Version15Host.ChromatographyConnection))
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
                    if (isIncludeAuditTrail)
                        sequenceSource.AuditTrailLogs = EntityAssociatedAuditTrailSource.GetAuditTrail(sequence.Guid.ToString(), EntityTypeConstants.Sequence);

                    migrationEntities.Add(sequenceSource);
                }
                connection.Close();
            }

            return migrationEntities;
        }
    }
}
