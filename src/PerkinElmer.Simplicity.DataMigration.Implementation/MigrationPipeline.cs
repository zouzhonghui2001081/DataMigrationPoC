using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    internal class MigrationPipeline
    {
        private readonly string _startVersion;

        private readonly string _endVersion;

        private readonly MigrationMessageHandler _migrationMessageHandler;
        
        private readonly MigrationComponentsFactory _migrationComponentsFactory;

        public MigrationPipeline(string startVersion, string endVersion, MigrationComponentsFactory migrationComponentsFactory)
        {
            _startVersion = startVersion;
            _endVersion = endVersion;

            _migrationMessageHandler = new MigrationMessageHandler();
            _migrationComponentsFactory = migrationComponentsFactory;
            InitialBlockInstances(startVersion,endVersion);
            BuildTransformPipeline();
            BuildMessagePipeline();
        }

        public ISourceBlock<object> SourceBlock { get; private set; }

        public Stack<IPropagatorBlock<object, object>> TransformBlocks { get; private set; }

        public ITargetBlock<object> TargetBlock { get; private set; }

        public CancellationTokenSource Cancellation => new CancellationTokenSource();

        public void PreparePipeline(string pipelineConfig)
        {
            var targetMethod = _migrationComponentsFactory.GetTargetMethodInfo(_endVersion);
            targetMethod.Invoke(TargetBlock, new object[] { pipelineConfig });
        }

        public void StartPipeline(string pipelineStartConfig)
        {
            var sourceMethod = _migrationComponentsFactory.GetSourceMethodInfo(_startVersion);
            sourceMethod.Invoke(SourceBlock, new object[] { pipelineStartConfig });
        }

        private void InitialBlockInstances(string startVersion, string endVersion)
        {
            SourceBlock = _migrationComponentsFactory.CreateSourceBlockInstance(Cancellation.Token, startVersion);
            TargetBlock = _migrationComponentsFactory.CreateTargetBlockInstance(Cancellation.Token, endVersion);
            TransformBlocks = _migrationComponentsFactory.CreateTransformBlockInstances(Cancellation.Token, startVersion, endVersion);
        }

        private void BuildTransformPipeline()
        {
            if (TransformBlocks != null && TransformBlocks.Count > 0)
            {
                var currentPropagatorBlock = TransformBlocks.Pop();
                currentPropagatorBlock.LinkTo(TargetBlock, new DataflowLinkOptions{PropagateCompletion = true});

                while (TransformBlocks.Count > 0)
                {
                    var previousPropagatorBlock = TransformBlocks.Pop();
                    previousPropagatorBlock.LinkTo(currentPropagatorBlock, new DataflowLinkOptions { PropagateCompletion = true });
                    currentPropagatorBlock = previousPropagatorBlock;
                }

                SourceBlock.LinkTo(currentPropagatorBlock, new DataflowLinkOptions { PropagateCompletion = true });
            }
            else
                SourceBlock.LinkTo(TargetBlock, new DataflowLinkOptions { PropagateCompletion = true });
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
    }
}
