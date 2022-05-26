

using System;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    public static class MigrationExtension
    {
        public static void StartSourceDataflow(this ISourceBlock<object> sourceBlock, string sourceVersion, string sourceConfig)
        {
            var sourceComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(versionComponent => versionComponent.Version == sourceVersion);
            var sourceMethod = MigrationComponenetsFactory.GetSourceMethodInfo(sourceComponent);
            sourceMethod.Invoke(sourceBlock, new object[] { sourceConfig });
        }

        public static void PrepareTarget(this ITargetBlock<object> targetBlock, string targetVersion, string targetConfig)
        {
            var targetComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(versionComponent => versionComponent.Version == targetVersion);
            var targetMethod = MigrationComponenetsFactory.GetTargetMethodInfo(targetComponent);
            targetMethod.Invoke(targetBlock, new object[] { targetConfig });
        }
    }
}
