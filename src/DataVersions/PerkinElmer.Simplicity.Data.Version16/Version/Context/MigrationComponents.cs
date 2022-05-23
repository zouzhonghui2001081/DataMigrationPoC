
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Context
{
    internal class TransformComponent
    {
        public string Name { get; set; }

        public string FromVersion { get; set; }

        public string ToVersion { get; set; }

        public string DllName { get; set; }

        public string MigrationClassName { get; set; }
    }

    internal class VersionComponent
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public string DllName { get; set; }

        public string MigrationClassName { get; set; }
    }

    internal class MigrationComponents
    {
        public IList<VersionComponent> VersionComponents { get; }

        public IList<TransformComponent> TransformComponents { get; }
    }
}
