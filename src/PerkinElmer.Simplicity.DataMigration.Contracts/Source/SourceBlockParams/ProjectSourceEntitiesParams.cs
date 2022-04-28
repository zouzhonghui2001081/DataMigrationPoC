using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceBlockParams
{
    public class ProjectSourceEntitiesParams : SourceParamBase
    {
        public override SourceParamTypes SourceKeyType => SourceParamTypes.ProjectAndEntitiesGuid;

        public Guid ProjectGuid { get; set; }

        public List<Guid> EntityGuids { get; set; }
    }
}
