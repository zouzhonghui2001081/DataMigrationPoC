using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class BatchRunData : Version16DataBase
    {
        public BatchRunData()
        {
            NamedContents = new List<NamedContent>();
            StreamDataBatchResults = new List<StreamDataBatchResult>();
        }

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.BatchResultSet;

        public BatchRun BatchRun { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public SequenceSampleInfoBatchResult SequenceSampleInfoBatchResult { get; set; }

        public ProcessingMethod ProcessingMethod { get; set; }

        public IList<NamedContent> NamedContents { get; set; }

        public IList<StreamDataBatchResult> StreamDataBatchResults { get; set; }
    }

    public class BatchResultSetData : Version16DataBase
    {
        public BatchResultSetData()
        {
            DeviceModuleDetails = new List<BatchResultDeviceModuleDetails>();
            DeviceDriverItemDetails = new List<DeviceDriverItemDetails>();
            BatchRuns = new List<BatchRunData>();
        }

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.BatchResultSet;

        public Guid ProjectGuid { get; set; }

        public BatchResultSet BatchResultSet { get; set; }

        public IList<BatchResultDeviceModuleDetails> DeviceModuleDetails { get; set; }

        public IList<DeviceDriverItemDetails> DeviceDriverItemDetails { get; set; }

        public IList<BatchRunData> BatchRuns { get; set; }
    }
}
