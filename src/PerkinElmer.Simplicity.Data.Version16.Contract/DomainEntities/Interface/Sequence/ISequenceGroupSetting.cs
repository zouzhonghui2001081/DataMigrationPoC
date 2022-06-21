using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Sequence
{
    public interface ISequenceGroupSetting
    {
        long Id { get; set; }
        string ExportPath { get; set; }
        List<ReportGroup> ReportGroups { get; set; }
        string ProjectName { get; set; }
        bool IsGlobal { get; set; }
        bool IsDefault { get; set; }
    }
}
