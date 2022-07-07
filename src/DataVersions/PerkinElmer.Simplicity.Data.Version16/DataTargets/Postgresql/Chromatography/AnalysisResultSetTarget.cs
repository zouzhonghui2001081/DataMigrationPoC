using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.Data.Version16.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Version.Context.TargetContext;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Postgresql.Chromatography
{
    internal class AnalysisResultSetTarget 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal static void SaveAnalysisResultSet(AnalysisResultSetData analysisResultSetData, PostgresqlTargetContext postgresqlTargetContext)
        {
            using var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnectionString);
            if (connection.State != ConnectionState.Open) connection.Open();
            CreateAnalysisResultSet(connection, analysisResultSetData);
            EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext.AuditTrailConnectionString, analysisResultSetData.AuditTrailLogs);
            connection.Close();
        }

        internal static void CreateAnalysisResultSet(IDbConnection connection,
            AnalysisResultSetData analysisResultSetData)
        {
            var projectDao = new ProjectDao();
            var analysisResultSetDao = new AnalysisResultSetDao();
            var batchRunChannelMapDao = new BatchRunChannelMapDao();
            var manualOverrideMapDao = new ManualOverrideMapDao();
            var brChannelsWithExceededNumberOfPeaksDao = new BrChannelsWithExceededNumberOfPeaksDao();
            var compoundSuitabilityResultDao = new CompoundSuitabilitySummaryResultDao();

            var project = projectDao.GetProject(connection, analysisResultSetData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));

            //Analysis result set
            var analysisResultSet = analysisResultSetData.AnalysisResultSet;
            analysisResultSet.ProjectId = project.Id;
            analysisResultSetDao.Create(connection, analysisResultSet);

            //Batch result set
            foreach (var batchResultSet in analysisResultSetData.BatchResultSetData)
                BatchResultSetTarget.CreateBatchResultSet(connection, batchResultSet);


            //Batch run channel maps
            foreach (var batchRunChannelMap in analysisResultSetData.BatchRunChannelMaps)
            {
                batchRunChannelMap.AnalysisResultSetId = analysisResultSet.Id;
                batchRunChannelMapDao.Create(connection, batchRunChannelMap);
            }

            //Manual override maps
            foreach (var manualOverrideMap in analysisResultSetData.ManualOverrideMaps)
            {
                manualOverrideMap.AnalysisResultSetId = analysisResultSet.Id;
                manualOverrideMapDao.Create(connection, manualOverrideMap);
            }

            //Branch channels with exceeded number of peaks
            foreach (var brChannelsWithExceededNumberOfPeaks in analysisResultSetData.BrChannelsWithExceededNumberOfPeaks)
                brChannelsWithExceededNumberOfPeaks.AnalysisResultSetId = analysisResultSet.Id;
            brChannelsWithExceededNumberOfPeaksDao.Create(connection,
                analysisResultSetData.BrChannelsWithExceededNumberOfPeaks);

            //Compound Suitability Summary Results
            foreach (var compoundSuitabilitySummaryResult in analysisResultSetData.CompoundSuitabilitySummaryResults)
                compoundSuitabilitySummaryResult.AnalysisResultSetId = analysisResultSet.Id;
            compoundSuitabilityResultDao.Create(connection, analysisResultSetData.CompoundSuitabilitySummaryResults);

            CreateBatchRunAnalysisResultData(connection, analysisResultSetData.ProjectGuid, analysisResultSet.Id,
                analysisResultSetData.BatchRunAnalysisResults);

            EntityAssociatedReviewApproveTarget.CreateReviewApproveEntity(connection, analysisResultSetData.ReviewApproveData);
        }

        internal static void CreateBatchRunAnalysisResultData(IDbConnection connection, Guid projectGuid,
            long analysisResultSetId,
            IList<BatchRunAnalysisResultData> batchRunAnalysisResultsData)
        {
            var sequenceSampleInfoModifiableDao = new SequenceSampleInfoModifiableDao();
            var batchRunAnalysisResultDao = new BatchRunAnalysisResultDao();
            var calculatedChannelDataDao = new CalculatedChannelDataDao();
            var runPeakResultDao = new RunPeakResultDao();
            var suitabilityResultDao = new SuitabilityResultDao();

            var processingMethodIdsCreated = new Dictionary<long /*Postgres method id*/, long /*Export method id*/>();
            foreach (var batchRunAnalysisResultData in batchRunAnalysisResultsData)
            {
                var batchRunAnalysisResult = batchRunAnalysisResultData.BatchRunAnalysisResult;

                //Sequence
                var sequenceSampleInfoModifiable = batchRunAnalysisResultData.SequenceSampleInfoModifiable;
                sequenceSampleInfoModifiable.AnalysisResultSetId = analysisResultSetId;
                sequenceSampleInfoModifiable.Id  = sequenceSampleInfoModifiableDao.SaveSequenceSampleInfoModifiable(connection, sequenceSampleInfoModifiable);
                batchRunAnalysisResult.SequenceSampleInfoModifiableId = sequenceSampleInfoModifiable.Id;

                //Processing method
                var processingMethod = batchRunAnalysisResultData.ModifiableProcessingMethod;
                if (processingMethodIdsCreated.ContainsKey(batchRunAnalysisResult.ProcessingMethodModifiableId))
                    batchRunAnalysisResult.ProcessingMethodModifiableId =
                        processingMethodIdsCreated[batchRunAnalysisResult.ProcessingMethodModifiableId];
                else
                {
                    ProcessingMethodTarget.CreateModifiableProcessingMethod(connection, analysisResultSetId,
                        processingMethod);
                    processingMethodIdsCreated.Add(batchRunAnalysisResult.ProcessingMethodModifiableId,
                        processingMethod.Id);
                    batchRunAnalysisResult.ProcessingMethodModifiableId = processingMethod.Id;
                }

                //Batch run analysis results
                batchRunAnalysisResult.AnalysisResultSetId = analysisResultSetId;
                batchRunAnalysisResult.SequenceSampleInfoModifiableId = sequenceSampleInfoModifiable.Id;
                batchRunAnalysisResult.Id =
                    batchRunAnalysisResultDao.SaveBatchRunAnalysisResult(connection, projectGuid,
                        batchRunAnalysisResult);

                //Calculated channel data
                foreach (var calculatedChannelData in batchRunAnalysisResultData.CalculatedChannelData)
                {
                    calculatedChannelData.CalculatedChannelData.BatchRunAnalysisResultId = batchRunAnalysisResult.Id;
                    calculatedChannelData.CalculatedChannelData.Id =
                        calculatedChannelDataDao.SaveCalculatedChannelData(connection,
                            calculatedChannelData.CalculatedChannelData);

                    //Run peak results
                    foreach (var runPeakResult in calculatedChannelData.RunPeakResults)
                    {
                        runPeakResult.CalculatedChannelDataId = calculatedChannelData.CalculatedChannelData.Id;
                        runPeakResultDao.SaveRunPeakResult(connection, runPeakResult);
                    }

                    //Suitability results
                    foreach (var suitabilityResult in calculatedChannelData.SuitabilityResults)
                    {
                        suitabilityResult.CalculatedChannelDataId = calculatedChannelData.CalculatedChannelData.Id;
                        suitabilityResultDao.Create(connection, suitabilityResult);
                    }
                }
            }
        }
    }
}
