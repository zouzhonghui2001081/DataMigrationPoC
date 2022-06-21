﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Version.Context.SourceContext;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql.Chromatography
{
    internal class BatchResultSetSource 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static IList<Version15DataBase> GetBatchResultSets(Guid projectGuid, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version15DataBase>();
            var batchResultSetDao = new BatchResultSetDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                var batchResultSets = batchResultSetDao.GetAll(connection, projectGuid);
                foreach (var batchResultSet in batchResultSets)
                {
                    var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                    migrationEntities.Add(batchResultSetData);
                }
                connection.Close();
            }
            return migrationEntities;
        }

        public static IList<Version15DataBase> GetBatchResultSets( Guid projectGuid, IList<Guid> batchResultSetGuids, PostgresqlSourceContext postgresqlSourceContext)
        {
            var migrationEntities = new List<Version15DataBase>();
            var batchResultSetDao = new BatchResultSetDao();
            using (var connection = new NpgsqlConnection(postgresqlSourceContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
               
                foreach (var batchResultSetGuid in batchResultSetGuids)
                {
                    var batchResultSet = batchResultSetDao.Get(connection, projectGuid, batchResultSetGuid);
                    var batchResultSetData = CreateBatchResultSetData(connection, projectGuid, batchResultSet);
                    migrationEntities.Add(batchResultSetData);
                }
                connection.Close();
            }
            return migrationEntities;
        }

        internal static BatchResultSetData CreateBatchResultSetData(IDbConnection connection, Guid projectId, BatchResultSet batchResultSet)
        {
            if (batchResultSet == null) return null;
            var batchRunDao = new BatchRunDao();
            var sequenceSampleInfoDao = new SequenceSampleInfoBatchResultDao();
            var processingMethodBatchResultDao = new ProcessingMethodBatchResultDao();
            var namedContentDao = new NamedContentDao();
            var streamDataBatchResultDao = new StreamDataBatchResultDao();
            var batchResultDeviceModuleDetailsDao = new BatchResultDeviceModuleDetailsDao();
            var deviceDriverItemDetailsDao = new DeviceDriverItemDetailsDao();

            var batchResultSetData = new BatchResultSetData { ProjectGuid = projectId, BatchResultSet = batchResultSet };

            var batchRuns = batchRunDao.GetBatchRunsByBatchResultSetIdAndSequenceSampleInfoBatchResultId(connection, batchResultSet.Id);

            foreach (var batchRun in batchRuns)
            {
                var batchRunData = new BatchRunData
                {
                    BatchRun = batchRun,
                    AcquisitionMethod = AcquisitionMethodSource.GetAcqusitionMethod(connection, batchRun.AcquisitionMethodBatchResultId),
                    SequenceSampleInfoBatchResult = sequenceSampleInfoDao.GetSequenceSampleInfoBatchResultById(connection, batchRun.SequenceSampleInfoBatchResultId),
                    ProcessingMethod = processingMethodBatchResultDao.GetProcessingMethodById(connection, batchRun.ProcessingMethodBatchResultId),
                    NamedContents = namedContentDao.Get(connection, batchRun.Id),
                    StreamDataBatchResults = streamDataBatchResultDao.GetStreamInfo(connection, batchRun.Guid)
                };
                using (var _ = connection.BeginTransaction())
                {
                    foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
                    {
                        var valuesStreamData = streamDataBatchResultDao.GetStreamData(connection, batchRun.Guid, streamDataBatchResult.DeviceId, streamDataBatchResult.StreamIndex);
                        streamDataBatchResult.YData = valuesStreamData.data;
                    }
                }
                batchResultSetData.BatchRuns.Add(batchRunData);
            }

            batchResultSetData.DeviceModuleDetails = batchResultDeviceModuleDetailsDao.GetDeviceModules(connection, batchResultSet.Id);
            batchResultSetData.DeviceDriverItemDetails = deviceDriverItemDetailsDao.Get(connection, batchResultSet.Id);

            return batchResultSetData;
        }
    }
}
