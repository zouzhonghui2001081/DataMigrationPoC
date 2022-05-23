
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    public class TransformComponent
    {
        public string Name { get; set; }

        public string FromVersion { get; set; }

        public string ToVersion { get; set; }

        public string DllName { get; set; }

        public string MigrationClassName { get; set; }

        public object PropagatorBlock { get; set; }
    }

    public class VersionComponent
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string DllName { get; set; }

        public string MigrationClassName { get; set; }

        public string SourceStartMethodName { get; set; }

        public string ApplyTargetConfigMethodName { get; set; }

        public object VersionBlock { get; set; }
    }

    public class MigrationComponents
    {
        public MigrationComponents()
        {
            VersionComponents = new List<VersionComponent>();
            TransformComponents = new List<TransformComponent>();
        }

        public IList<VersionComponent> VersionComponents { get; }

        public IList<TransformComponent> TransformComponents { get; }
    }
}
