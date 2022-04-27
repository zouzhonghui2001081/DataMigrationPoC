using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Common;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration.MigrationContext;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.PipelineBuilder
{
    public abstract class PipelineBuilderBase
    {
        protected abstract IList<SourceBase> Sources { get; }

        protected abstract IList<TargetBase> Targets { get; }

        protected abstract IDictionary<ReleaseVersions, IDictionary<ReleaseVersions, TransformBase>> TransformMaps { get; }

        public (IPropagatorBlock<Tuple<Guid, IList<Guid>>, MigrationDataBase>, Task) CreateProjectEntityIdsPipeline(
            MigrationContextBase migrationContextBase)
        {
            var sourceVersion = migrationContextBase.SourceContext.FromReleaseVersion;
            var sourceType = migrationContextBase.SourceContext.SourceType;
            var targetVersion = migrationContextBase.TargetContext.TargetReleaseVersion;
            var targetType = migrationContextBase.TargetContext.TargetType;

            var sourceBlockCreator = GenerateSourceBlock(sourceVersion, sourceType);
            var targetBlockCreator = GenerateTargetBlock(targetVersion, targetType);
            var transformStack = GenerateTransformBlock(sourceVersion, targetVersion);

            if (sourceBlockCreator == null || targetBlockCreator == null) return (null, null);
            if (sourceVersion != targetVersion && transformStack.Count == 0) return (null, null);

            var sourceBlock = sourceBlockCreator.CreateSourceByIds(migrationContextBase.SourceContext);
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

        public (IPropagatorBlock<Guid, MigrationDataBase>, Task) CreateProjectPipeline(MigrationContextBase migrationContextBase)
        {
            var sourceVersion = migrationContextBase.SourceContext.FromReleaseVersion;
            var sourceType = migrationContextBase.SourceContext.SourceType;
            var targetVersion = migrationContextBase.TargetContext.TargetReleaseVersion;
            var targetType = migrationContextBase.TargetContext.TargetType;

            var sourceBlockCreator = GenerateSourceBlock(sourceVersion, sourceType);
            var targetBlockCreator = GenerateTargetBlock(targetVersion, targetType);
            var transformStack = GenerateTransformBlock(sourceVersion, targetVersion);

            if (sourceBlockCreator == null || targetBlockCreator == null) return (null, null);
            if (sourceVersion != targetVersion && transformStack.Count == 0) return (null, null);

            var sourceBlock = sourceBlockCreator.CreateSourceByProjectId(migrationContextBase.SourceContext);
            var targetBlock = targetBlockCreator.CreateTarget(migrationContextBase.TargetContext);

            if (transformStack != null && transformStack.Count > 0)
            {
                var previousTransformCreator = transformStack.Pop();
                var previousTransformBlock = previousTransformCreator.CreateTransform(migrationContextBase.TransformContext);
                previousTransformBlock.LinkTo(targetBlock, new DataflowLinkOptions { PropagateCompletion = true });
                while (transformStack.Count > 0)
                {
                    var currentTransoformCreator = transformStack.Pop();
                    var currentTransformBlock = currentTransoformCreator.CreateTransform(migrationContextBase.TransformContext);
                    currentTransformBlock.LinkTo(previousTransformBlock, new DataflowLinkOptions { PropagateCompletion = true });
                    previousTransformBlock = currentTransformBlock;
                }

                sourceBlock.LinkTo(previousTransformBlock, new DataflowLinkOptions { PropagateCompletion = true });
            }
            else
                sourceBlock.LinkTo(targetBlock, new DataflowLinkOptions { PropagateCompletion = true });

            return (sourceBlock, targetBlock.Completion);
        }

        protected SourceBase GenerateSourceBlock(ReleaseVersions startReleaseVersion,
            SourceTypes sourceType)
        {
            return Sources.FirstOrDefault(source => source.SourceReleaseVersion == startReleaseVersion && 
                                                    source.SourceType == sourceType);
        }

        protected TargetBase GenerateTargetBlock(ReleaseVersions targetReleaseVersion,
            TargetTypes targetType)
        {
            return Targets.FirstOrDefault(target => target.TargetReleaseVersion == targetReleaseVersion &&
                                                    target.TargetType == targetType);
        }

        protected Stack<TransformBase> GenerateTransformBlock(ReleaseVersions startReleaseVersion, ReleaseVersions endReleaseVersion)
        {
            var transformBlocks = new Stack<TransformBase>();
            if (!TransformMaps.ContainsKey(startReleaseVersion)) return transformBlocks;

            var endBlockMaps = TransformMaps[startReleaseVersion];
            if (!endBlockMaps.ContainsKey(endReleaseVersion))
                return GetTransformBlocksRecursively(endReleaseVersion, endBlockMaps);

            transformBlocks.Push(endBlockMaps[endReleaseVersion]);
            return transformBlocks;
        }

        private Stack<TransformBase> GetTransformBlocksRecursively(ReleaseVersions endReleaseVersion,
            IDictionary<ReleaseVersions, TransformBase> targets)
        {
            var transformBlocks = new Stack<TransformBase>();
            foreach (var target in targets)
            {
                if (TransformMaps.ContainsKey(target.Key))
                {
                    var nextTargets = TransformMaps[target.Key];
                    if (nextTargets != null && nextTargets.Count > 0)
                    {
                        if (nextTargets.ContainsKey(endReleaseVersion))
                        {
                            transformBlocks.Push(nextTargets[endReleaseVersion]);
                            transformBlocks.Push(targets[target.Key]);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endReleaseVersion, nextTargets);
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
