using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;
using BatchRunAnalysisResultData15 = PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography.BatchRunAnalysisResultData;
using BatchRunAnalysisResultData16 = PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography.BatchRunAnalysisResultData;
using CalculatedChannelCompositeData15 = PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography.CalculatedChannelCompositeData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class AnalysisResultSetDataTransform : TransformBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override MigrationVersion FromVersion => MigrationVersion.Version15;

        public override MigrationVersion ToVersion => MigrationVersion.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var arsTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(
                fromVersionData =>
                {
                    if (fromVersionData.MigrationVersion != MigrationVersion.Version15 ||
                        !(fromVersionData is Data.Version15.MigrationData.Chromatography.AnalysisResultSetMigrationData analysisResultSetData))
                        throw new ArgumentException("From version data is incorrect!");
                    return Transform(analysisResultSetData);
                }, transformContext.BlockOption);
            arsTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"analysis result set transform complete with State{_.Status}");
            });
            return arsTransformBlock;
        }

        internal static AnalysisResultSetMigrationData Transform(Data.Version15.MigrationData.Chromatography.AnalysisResultSetMigrationData analysisResultSetMigrationData)
        {
            if (analysisResultSetMigrationData == null) throw new ArgumentNullException(nameof(analysisResultSetMigrationData));
            var analysisResultSetData16 = new AnalysisResultSetMigrationData
            {
                ProjectGuid = analysisResultSetMigrationData.ProjectGuid,
                AnalysisResultSet = AnalysisResultSet.Transform(analysisResultSetMigrationData.AnalysisResultSet)
            };
            if (analysisResultSetMigrationData.ReviewApproveData != null)
                analysisResultSetData16.ReviewApproveData = ReviewApproveDataTransform.Transform(analysisResultSetMigrationData.ReviewApproveData);
            if (analysisResultSetMigrationData.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in analysisResultSetMigrationData.AuditTrailLogs)
                    analysisResultSetData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            foreach (var batchResultSetData in analysisResultSetMigrationData.BatchResultSetData)
                analysisResultSetData16.BatchResultSetData.Add(BatchResultSetDataTransform.Transform(batchResultSetData));
            foreach (var batchRunAnalysisResult in analysisResultSetMigrationData.BatchRunAnalysisResults)
                analysisResultSetData16.BatchRunAnalysisResults.Add(Transform(batchRunAnalysisResult));
            foreach (var batchRunChannelMap in analysisResultSetMigrationData.BatchRunChannelMaps)
                analysisResultSetData16.BatchRunChannelMaps.Add(BatchRunChannelMap.Transform(batchRunChannelMap));
            foreach (var manualOverrideMap in analysisResultSetMigrationData.ManualOverrideMaps)
                analysisResultSetData16.ManualOverrideMaps.Add(ManualOverrideMap.Transform(manualOverrideMap));
            foreach (var brChannelsWithExceededNumberOfPeak in analysisResultSetMigrationData.BrChannelsWithExceededNumberOfPeaks)
                analysisResultSetData16.BrChannelsWithExceededNumberOfPeaks.Add(BrChannelsWithExceededNumberOfPeaks.Transform(brChannelsWithExceededNumberOfPeak));
            foreach (var compoundSuitabilitySummaryResult in analysisResultSetMigrationData.CompoundSuitabilitySummaryResults)
                analysisResultSetData16.CompoundSuitabilitySummaryResults.Add(CompoundSuitabilitySummaryResults.Transform(compoundSuitabilitySummaryResult));
            foreach (var compoundLibraryData in analysisResultSetMigrationData.CompoundLibraryData)
                analysisResultSetData16.CompoundLibraryData.Add(CompoundLibraryDataTransform.TransformSnapshotCompoundLibary(compoundLibraryData));

            return analysisResultSetData16;
        }

        public static BatchRunAnalysisResultData16 Transform(BatchRunAnalysisResultData15 batchRunAnalysisResultData15)
        {
            if (batchRunAnalysisResultData15 == null) return null;
            var batchRunAnalysisResultData16 = new BatchRunAnalysisResultData16
            {
                BatchRunAnalysisResult = BatchRunAnalysisResult.Transform(batchRunAnalysisResultData15.BatchRunAnalysisResult),
                SequenceSampleInfoModifiable = SequenceSampleInfoModifiable.Transform(batchRunAnalysisResultData15.SequenceSampleInfoModifiable),
                ModifiableProcessingMethod = ProcessingMethod.Transform(batchRunAnalysisResultData15.ModifiableProcessingMethod),
            };
            
            foreach (var calculatedChannelData in batchRunAnalysisResultData15.CalculatedChannelData)
            {
                batchRunAnalysisResultData16.CalculatedChannelData.Add(Transform(calculatedChannelData));
            }

            return batchRunAnalysisResultData16;
        }

        internal static CalculatedChannelCompositeMigrationData Transform(
            CalculatedChannelCompositeData15 calculatedChannelCompositeData)
        {
            if (calculatedChannelCompositeData == null) return null;

            var calculatedChannelCompositeData16 = new CalculatedChannelCompositeMigrationData
            {
                CalculatedChannelData = CalculatedChannelData.Transform(calculatedChannelCompositeData.CalculatedChannelData)
            };
            foreach (var runPeakResult in calculatedChannelCompositeData.RunPeakResults)
                calculatedChannelCompositeData16.RunPeakResults.Add(RunPeakResult.Transform(runPeakResult));

            foreach (var suitabilityResult in calculatedChannelCompositeData.SuitabilityResults)
                calculatedChannelCompositeData16.SuitabilityResults.Add(SuitabilityResult.Transform(suitabilityResult));

            return calculatedChannelCompositeData16;
        }
    }
}
