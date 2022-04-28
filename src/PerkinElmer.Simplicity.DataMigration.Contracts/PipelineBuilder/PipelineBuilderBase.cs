using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder
{
    public abstract class PipelineBuilderBase
    {
        protected abstract IList<SourceBlockCreatorBase> Sources { get; }

        protected abstract IList<TargetBlockCreatorBase> Targets { get; }

        protected abstract IDictionary<MigrationVersions, IDictionary<MigrationVersions, TransformBlockCreatorBase>> TransformMaps { get; }

        public (IPropagatorBlock<SourceParamBase, MigrationDataBase>, Task) CreateEntitesTransformationPipeline(
            MigrationContextBase migrationContextBase)
        {
            var sourceVersion = migrationContextBase.SourceContext.FromMigrationVersion;
            var sourceType = migrationContextBase.SourceContext.SourceType;
            var targetVersion = migrationContextBase.TargetContext.TargetMigrationVersion;
            var targetType = migrationContextBase.TargetContext.TargetType;

            var sourceBlockCreator = GenerateSourceBlock(sourceVersion, sourceType);
            var targetBlockCreator = GenerateTargetBlock(targetVersion, targetType);
            var transformStack = GenerateTransformBlock(sourceVersion, targetVersion);

            if (sourceBlockCreator == null || targetBlockCreator == null) return (null, null);
            if (sourceVersion != targetVersion && transformStack.Count == 0) return (null, null);

            var sourceBlock = sourceBlockCreator.CreateSourceBlock(migrationContextBase.SourceContext);
            var targetBlock = targetBlockCreator.CreateTarget(migrationContextBase.TargetContext);

            if (transformStack != null && transformStack.Count > 0)
            {
                var previousTransformCreator = transformStack.Pop();
                var previousTransformBlock = previousTransformCreator.CreateTransform(migrationContextBase.TransformContext);
                previousTransformBlock.LinkTo(targetBlock);
                while (transformStack.Count > 0)
                {
                    var currentTransoformCreator = transformStack.Pop();
                    var currentTransformBlock = currentTransoformCreator.CreateTransform(migrationContextBase.TransformContext);
                    currentTransformBlock.LinkTo(previousTransformBlock);
                    previousTransformBlock = currentTransformBlock;
                }

                sourceBlock.LinkTo(previousTransformBlock);
            }
            else
                sourceBlock.LinkTo(targetBlock);

            return (sourceBlock, targetBlock.Completion);
        }

        protected SourceBlockCreatorBase GenerateSourceBlock(MigrationVersions startMigrationVersion,
            SourceTypes sourceType)
        {
            return Sources.FirstOrDefault(source => source.SourceVersion == startMigrationVersion && 
                                                    source.SourceType == sourceType);
        }

        protected TargetBlockCreatorBase GenerateTargetBlock(MigrationVersions targetMigrationVersion,
            TargetTypes targetType)
        {
            return Targets.FirstOrDefault(target => target.TargetVersion == targetMigrationVersion &&
                                                    target.TargetType == targetType);
        }

        protected Stack<TransformBlockCreatorBase> GenerateTransformBlock(MigrationVersions startMigrationVersion, MigrationVersions endMigrationVersion)
        {
            var transformBlocks = new Stack<TransformBlockCreatorBase>();
            if (!TransformMaps.ContainsKey(startMigrationVersion)) return transformBlocks;

            var endBlockMaps = TransformMaps[startMigrationVersion];
            if (!endBlockMaps.ContainsKey(endMigrationVersion))
                return GetTransformBlocksRecursively(endMigrationVersion, endBlockMaps);

            transformBlocks.Push(endBlockMaps[endMigrationVersion]);
            return transformBlocks;
        }

        private Stack<TransformBlockCreatorBase> GetTransformBlocksRecursively(MigrationVersions endMigrationVersion,
            IDictionary<MigrationVersions, TransformBlockCreatorBase> targets)
        {
            var transformBlocks = new Stack<TransformBlockCreatorBase>();
            foreach (var target in targets)
            {
                if (TransformMaps.ContainsKey(target.Key))
                {
                    var nextTargets = TransformMaps[target.Key];
                    if (nextTargets != null && nextTargets.Count > 0)
                    {
                        if (nextTargets.ContainsKey(endMigrationVersion))
                        {
                            transformBlocks.Push(nextTargets[endMigrationVersion]);
                            transformBlocks.Push(targets[target.Key]);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endMigrationVersion, nextTargets);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            transformBlocks.Push(targets[target.Key]);

                            return transformBlocks;
                        }
                    }
                }
            }

            return transformBlocks;
        }
    }
}
