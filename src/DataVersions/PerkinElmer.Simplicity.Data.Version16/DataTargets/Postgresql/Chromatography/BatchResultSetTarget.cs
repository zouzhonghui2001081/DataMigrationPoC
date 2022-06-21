using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    internal class BatchResultSetTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveBatchResultSet(BatchResultSetData batchResultSetData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString))
            {
                if (connection.State != ConnectionState.Open) connection.Open();
                CreateBatchResultSet(connection, batchResultSetData);
                connection.Close();
            }
        }

        internal static void CreateBatchResultSet(IDbConnection connection, BatchResultSetData batchResultSetData)
        {
            var projectDao = new ProjectDao();
            var batchResultSetDao = new BatchResultSetDao();
            var batchRunDao = new BatchRunDao();
            var sequenceSampleInfoBatchResultDao = new SequenceSampleInfoBatchResultDao();
            var namedContentDao = new NamedContentDao();
            var streamDataBatchResultDao = new StreamDataBatchResultDao();
            var deviceDriverItemDetailsDao = new DeviceDriverItemDetailsDao();
            var batchResultDeviceModuleDetailsDao = new BatchResultDeviceModuleDetailsDao();

            
            using (var transaction = connection.BeginTransaction())
            {
                var project = projectDao.GetProject(connection, batchResultSetData.ProjectGuid);
                if (project == null) throw new ArgumentNullException(nameof(project));
                batchResultSetData.BatchResultSet.ProjectId = project.Id;

                if (batchResultSetDao.IsExists(connection, batchResultSetData.ProjectGuid, batchResultSetData.BatchResultSet.Guid))
                    return;

                var batchResultSet = batchResultSetDao.CreateBatchResultSet(connection, batchResultSetData.BatchResultSet);
                var batchResultSetId = batchResultSet.Id;

                foreach (var deviceModuleDetail in batchResultSetData.DeviceModuleDetails)
                    deviceModuleDetail.BatchResultSetId = batchResultSetId;
                batchResultDeviceModuleDetailsDao.Create(connection, batchResultSetData.DeviceModuleDetails.ToArray());

                foreach (var deviceDriverItemDetail in batchResultSetData.DeviceDriverItemDetails)
                    deviceDriverItemDetail.BatchResultSetId = batchResultSetId;
                deviceDriverItemDetailsDao.Create(connection, batchResultSetData.DeviceDriverItemDetails.ToArray());

                var acquisitionMethods = new List<(Guid acquisistionMethodGuid, long id)>();
                var processingMethods = new List<(Guid processingMethodGuid, long id)>();

                foreach (var batchRunData in batchResultSetData.BatchRuns)
                {
                    batchRunData.BatchRun.BatchResultSetId = batchResultSetId;

                    //Sequence Sample Info
                    if (batchRunData.SequenceSampleInfoBatchResult != null)
                    {
                        batchRunData.SequenceSampleInfoBatchResult.BatchResultSetId = batchResultSetId;
                        sequenceSampleInfoBatchResultDao.CreateSequenceSampleInfo(connection, batchRunData.SequenceSampleInfoBatchResult);
                        batchRunData.BatchRun.SequenceSampleInfoBatchResultId = batchRunData.SequenceSampleInfoBatchResult.Id;
                    }

                    //Acquisition Method
                    var acquisitionMethod = batchRunData.AcquisitionMethod;
                    if (acquisitionMethods.Exists(a => a.acquisistionMethodGuid == acquisitionMethod.Guid))
                    {
                        var acquisitionMethodValues =
                            acquisitionMethods.Find(a => a.acquisistionMethodGuid == acquisitionMethod.Guid);
                        batchRunData.BatchRun.AcquisitionMethodBatchResultId = acquisitionMethodValues.id;
                    }
                    else
                    {
                        var acquisitionMethodId = AcquisitionMethodTarget.CreateAcquisitionMethod(connection, batchResultSet.Id, acquisitionMethod);
                        batchRunData.BatchRun.AcquisitionMethodBatchResultId = acquisitionMethodId;
                        acquisitionMethods.Add((acquisitionMethod.Guid, acquisitionMethodId));
                    }

                    //Processing Method
                    var processingMethod = batchRunData.ProcessingMethod;
                    if (processingMethods.Exists(p => p.processingMethodGuid == processingMethod.Guid))
                    {
                        var processingMethodIdPair = processingMethods.Find(p => p.processingMethodGuid == processingMethod.Guid);
                        batchRunData.BatchRun.ProcessingMethodBatchResultId = processingMethodIdPair.id;
                    }
                    else
                    {
                        var processingMethodId = ProcessingMethodTarget.CreateProcessingMethod(connection, batchResultSetId, processingMethod);
                        batchRunData.BatchRun.ProcessingMethodBatchResultId = processingMethodId;
                        processingMethods.Add((processingMethod.Guid, processingMethodId));
                    }


                    batchRunDao.Create(connection, batchRunData.BatchRun);

                    //Name Content
                    foreach (var namedContent in batchRunData.NamedContents)
                        namedContent.BatchRunId = batchRunData.BatchRun.Id;
                    namedContentDao.Create(connection, batchRunData.NamedContents.ToArray());

                    //Stream Data Batch Result
                    var manager = new NpgsqlLargeObjectManager((NpgsqlConnection)connection);
                    foreach (var streamDataBatchResult in batchRunData.StreamDataBatchResults)
                    {
                        streamDataBatchResult.BatchRunId = batchRunData.BatchRun.Id;
                        if (streamDataBatchResult.UseLargeObjectStream)
                        {
                            uint oid = manager.Create();
                            streamDataBatchResult.LargeObjectOid = oid;
                            var largeObjectStream = manager.OpenReadWrite(oid);
                            largeObjectStream.Write(streamDataBatchResult.YData, 0, streamDataBatchResult.YData.Length);
                            largeObjectStream.Close();
                            streamDataBatchResult.YData = null;
                        }
                        streamDataBatchResultDao.InsertStreamData(connection, streamDataBatchResult);
                    }

                }

                transaction.Commit();
            }
        }
    }
}
