using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;
using PerkinElmer.Simplicity.DataMigration.Implementation.Common;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationManager
    {
        private readonly VersionComponent _sourceComponent;

        private readonly Stack<TransformComponent> _transformComponenents;

        private readonly VersionComponent _targetComponents;

        public MigrationManager(string startVersion, string endVersion)
        {
            LoadMigrationComponents();
            _sourceComponent = GenerateSourceBlock(startVersion);
            _transformComponenents = GenerateTransformBlock(startVersion, endVersion);
            _targetComponents = GenerateTargetBlock(endVersion);
            if (!BuildPipeline())
                throw new ArgumentException($"Failed setup pipeline from {startVersion} to {endVersion}");
        }

        private IList<VersionComponent> Versions { get; set; }

        private IDictionary<string, IDictionary<string, TransformComponent>> Transforms { get; set; }

        private void LoadMigrationComponents()
        {
            var assembly = typeof(MigrationManager).Assembly;
            var resource = "PerkinElmer.Simplicity.DataMigration.Implementation.MigrationComponents.json";
            using (var stream = assembly.GetManifestResourceStream(resource))
            {
                using (var reader = new StreamReader(stream ?? throw new InvalidOperationException(
                                                         $"Failed to load resource {resource}")))
                {
                    var componentConfig = reader.ReadToEnd();
                    var migrationComponent = JsonConvert.DeserializeObject<MigrationComponents>(componentConfig);

                    Versions = migrationComponent.VersionComponents;
                    var transforms = new Dictionary<string, IDictionary<string, TransformComponent>>();
                    foreach (var transformComponent in migrationComponent.TransformComponents)
                    {
                        if (!transforms.ContainsKey(transformComponent.FromVersion))
                        {
                            transforms[transformComponent.FromVersion] = new Dictionary<string, TransformComponent>
                            {
                                [transformComponent.ToVersion] = transformComponent
                            };
                            continue;
                        }
                        transforms[transformComponent.FromVersion][transformComponent.ToVersion] = transformComponent;
                    }

                    Transforms = transforms;
                }
            }
        }

        public void Start(MigrationContext migrationContext)
        {
            SetTargetType(migrationContext.TargetConfig);
            StartDataflowInternal(migrationContext.SourceConfig);
        }

        private void SetTargetType(string targetConfig)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath, _targetComponents.DllName));

            var typeInstance = assembly.GetType(_targetComponents.MigrationClassName);

            if (typeInstance == null) return;
            var methodInfo = typeInstance.GetMethod(_targetComponents.ApplyTargetConfigMethodName);
            if (methodInfo != null && _targetComponents.VersionBlock != null)
                methodInfo.Invoke(_targetComponents.VersionBlock, new object[] { targetConfig });
        }

        private void StartDataflowInternal(string sourceFlowConfig)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath, _sourceComponent.DllName));
            var typeInstance = assembly.GetType(_sourceComponent.MigrationClassName);

            if (typeInstance == null) return;
            var methodInfo = typeInstance.GetMethod(_sourceComponent.SourceStartMethodName);
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
            var versionComponentInfo = Versions.FirstOrDefault(version => version.Version == sourceVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            var versionInstance = CreateInstance(versionComponentInfo.DllName, versionComponentInfo.MigrationClassName);
            versionComponentInfo.VersionBlock = versionInstance;
            return versionComponentInfo;
        }

        private VersionComponent GenerateTargetBlock(string targetVersionName)
        {
            var versionComponentInfo = Versions.FirstOrDefault(version => version.Version == targetVersionName);
            if (versionComponentInfo == null) throw new ArgumentException("Source version should not be null!");

            var versionInstance = CreateInstance(versionComponentInfo.DllName, versionComponentInfo.MigrationClassName);
            versionComponentInfo.VersionBlock = versionInstance;
            return versionComponentInfo;
        }

        private Stack<TransformComponent> GenerateTransformBlock(string startVersionName, string endVersionName)
        {
            var transformComponents = new Stack<TransformComponent>();
            if (!Transforms.ContainsKey(startVersionName)) return transformComponents;

            var transformMap = Transforms[startVersionName];
            if (!transformMap.ContainsKey(endVersionName))
                return GetTransformBlocksRecursively(endVersionName, transformMap);

            var transformComponent = transformMap[endVersionName];
            var transformInstance = CreateInstance(transformComponent.DllName, transformComponent.MigrationClassName);
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
                if (Transforms.ContainsKey(transform.Key))
                {
                    var nextTransformMap = Transforms[transform.Key];
                    if (nextTransformMap != null && nextTransformMap.Count > 0)
                    {
                        if (nextTransformMap.ContainsKey(endVersionName))
                        {
                            var nextTransformComponet = nextTransformMap[endVersionName];
                            var nextPropagator = CreateInstance(nextTransformComponet.DllName, nextTransformComponet.MigrationClassName);
                            nextTransformComponet.PropagatorBlock = nextPropagator;
                            transformBlocks.Push(nextTransformComponet);

                            var currentTransformComponent = transformMap[transform.Key];
                            var currentPropagator = CreateInstance(currentTransformComponent.DllName, currentTransformComponent.MigrationClassName);
                            currentTransformComponent.PropagatorBlock = currentPropagator;
                            transformBlocks.Push(currentTransformComponent);
                            return transformBlocks;
                        }

                        var subResult = GetTransformBlocksRecursively(endVersionName, nextTransformMap);
                        if (subResult != null && subResult.Count > 0)
                        {
                            transformBlocks = subResult;
                            var component = transformMap[transform.Key];
                            var propagator = CreateInstance(component.DllName, component.MigrationClassName);
                            component.PropagatorBlock = propagator;
                            transformBlocks.Push(component);
                            return transformBlocks;
                        }
                    }
                }
            }
            return transformBlocks;
        }

        private object CreateInstance(string dllName, string className)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath ,dllName));
            var typeInstance = assembly.GetType(className);
            if (typeInstance != null)
                return Activator.CreateInstance(typeInstance, null);

            throw new ArgumentException("Component configuration is incorrect!");
        }
    }
}
