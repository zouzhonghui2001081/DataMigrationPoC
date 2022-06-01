using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    public class MigrationComponentsFactory
    {
        private const string ComponentConfiguration = "MigrationComponents.json";

        private IDictionary<string, MigrationLoadContext> _migrationLoadContexts;

        private IDictionary<string, Assembly> _migrationVersionAssemblies;

        public MigrationComponentsFactory()
        {
            LoadMigrationComponents();
            LoadMigrationAssemblyContexts();
        }

        private MigrationComponents MigrationComponents { get; set; }

        private IDictionary<string, IDictionary<string, TransformComponent>> Transforms { get; set; }

        public ISourceBlock<object> CreateSourceBlockInstance(CancellationToken cancellToken, string version)
        {
            var migrationComponent = MigrationComponents.VersionComponents.FirstOrDefault(component => component.Version == version);
            if (migrationComponent == null) throw new ArgumentException(nameof(version));
            var assembly = _migrationVersionAssemblies[migrationComponent.DllName];
            var typeInstance = assembly.GetType(migrationComponent.VersionClassName);
            var instance = Activator.CreateInstance(typeInstance, cancellToken);
            if (!(instance is ISourceBlock<object> sourceBlock))
                throw new ArgumentException(nameof(migrationComponent));
            return sourceBlock;
        }

        public ITargetBlock<object> CreateTargetBlockInstance(CancellationToken cancellToken, string version)
        {
            var migrationComponent = MigrationComponents.VersionComponents.FirstOrDefault(component => component.Version == version);
            if (migrationComponent == null) throw new ArgumentException(nameof(version));
            var assembly = _migrationVersionAssemblies[migrationComponent.DllName];
            var typeInstance = assembly.GetType(migrationComponent.VersionClassName);
            var instance = Activator.CreateInstance(typeInstance, cancellToken);
            if (!(instance is ITargetBlock<object> targetBlock))
                throw new ArgumentException(nameof(migrationComponent));
            return targetBlock;
        }

        public Stack<IPropagatorBlock<object, object>> CreateTransformBlockInstances(CancellationToken cancellToken, string fromVersion, string toVersion)
        {
            throw new NotImplementedException();
        }

        private IPropagatorBlock<object, object> CreateTransformBlockInstance(CancellationToken cancellToken,
            string fromVersion, string toVersion)
        {

            var migrationComponent = MigrationComponents.TransformComponents.FirstOrDefault(component =>
                component.FromVersion == fromVersion && component.ToVersion == toVersion);
            if (migrationComponent == null) throw new ArgumentException($"From Version {fromVersion} or To Version {toVersion} is incorrest!");
            var assembly = _migrationVersionAssemblies[migrationComponent.DllName];
            var typeInstance = assembly.GetType(migrationComponent.MigrationClassName);
            var instance = Activator.CreateInstance(typeInstance, cancellToken);

            if (!(instance is IPropagatorBlock<object, object> transformBlock))
                throw new ArgumentException(nameof(migrationComponent));
            return transformBlock;
        }

        public MethodInfo GetSourceMethodInfo(string version)
        {
            var migrationComponent = MigrationComponents.VersionComponents.FirstOrDefault(component => component.Version == version);
            if (migrationComponent == null) throw new ArgumentException(nameof(version));
            var assembly = _migrationVersionAssemblies[migrationComponent.DllName];
            var typeInstance = assembly.GetType(migrationComponent.VersionClassName);
            return typeInstance.GetMethod(migrationComponent.SourceVersionMethodName);
        }

        public MethodInfo GetTargetMethodInfo(string version)
        {
            var migrationComponent = MigrationComponents.VersionComponents.FirstOrDefault(component => component.Version == version);
            if (migrationComponent == null) throw new ArgumentException(nameof(version));
            var assembly = _migrationVersionAssemblies[migrationComponent.DllName];
            var typeInstance = assembly.GetType(migrationComponent.VersionClassName);
            return typeInstance.GetMethod(migrationComponent.TargetVersionMethodName);
        }

        private void LoadMigrationComponents()
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Output path is null!");
            var resourcePath = Path.Combine(outputPath, ComponentConfiguration);
            if (!File.Exists(resourcePath))
                throw new ArgumentException("The migration components configration not exist!");

            MigrationComponents = JsonConvert.DeserializeObject<MigrationComponents>(File.ReadAllText(resourcePath));
            var transforms = new Dictionary<string, IDictionary<string, TransformComponent>>();
            foreach (var transformComponent in MigrationComponents.TransformComponents)
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

        private void LoadMigrationAssemblyContexts()
        {
            //TODO: Need research how to unload migration load contenxt.
            _migrationLoadContexts = new Dictionary<string, MigrationLoadContext>();
            _migrationVersionAssemblies = new Dictionary<string, Assembly>();

            foreach (var versionComponent in MigrationComponents.VersionComponents)
            {
                var versionAssemblyFilePath = Path.Combine(versionComponent.ComponentFolder, versionComponent.DllName);
                var assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(versionAssemblyFilePath));
                var migrationLoadContext = new MigrationLoadContext(versionAssemblyFilePath);
                _migrationLoadContexts[versionComponent.DllName] = migrationLoadContext;
                _migrationVersionAssemblies[versionComponent.DllName] = migrationLoadContext.LoadFromAssemblyName(assemblyName);
            }
            foreach(var transformComponent in MigrationComponents.TransformComponents)
            {
                var transformAssemblyFilePath = Path.Combine(transformComponent.ComponentFolder, transformComponent.DllName);
                var assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(transformAssemblyFilePath));
                var migrationLoadContext = new MigrationLoadContext(transformAssemblyFilePath);
                _migrationLoadContexts[transformComponent.DllName] = migrationLoadContext;
                _migrationVersionAssemblies[transformComponent.DllName] = migrationLoadContext.LoadFromAssemblyName(assemblyName);
            }
        }
    }
}
