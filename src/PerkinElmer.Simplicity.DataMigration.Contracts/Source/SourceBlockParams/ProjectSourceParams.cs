using System;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams
{
    public class ProjectSourceParams : SourceParamBase
    {
        public ProjectSourceParams(Guid projectGuid)
        {
            ProjectGuid = projectGuid;
        }

        public override SourceParamTypes SourceKeyType => SourceParamTypes.ProjectGuid;

        public Guid ProjectGuid { get; set; }
    }
}
