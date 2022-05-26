using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    internal class MigrationPipeline
    {
        private readonly MigrationMessageHandler _migrationMessageHandler;

        public MigrationPipeline(string startVersion, string endVersion)
        {
            _migrationMessageHandler = new MigrationMessageHandler();

            GenerateSourceBlock(startVersion);
            GenerateTransformBlocks(startVersion, endVersion);
            GenerateTargetBlock(endVersion);
            BuildTransformPipeline();
            BuildMessagePipeline();
        }

        public ISourceBlock<object> SourceBlock { get; private set; }

        public Stack<IPropagatorBlock<object, object>> TransformBlocks { get; private set; }

        public ITargetBlock<object> TargetBlock { get; private set; }

        public CancellationTokenSource Cancellation => new CancellationTokenSource();

        private void GenerateSourceBlock(string startVersion)
        {
            var startVersionComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(migrationComponents =>
                migrationComponents.Version == startVersion);
            if (startVersionComponent == null) throw new ArgumentException("Start version is incorrect!");
            if (startVersionComponent.VersionBlock == null)
                MigrationComponenetsFactory.CreateVersionBlockInstance(Cancellation.Token, startVersionComponent);

            SourceBlock = startVersionComponent.VersionBlock as ISourceBlock<object>;
            if (SourceBlock == null)
                throw new ArgumentException("Source block should not be null!");
        }

        private void GenerateTransformBlocks(string startVersion, string endVersion)
        {
            var transformBlocks = new Stack<IPropagatorBlock<object, object>>();
            if (!MigrationComponenetsFactory.Transforms.ContainsKey(startVersion))
            {
                TransformBlocks = transformBlocks;
                return;
            }

            var transformMap = MigrationComponenetsFactory.Transforms[startVersion];
            if (!transformMap.ContainsKey(endVersion))
            {
                TransformBlocks = GenerateTransformBlocksRecursively(endVersion, transformMap);
                return;
            }

            var transformComponent = transformMap[endVersion];
            if(transformComponent.PropagatorBlock == null)
                MigrationComponenetsFactory.CreateTransformBlockInstance(Cancellation.Token, transformComponent);
            if(transformComponent.PropagatorBlock is IPropagatorBlock<object, object> propagatorBlock)
                transformBlocks.Push(propagatorBlock);
            TransformBlocks = transformBlocks;
        }

        private void GenerateTargetBlock(string endVersion)
        {
            var endVersionComponent = MigrationComponenetsFactory.Versions.FirstOrDefault(migrationComponents =>
                migrationComponents.Version == endVersion);
            if (endVersionComponent == null) throw new ArgumentException("End version is incorrect!");

            if (endVersionComponent.VersionBlock == null)
                MigrationComponenetsFactory.CreateVersionBlockInstance(Cancellation.Token, endVersionComponent);

            TargetBlock = endVersionComponent.VersionBlock as ITargetBlock<object>;
            if (TargetBlock == null)
                throw new ArgumentException("Target block should not be null!");
        }

        private void BuildTransformPipeline()
        {
            if (TransformBlocks != null && TransformBlocks.Count > 0)
            {
                var currentPropagatorBlock = TransformBlocks.Pop();
                currentPropagatorBlock.LinkTo(TargetBlock);

                while (TransformBlocks.Count > 0)
                {
                    var previousPropagatorBlock = TransformBlocks.Pop();
                    previousPropagatorBlock.LinkTo(currentPropagatorBlock);
                    currentPropagatorBlock = previousPropagatorBlock;
                }

                SourceBlock.LinkTo(currentPropagatorBlock);
            }
            else
                SourceBlock.LinkTo(TargetBlock);
        }

        private void BuildMessagePipeline()
        {
            if (SourceBlock is ISourceBlock<string> sourceBlockAsMessageSource)
                sourceBlockAsMessageSource.LinkTo(_migrationMessageHandler);
            if (TargetBlock is ISourceBlock<string> targetBlockAsMessageSource)
                targetBlockAsMessageSource.LinkTo(_migrationMessageHandler);
            if (TransformBlocks.Count > 0)
            {
                foreach (var transformBlock in TransformBlocks)
                {
                    if (transformBlock is ISourceBlock<string> transformBlockAsMessageSource)
                        transformBlockAsMessageSource.LinkTo(_migrationMessageHandler);
                }
            }
        }

        private Stack<IPropagatorBlock<object, object>> GenerateTransformBlocksRecursively(string endVersionName, IDictionary<string, TransformComponent> transformMap)
        {
            var transformBlocks = new Stack<IPropagatorBlock<object, object>>();
            foreach (var transform in transformMap)
            {
                if (MigrationComponenetsFactory.Transforms.ContainsKey(transform.Key))
                {
                    var nextTransformMap = MigrationComponenetsFactory.Transforms[transform.Key];
                    if (nextTransformMap != null && nextTransformMap.Count > 0)
                    {
                        if (nextTransformMap.ContainsKey(endVersionName))
                        {
                            var nextTransformComponet = nextTransformMap[endVersionName];
                            if(nextTransformComponet.PropagatorBlock == null)
                                MigrationComponenetsFactory.CreateTransformBlockInstance(Cancellation.Token, nextTransformComponet);
                            if(nextTransformComponet.PropagatorBlock is IPropagatorBlock<object, object> nextPropagatorBlock)
                                transformBlocks.Push(nextPropagatorBlock);

                            var currentTransformComponent = transformMap[transform.Key];
                            if(currentTransformComponent.PropagatorBlock == null)
                                MigrationComponenetsFactory.CreateTransformBlockInstance(Cancellation.Token, currentTransformComponent);
                            if (currentTransformComponent.PropagatorBlock is IPropagatorBlock<object, object> currentPropagatorBlock)
                                transformBlocks.Push(currentPropagatorBlock);
                            return transformBlocks;
                        }

                        var subResult = GenerateTransformBlocksRecursively(endVersionName, nextTransformMap);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            var component = transformMap[transform.Key];
                            if (component.PropagatorBlock == null)
                                MigrationComponenetsFactory.CreateTransformBlockInstance(Cancellation.Token, component);
                            if (component.PropagatorBlock is IPropagatorBlock<object, object> propagatorBlock)
                                transformBlocks.Push(propagatorBlock);
                            return transformBlocks;
                        }
                    }
                }
            }
            return transformBlocks;
        }
    }
}
