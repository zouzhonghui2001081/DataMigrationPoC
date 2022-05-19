using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography
{
    public class CalculatedChannelCompositeData
    {
        public CalculatedChannelCompositeData()
        {
            RunPeakResults = new List<RunPeakResult>();
            SuitabilityResults = new List<SuitabilityResult>();
        }

        public CalculatedChannelData CalculatedChannelData { get; set; }

        public IList<RunPeakResult> RunPeakResults { get; set; }

        public IList<SuitabilityResult> SuitabilityResults { get; set; }
    }

    public class BatchRunAnalysisResultData
    {
        public BatchRunAnalysisResultData()
        {
            CalculatedChannelData = new List<CalculatedChannelCompositeData>();
        }

        public BatchRunAnalysisResult BatchRunAnalysisResult { get; set; }

        public SequenceSampleInfoModifiable SequenceSampleInfoModifiable { get; set; }

        public ProcessingMethod ModifiableProcessingMethod { get; set; }

        public IList<CalculatedChannelCompositeData> CalculatedChannelData { get; set; }
    }

    public class AnalysisResultSetData : Version15DataBase
    {
        public AnalysisResultSetData()
        {
            BatchResultSetData = new List<BatchResultSetData>();
            BatchRunAnalysisResults = new List<BatchRunAnalysisResultData>();
            BatchRunChannelMaps = new List<BatchRunChannelMap>();
            ManualOverrideMaps = new List<ManualOverrideMap>();
            BrChannelsWithExceededNumberOfPeaks = new List<BrChannelsWithExceededNumberOfPeaks>();
            CompoundSuitabilitySummaryResults = new List<CompoundSuitabilitySummaryResults>();
            CompoundLibraryData = new List<SnapshotCompoundLibraryData>();
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }
       
        public override Version15DataTypes Version15DataTypes => Version15DataTypes.AnalysisResultSet;

        public Guid ProjectGuid { get; set; }

        public AnalysisResultSet AnalysisResultSet { get; set; }

        public IList<BatchResultSetData> BatchResultSetData { get; set; }

        public IList<BatchRunAnalysisResultData> BatchRunAnalysisResults { get; set; }

        public IList<BatchRunChannelMap> BatchRunChannelMaps { get; set; }

        public IList<ManualOverrideMap> ManualOverrideMaps { get; set; }

        public IList<BrChannelsWithExceededNumberOfPeaks> BrChannelsWithExceededNumberOfPeaks { get; set; }

        public IList<CompoundSuitabilitySummaryResults> CompoundSuitabilitySummaryResults { get; set; }

        public IList<SnapshotCompoundLibraryData> CompoundLibraryData { get; set; }

        public ReviewApproveData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
