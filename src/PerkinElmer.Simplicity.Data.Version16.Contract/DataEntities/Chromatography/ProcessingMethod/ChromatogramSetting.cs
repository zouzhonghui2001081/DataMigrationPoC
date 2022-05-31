using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProcessingMethod
{
    public class ChromatogramSetting
    {
        public long Id { get; set; }
        public string ConfigurePeakLabels { get; set; }
        public bool IsOrientationVertical { get; set; }
        public bool IsSignalUnitInUv { get; set; }
        public bool IsTimeUnitInMinute { get; set; }
        public bool IsRescalePlotSignalToFull { get; set; }
        public bool IsRescalePlotSignalToMaxY { get; set; }
        public bool IsRescalePlotSignalToCustom { get; set; }
        public bool IsRescalePlottimeFull { get; set; }
        public long RescalePlotSignalFrom { get; set; }
        public long RescalePlotSignalTo { get; set; }
        public double RescalePlotTimeFrom { get; set; }
        public double RescalePlotTimeTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUserId { get; set; }

    }
}
