using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly VersionComponent _sourceComponent;

        private readonly Stack<TransformComponent> _transformComponenents;

        private readonly VersionComponent _targetComponents;

        private readonly BroadcastBlock<string> _msgBroadcaseBlock;

        private readonly ActionBlock<string> _migrationAuditBlock;

        private readonly ActionBlock<string> _progressUpdateBlock;

        private readonly MigrationComponenetsFactory _migrationComponenetsFactory;

        public MigrationManager(string startVersion, string endVersion)
        {
            _migrationComponenetsFactory = new MigrationComponenetsFactory();
            var executeBlockOption = new ExecutionDataflowBlockOptions { CancellationToken = Cancellation.Token };
            _sourceComponent = GenerateSourceBlock(startVersion);
            _transformComponenents = GenerateTransformBlocks(startVersion, endVersion);
            _targetComponents = GenerateTargetBlock(endVersion);
            _msgBroadcaseBlock = new BroadcastBlock<string>(message => message, executeBlockOption);
            _migrationAuditBlock = new ActionBlock<string>(AuditMigration, executeBlockOption);
            _progressUpdateBlock = new ActionBlock<string>(ProcessUpdate, executeBlockOption);
            if (!BuildPipeline())
                throw new ArgumentException($"Failed setup pipeline from {startVersion} to {endVersion}");
        }

        public CancellationTokenSource Cancellation => new CancellationTokenSource();

        private int TotalMigrationItemCount { get; }

        private int ProcessedMigrationItem { get; }

        public void Start(MigrationContext migrationContext)
        {
            SetTargetConfig(migrationContext.TargetConfig);
            StartDataflowInternal(migrationContext.SourceConfig);
        }

        private void SetTargetConfig(string targetConfig)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath, _targetComponents.DllName));

            var typeInstance = assembly.GetType(_targetComponents.VersionClassName);
            if (typeInstance == null) return;

            var methodInfo = typeInstance.GetMethod(_targetComponents.TargetVersionMethodName);
            if (methodInfo != null && _targetComponents.VersionBlock != null)
                methodInfo.Invoke(_targetComponents.VersionBlock, new object[] { targetConfig });
        }

        private void StartDataflowInternal(string sourceFlowConfig)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath, _sourceComponent.DllName));
            var typeInstance = assembly.GetType(_sourceComponent.VersionClassName);

            if (typeInstance == null) return;
            var methodInfo = typeInstance.GetMethod(_sourceComponent.SourceVersionMethodName);
            if (methodInfo != null && _sourceComponent.VersionBlock != null)
                methodInfo.Invoke(_sourceComponent.VersionBlock, new object[] { sourceFlowConfig } );
        }

        private bool BuildPipeline()
        {
            if (_sourceComponent == null || _targetComponents == null) return false;

            if (!(_sourceComponent.VersionBlock is ISourceBlock<object> sourceBlock))
                throw new ArgumentException("Version component is incorrect!");
            if(!(_targetComponents.VersionBlock is ITargetBlock<object> targetBlock))
                throw new ArgumentException("Version component is incorrect!");

            _msgBroadcaseBlock.LinkTo(_migrationAuditBlock);
            _msgBroadcaseBlock.LinkTo(_progressUpdateBlock);

            if (_sourceComponent.VersionBlock is ISourceBlock<string> sourceMsgBlock)
                sourceMsgBlock.LinkTo(_msgBroadcaseBlock);
            if (_targetComponents.VersionBlock is ISourceBlock<string> targetMsgBlock)
                targetMsgBlock.LinkTo(_msgBroadcaseBlock);

            if (_transformComponenents != null && _transformComponenents.Count > 0)
            {
                var currentTransformInfo = _transformComponenents.Pop();
                if(!(currentTransformInfo.PropagatorBlock is IPropagatorBlock<object, object> currentPropagator))
                    throw new ArgumentException("Version component is incorrect!");
                currentPropagator.LinkTo(targetBlock);
                if (currentTransformInfo.PropagatorBlock is ISourceBlock<string> transformMsgSource)
                    transformMsgSource.LinkTo(_msgBroadcaseBlock);

                while (_transformComponenents.Count > 0)
                {
                    var previousTransoformComponent = _transformComponenents.Pop();
                    if (!(previousTransoformComponent.PropagatorBlock is IPropagatorBlock<object, object> previousPropagator))
                        throw new ArgumentException("Version component is incorrect!");
                    if (previousTransoformComponent.PropagatorBlock is ISourceBlock<string> previousMsgSource)
                        previousMsgSource.LinkTo(_msgBroadcaseBlock);
                    previousPropagator.LinkTo(currentPropagator);
                    currentPropagator = previousPropagator;
                }

                sourceBlock.LinkTo(currentPropagator);
            }
            else
                sourceBlock.LinkTo(targetBlock);
            return true;
        }

        private void ProcessUpdate(string message)
        {
            
        }

        private void AuditMigration(string message)
        {
            
        }

        private VersionComponent GenerateSourceBlock(string sourceVersionName)
        {
            var versionComponentInfo = _migrationComponenetsFactory.Versions.FirstOrDefault(version => version.Version == sourceVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            if (versionComponentInfo.VersionBlock != null) return versionComponentInfo;

            var versionInstance = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, versionComponentInfo.DllName, versionComponentInfo.VersionClassName);
            versionComponentInfo.VersionBlock = versionInstance;
            return versionComponentInfo;
        }

        private VersionComponent GenerateTargetBlock(string targetVersionName)
        {
            var versionComponentInfo = _migrationComponenetsFactory.Versions.FirstOrDefault(version => version.Version == targetVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            if (versionComponentInfo.VersionBlock != null) return versionComponentInfo;

            var versionInstance = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, versionComponentInfo.DllName, versionComponentInfo.VersionClassName);
            versionComponentInfo.VersionBlock = versionInstance;
            return versionComponentInfo;
        }

        private Stack<TransformComponent> GenerateTransformBlocks(string startVersionName, string endVersionName)
        {
            var transformComponents = new Stack<TransformComponent>();
            if (!_migrationComponenetsFactory.Transforms.ContainsKey(startVersionName)) return transformComponents;

            var transformMap = _migrationComponenetsFactory.Transforms[startVersionName];
            if (!transformMap.ContainsKey(endVersionName))
                return GetTransformBlocksRecursively(endVersionName, transformMap);

            var transformComponent = transformMap[endVersionName];
            var transformInstance = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, transformComponent.DllName, transformComponent.MigrationClassName);
            transformComponent.PropagatorBlock = transformInstance;
            transformComponents.Push(transformComponent);
            return transformComponents;
        }

        private Stack<TransformComponent> GetTransformBlocksRecursively(string endVersionName,
            IDictionary<string, TransformComponent> transformMap)
        {
            var transformBlocks = new Stack<TransformComponent>();
            foreach (var transform in transformMap)
            {
                if (_migrationComponenetsFactory.Transforms.ContainsKey(transform.Key))
                {
                    var nextTransformMap = _migrationComponenetsFactory.Transforms[transform.Key];
                    if (nextTransformMap != null && nextTransformMap.Count > 0)
                    {
                        if (nextTransformMap.ContainsKey(endVersionName))
                        {
                            var nextTransformComponet = nextTransformMap[endVersionName];
                            var nextPropagator = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, nextTransformComponet.DllName, nextTransformComponet.MigrationClassName);
                            nextTransformComponet.PropagatorBlock = nextPropagator;
                            transformBlocks.Push(nextTransformComponet);

                            var currentTransformComponent = transformMap[transform.Key];
                            var currentPropagator = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, currentTransformComponent.DllName, currentTransformComponent.MigrationClassName);
                            currentTransformComponent.PropagatorBlock = currentPropagator;
                            transformBlocks.Push(currentTransformComponent);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endVersionName, nextTransformMap);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            var component = transformMap[transform.Key];
                            var propagator = _migrationComponenetsFactory.CreateBlockInstance(Cancellation.Token, component.DllName, component.MigrationClassName);
                            component.PropagatorBlock = propagator;
                            transformBlocks.Push(component);
                            return transformBlocks;
                        }
                    }
                }
            }
            return transformBlocks;
        }
    }
}
