using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly VersionComponent _sourceComponent;

        private readonly Stack<TransformComponent> _transformComponenents;

        private readonly VersionComponent _targetComponents;

        private readonly MigrationComponenetsFactory _migrationComponenetsFactory;

        public MigrationManager(string startVersion, string endVersion)
        {
            _migrationComponenetsFactory = new MigrationComponenetsFactory();
            _sourceComponent = GenerateSourceBlock(startVersion);
            _transformComponenents = GenerateTransformBlocks(startVersion, endVersion);
            _targetComponents = GenerateTargetBlock(endVersion);
            if (!BuildTransformPipeline())
                throw new ArgumentException($"Failed setup pipeline from {startVersion} to {endVersion}");
        }

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

        private bool BuildTransformPipeline()
        {
            if (_sourceComponent == null || _targetComponents == null) return false;

            if (!(_sourceComponent.VersionBlock is ISourceBlock<object> sourceBlock))
                throw new ArgumentException("Version component is incorrect!");
            if(!(_targetComponents.VersionBlock is ITargetBlock<object> targetBlock))
                throw new ArgumentException("Version component is incorrect!");

            if (_transformComponenents != null && _transformComponenents.Count > 0)
            {
                var currentTransformInfo = _transformComponenents.Pop();
                if(!(currentTransformInfo.PropagatorBlock is IPropagatorBlock<object, object> currentPropagator))
                    throw new ArgumentException("Version component is incorrect!");
                currentPropagator.LinkTo(targetBlock);

                while (_transformComponenents.Count > 0)
                {
                    var previousTransoformComponent = _transformComponenents.Pop();
                    if (!(previousTransoformComponent.PropagatorBlock is IPropagatorBlock<object, object> previousPropagator))
                        throw new ArgumentException("Version component is incorrect!");
                    previousPropagator.LinkTo(currentPropagator);
                    currentPropagator = previousPropagator;
                }

                sourceBlock.LinkTo(currentPropagator);
            }
            else
                sourceBlock.LinkTo(targetBlock);
            return true;
        }
       
        private VersionComponent GenerateSourceBlock(string sourceVersionName)
        {
            var versionComponentInfo = _migrationComponenetsFactory.Versions.FirstOrDefault(version => version.Version == sourceVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            if (versionComponentInfo.VersionBlock != null) return versionComponentInfo;

            var versionInstance = _migrationComponenetsFactory.CreateInstance(versionComponentInfo.DllName, versionComponentInfo.VersionClassName);
            versionComponentInfo.VersionBlock = versionInstance;
            return versionComponentInfo;
        }

        private VersionComponent GenerateTargetBlock(string targetVersionName)
        {
            var versionComponentInfo = _migrationComponenetsFactory.Versions.FirstOrDefault(version => version.Version == targetVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            if (versionComponentInfo.VersionBlock != null) return versionComponentInfo;

            var versionInstance = _migrationComponenetsFactory.CreateInstance(versionComponentInfo.DllName, versionComponentInfo.VersionClassName);
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
            var transformInstance = _migrationComponenetsFactory.CreateInstance(transformComponent.DllName, transformComponent.MigrationClassName);
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
                            var nextPropagator = _migrationComponenetsFactory.CreateInstance(nextTransformComponet.DllName, nextTransformComponet.MigrationClassName);
                            nextTransformComponet.PropagatorBlock = nextPropagator;
                            transformBlocks.Push(nextTransformComponet);

                            var currentTransformComponent = transformMap[transform.Key];
                            var currentPropagator = _migrationComponenetsFactory.CreateInstance(currentTransformComponent.DllName, currentTransformComponent.MigrationClassName);
                            currentTransformComponent.PropagatorBlock = currentPropagator;
                            transformBlocks.Push(currentTransformComponent);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endVersionName, nextTransformMap);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            var component = transformMap[transform.Key];
                            var propagator = _migrationComponenetsFactory.CreateInstance(component.DllName, component.MigrationClassName);
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
