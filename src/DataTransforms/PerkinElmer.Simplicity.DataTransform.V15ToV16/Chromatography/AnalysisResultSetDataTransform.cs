using System;
using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.AuditTrail;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography.ProcessingMethod;
using AnalysisResultSetData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.AnalysisResultSetData;
using AnalysisResultSetData16 = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.AnalysisResultSetData;
using BatchRunAnalysisResultData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.BatchRunAnalysisResultData;
using BatchRunAnalysisResultData16 = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.BatchRunAnalysisResultData;
using CalculatedChannelCompositeData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.CalculatedChannelCompositeData;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class AnalysisResultSetDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static AnalysisResultSetData16 Transform(AnalysisResultSetData15 analysisResultSetData15)
        {
            if (analysisResultSetData15 == null) throw new ArgumentNullException(nameof(analysisResultSetData15));
            var analysisResultSetData16 = new AnalysisResultSetData
            {
                ProjectGuid = analysisResultSetData15.ProjectGuid,
                AnalysisResultSet = AnalysisResultSet.Transform(analysisResultSetData15.AnalysisResultSet)
            };
            if (analysisResultSetData16.ReviewApproveData != null)
                analysisResultSetData16.ReviewApproveData = ReviewApproveDataTransform.Transform(analysisResultSetData15.ReviewApproveData);
            if (analysisResultSetData16.AuditTrailLogs != null)
            {
                foreach (var auditTrailLog in analysisResultSetData15.AuditTrailLogs)
                    analysisResultSetData16.AuditTrailLogs.Add(AuditTrailLogEntry.Transform(auditTrailLog));
            }
            foreach (var batchResultSetData in analysisResultSetData15.BatchResultSetData)
                analysisResultSetData16.BatchResultSetData.Add(BatchResultSetDataTransform.Transform(batchResultSetData));
            foreach (var batchRunAnalysisResult in analysisResultSetData15.BatchRunAnalysisResults)
                analysisResultSetData16.BatchRunAnalysisResults.Add(Transform(batchRunAnalysisResult));
            foreach (var batchRunChannelMap in analysisResultSetData15.BatchRunChannelMaps)
                analysisResultSetData16.BatchRunChannelMaps.Add(BatchRunChannelMap.Transform(batchRunChannelMap));
            foreach (var manualOverrideMap in analysisResultSetData15.ManualOverrideMaps)
                analysisResultSetData16.ManualOverrideMaps.Add(ManualOverrideMap.Transform(manualOverrideMap));
            foreach (var brChannelsWithExceededNumberOfPeak in analysisResultSetData15.BrChannelsWithExceededNumberOfPeaks)
                analysisResultSetData16.BrChannelsWithExceededNumberOfPeaks.Add(BrChannelsWithExceededNumberOfPeaks.Transform(brChannelsWithExceededNumberOfPeak));
            foreach (var compoundSuitabilitySummaryResult in analysisResultSetData15.CompoundSuitabilitySummaryResults)
                analysisResultSetData16.CompoundSuitabilitySummaryResults.Add(CompoundSuitabilitySummaryResults.Transform(compoundSuitabilitySummaryResult));
            foreach (var compoundLibraryData in analysisResultSetData15.CompoundLibraryData)
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

        internal static CalculatedChannelCompositeVersion16Data Transform(
            CalculatedChannelCompositeData15 calculatedChannelCompositeData)
        {
            if (calculatedChannelCompositeData == null) return null;

            var calculatedChannelCompositeData16 = new CalculatedChannelCompositeVersion16Data
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
