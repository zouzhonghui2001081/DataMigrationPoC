using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class CalculatedChannelCompositeMigrationData : MigrationDataBase
    {
        public CalculatedChannelCompositeMigrationData()
        {
            RunPeakResults = new List<RunPeakResult>();
            SuitabilityResults = new List<SuitabilityResult>();
        }

        public override MigrationVersion MigrationVersion => MigrationVersion.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.AnlysisResultSet;

        public CalculatedChannelData CalculatedChannelData { get; set; }

        public IList<RunPeakResult> RunPeakResults { get; set; }

        public IList<SuitabilityResult> SuitabilityResults { get; set; }
    }

    public class BatchRunAnalysisResultData
    {
        public BatchRunAnalysisResultData()
        {
            CalculatedChannelData = new List<CalculatedChannelCompositeMigrationData>();
        }

        public BatchRunAnalysisResult BatchRunAnalysisResult { get; set; }

        public SequenceSampleInfoModifiable SequenceSampleInfoModifiable { get; set; }

        public ProcessingMethod ModifiableProcessingMethod { get; set; }

        public IList<CalculatedChannelCompositeMigrationData> CalculatedChannelData { get; set; }
    }

    public class AnalysisResultSetMigrationData : MigrationDataBase
    {
        public AnalysisResultSetMigrationData()
        {
            BatchResultSetData = new List<BatchResultSetMigrationData>();
            BatchRunAnalysisResults = new List<BatchRunAnalysisResultData>();
            BatchRunChannelMaps = new List<BatchRunChannelMap>();
            ManualOverrideMaps = new List<ManualOverrideMap>();
            BrChannelsWithExceededNumberOfPeaks = new List<BrChannelsWithExceededNumberOfPeaks>();
            CompoundSuitabilitySummaryResults = new List<CompoundSuitabilitySummaryResults>();
            CompoundLibraryData = new List<SnapshotCompoundLibraryData>();
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override MigrationVersion MigrationVersion => MigrationVersion.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.AnlysisResultSet;

        public Guid ProjectGuid { get; set; }

        public AnalysisResultSet AnalysisResultSet { get; set; }

        public IList<BatchResultSetMigrationData> BatchResultSetData { get; set; }

        public IList<BatchRunAnalysisResultData> BatchRunAnalysisResults { get; set; }

        public IList<BatchRunChannelMap> BatchRunChannelMaps { get; set; }

        public IList<ManualOverrideMap> ManualOverrideMaps { get; set; }

        public IList<BrChannelsWithExceededNumberOfPeaks> BrChannelsWithExceededNumberOfPeaks { get; set; }

        public IList<CompoundSuitabilitySummaryResults> CompoundSuitabilitySummaryResults { get; set; }

        public IList<SnapshotCompoundLibraryData> CompoundLibraryData { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
        
    }
}
