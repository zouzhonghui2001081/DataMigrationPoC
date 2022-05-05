using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class BatchRunData
    {
        public BatchRunData()
        {
            NamedContents = new List<NamedContent>();
            StreamDataBatchResults = new List<StreamDataBatchResult>();
        }

        public BatchRun BatchRun { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public SequenceSampleInfoBatchResult SequenceSampleInfoBatchResult { get; set; }

        public ProcessingMethod ProcessingMethod { get; set; }

        public IList<NamedContent> NamedContents { get; set; }

        public IList<StreamDataBatchResult> StreamDataBatchResults { get; set; }
    }

    public class BatchResultSetMigrationData : MigrationDataBase
    {
        public BatchResultSetMigrationData()
        {
            DeviceModuleDetails = new List<BatchResultDeviceModuleDetails>();
            DeviceDriverItemDetails = new List<DeviceDriverItemDetails>();
            BatchRuns = new List<BatchRunData>();
        }

        public override MigrationVersion MigrationVersion => MigrationVersion.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.BatchResultSet;

        public Guid ProjectGuid { get; set; }

        public BatchResultSet BatchResultSet { get; set; }

        public IList<BatchResultDeviceModuleDetails> DeviceModuleDetails { get; set; }

        public IList<DeviceDriverItemDetails> DeviceDriverItemDetails { get; set; }

        public IList<BatchRunData> BatchRuns { get; set; }

    }
}
