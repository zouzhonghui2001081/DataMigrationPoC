using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography
{
    public class CalculatedChannelCompositeVersion16Data : Version16DataBase
    {
        public CalculatedChannelCompositeVersion16Data()
        {
            RunPeakResults = new List<RunPeakResult>();
            SuitabilityResults = new List<SuitabilityResult>();
        }


        public override Version16DataTypes Version16DataTypes => Version16DataTypes.AnalysisResultSet;

        public CalculatedChannelData CalculatedChannelData { get; set; }

        public IList<RunPeakResult> RunPeakResults { get; set; }

        public IList<SuitabilityResult> SuitabilityResults { get; set; }
    }

    public class BatchRunAnalysisResultData
    {
        public BatchRunAnalysisResultData()
        {
            CalculatedChannelData = new List<CalculatedChannelCompositeVersion16Data>();
        }

        public BatchRunAnalysisResult BatchRunAnalysisResult { get; set; }

        public SequenceSampleInfoModifiable SequenceSampleInfoModifiable { get; set; }

        public ProcessingMethod ModifiableProcessingMethod { get; set; }

        public IList<CalculatedChannelCompositeVersion16Data> CalculatedChannelData { get; set; }
    }

    public class AnalysisResultSetData : Version16DataBase
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

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.AnalysisResultSet;

        public Guid ProjectGuid { get; set; }

        public AnalysisResultSet AnalysisResultSet { get; set; }

        public IList<BatchResultSetData> BatchResultSetData { get; set; }

        public IList<BatchRunAnalysisResultData> BatchRunAnalysisResults { get; set; }

        public IList<BatchRunChannelMap> BatchRunChannelMaps { get; set; }

        public IList<ManualOverrideMap> ManualOverrideMaps { get; set; }

        public IList<BrChannelsWithExceededNumberOfPeaks> BrChannelsWithExceededNumberOfPeaks { get; set; }

        public IList<CompoundSuitabilitySummaryResults> CompoundSuitabilitySummaryResults { get; set; }

        public IList<SnapshotCompoundLibraryData> CompoundLibraryData { get; set; }

        public ReviewApproveVersion16Data ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
        
    }
}
