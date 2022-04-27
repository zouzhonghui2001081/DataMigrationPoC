namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography
{
    public class SequenceGroupSettingEntity
    {
        public long Id { get; set; }
        public string ExportPath { get; set; }
        public byte[] ReportGroups { get; set; }
        public long? ProjectId { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
