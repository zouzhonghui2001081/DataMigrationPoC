using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Sequence;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Sequence
{
    public class SequenceGroupSetting : ISequenceGroupSetting
    {
        public long Id { get; set; }
        public string ExportPath { get; set; }
        public List<ReportGroup> ReportGroups { get; set; } = new List<ReportGroup>();
        public string ProjectName { get; set ; }
        public bool IsGlobal { get; set; }
        public bool IsDefault { get; set; }
    }
}
