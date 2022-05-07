using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using Npgsql;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext;
using PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql.Chromatography
{
    public class AnalysisResultSetTargetBlockCreator: TargetBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override TargetType TargetType => TargetType.Posgresql;

        public override MigrationVersion TargetVersion => MigrationVersion.Version15;

        public override ITargetBlock<MigrationDataBase> CreateTargetBlock(TargetContextBase targetContext)
        {
            if (!(targetContext is PostgresqlTargetContext postgresqlTargetContext))
                throw new ArgumentException(nameof(targetContext));

            var actionBlock = new ActionBlock<MigrationDataBase>(versionData =>
            {
                if (versionData == null || versionData.MigrationDataTypes != MigrationDataTypes.AnlysisResultSet) return;
                if (versionData.MigrationVersion != MigrationVersion.Version15) return;
                if (!(versionData is AnalysisResultSetMigrationData analysisResultSetData)) return;

                using (var connection = new NpgsqlConnection(postgresqlTargetContext.ChromatographyConnection))
                {
                    if (connection.State != ConnectionState.Open) connection.Open();
                    CreateAnalysisResultSet(connection, analysisResultSetData);
                    EntityAssociatedAuditTrailTarget.CreateAuditTrailLogs(postgresqlTargetContext, analysisResultSetData.AuditTrailLogs);
                    connection.Close();
                }
            }, targetContext.BlockOption);

            actionBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"Create analysis result set targets finished with status {_.Status}.");
            });

            return actionBlock;
        }

        internal static void CreateAnalysisResultSet(IDbConnection connection,
            AnalysisResultSetMigrationData analysisResultSetMigrationData)
        {
            var projectDao = new ProjectDao();
            var analysisResultSetDao = new AnalysisResultSetDao();
            var batchRunChannelMapDao = new BatchRunChannelMapDao();
            var manualOverrideMapDao = new ManualOverrideMapDao();
            var brChannelsWithExceededNumberOfPeaksDao = new BrChannelsWithExceededNumberOfPeaksDao();
            var compoundSuitabilityResultDao = new CompoundSuitabilitySummaryResultDao();

            var project = projectDao.GetProject(connection, analysisResultSetMigrationData.ProjectGuid);
            if (project == null) throw new ArgumentNullException(nameof(project));
            if(analysisResultSetDao.IsExists(connection, project.Guid, analysisResultSetMigrationData.AnalysisResultSet.Name))
                return;
            

            //Analysis result set
            var analysisResultSet = analysisResultSetMigrationData.AnalysisResultSet;
            analysisResultSet.ProjectId = project.Id;
            analysisResultSetDao.Create(connection, analysisResultSet);

            //Batch result set
            foreach (var batchResultSet in analysisResultSetMigrationData.BatchResultSetData)
                BatchResultSetTargetBlockCreator.CreateBatchResultSet(connection, batchResultSet);
                

            //Batch run channel maps
            foreach (var batchRunChannelMap in analysisResultSetMigrationData.BatchRunChannelMaps)
            {
                batchRunChannelMap.AnalysisResultSetId = analysisResultSet.Id;
                batchRunChannelMapDao.Create(connection, batchRunChannelMap);
            }

            //Manual override maps
            foreach (var manualOverrideMap in analysisResultSetMigrationData.ManualOverrideMaps)
            {
                manualOverrideMap.AnalysisResultSetId = analysisResultSet.Id;
                manualOverrideMapDao.Create(connection, manualOverrideMap);
            }

            //Branch channels with exceeded number of peaks
            foreach (var brChannelsWithExceededNumberOfPeaks in analysisResultSetMigrationData
                .BrChannelsWithExceededNumberOfPeaks)
                brChannelsWithExceededNumberOfPeaks.AnalysisResultSetId = analysisResultSet.Id;
            brChannelsWithExceededNumberOfPeaksDao.Create(connection,
                analysisResultSetMigrationData.BrChannelsWithExceededNumberOfPeaks);

            //Compound Suitability Summary Results
            foreach (var compoundSuitabilitySummaryResult in analysisResultSetMigrationData.CompoundSuitabilitySummaryResults)
                compoundSuitabilitySummaryResult.AnalysisResultSetId = analysisResultSet.Id;
            compoundSuitabilityResultDao.Create(connection, analysisResultSetMigrationData.CompoundSuitabilitySummaryResults);

            CreateBatchRunAnalysisResultData(connection, analysisResultSetMigrationData.ProjectGuid, analysisResultSet.Id,
                analysisResultSetMigrationData.BatchRunAnalysisResults);

            if(analysisResultSetMigrationData.ReviewApproveData != null)
                EntityAssociatedReviewApproveTarget.CreateReviewApproveEntity(connection, analysisResultSetMigrationData.ReviewApproveData);
        }

        public static void CreateBatchRunAnalysisResultData(IDbConnection connection, Guid projectGuid,
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
                sequenceSampleInfoModifiableDao.SaveSequenceSampleInfoModifiable(connection,
                    batchRunAnalysisResultData.SequenceSampleInfoModifiable);
                batchRunAnalysisResult.SequenceSampleInfoModifiableId = sequenceSampleInfoModifiable.Id;

                //Processing method
                var processingMethod = batchRunAnalysisResultData.ModifiableProcessingMethod;
                if (processingMethodIdsCreated.ContainsKey(batchRunAnalysisResult.ProcessingMethodModifiableId))
                    batchRunAnalysisResult.ProcessingMethodModifiableId =
                        processingMethodIdsCreated[batchRunAnalysisResult.ProcessingMethodModifiableId];
                else
                {
                    ProcessingMethodTargetBlockCreator.CreateModifiableProcessingMethod(connection, analysisResultSetId,
                        processingMethod);
                    processingMethodIdsCreated.Add(batchRunAnalysisResult.ProcessingMethodModifiableId,
                        processingMethod.Id);
                    batchRunAnalysisResult.ProcessingMethodModifiableId = processingMethod.Id;
                }

                //Batch run analysis results
                batchRunAnalysisResult.AnalysisResultSetId = analysisResultSetId;
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
