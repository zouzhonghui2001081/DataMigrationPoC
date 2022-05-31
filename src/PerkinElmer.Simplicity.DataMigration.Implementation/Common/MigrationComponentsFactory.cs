using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Newtonsoft.Json;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    public class MigrationComponentsFactory
    {
        private const string ComponentConfiguration = "MigrationComponents.json";

        private readonly string _executingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private IDictionary<string, MigrationLoadContext> _migrationLoadContexts;

        private IDictionary<string, Assembly> _migrationVersionAssemblies;

        public MigrationComponentsFactory()
        {
            LoadMigrationComponents();
            LoadMigrationAssemblies();
            LoadMigrationAssemblyContexts();
        }

        private MigrationComponents MigrationComponents { get; set; }

        private IDictionary<string, IDictionary<string, TransformComponent>> Transforms { get; set; }

        public ISourceBlock<object> CreateSourceBlockInstance(CancellationToken cancellToken, string version)
        {
            throw new NotImplementedException();
            //var assembly = Assembly.LoadFile(Path.Combine(_executingAssemblyPath, versionComponent.DllName));
            //var typeInstance = assembly.GetType(versionComponent.VersionClassName);
            //if (typeInstance != null)
            //    versionComponent.VersionBlock =  Activator.CreateInstance(typeInstance, cancellToken);
            //else
            //    throw new ArgumentException("Component configuration is incorrect!");
        }

        public ITargetBlock<object> CreateTargetBlockInstance(CancellationToken cancellToken, string version)
        {
            throw new NotImplementedException();
        }

        public Stack<IPropagatorBlock<object, object>> CreateTransformBlockInstances(CancellationToken cancellToken, string fromVersion, string toVersion)
        {
            throw new NotImplementedException();
        }

        public object CreateTransformBlockInstance(CancellationToken cancellToken,
            string fromVersion, string toVersion)
        {
           
            throw new NotImplementedException();
            //var assembly = Assembly.LoadFile(Path.Combine(_executingAssemblyPath, transformComponent.DllName));
            //var typeInstance = assembly.GetType(transformComponent.MigrationClassName);
            //if (typeInstance != null)
            //    transformComponent.PropagatorBlock = Activator.CreateInstance(typeInstance, cancellToken);
            //else
            //    throw new ArgumentException("Transform configuration is incorrect!");
        }

        public MethodInfo GetSourceMethodInfo(string version)
        {
            throw new NotImplementedException();
            //if (versionComponent.VersionBlock == null)
            //    throw new ArgumentException("Version block should create first!");

            //var typeInstance = versionComponent.VersionBlock.GetType();
            //return typeInstance.GetMethod(versionComponent.SourceVersionMethodName);
        }

        public static MethodInfo GetTargetMethodInfo(string version)
        {
            throw new NotImplementedException();
            //if(versionComponent.VersionBlock == null)
            //    throw new ArgumentException("Version block should create first!");

            //var typeInstance = versionComponent.VersionBlock.GetType();
            //return typeInstance.GetMethod(versionComponent.TargetVersionMethodName);
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

        private void LoadMigrationAssemblies()
        {
            _migrationVersionAssemblies = new Dictionary<string, Assembly>();
            foreach (var versionComponent in MigrationComponents.VersionComponents)
            {
                var versionAssemblyFilePath = Path.Combine(versionComponent.ComponentFolder, versionComponent.DllName);
                var assembly = Assembly.LoadFrom(versionAssemblyFilePath);
                _migrationVersionAssemblies[assembly.FullName] = assembly;
            }

            foreach (var transformComponent in MigrationComponents.TransformComponents)
            {
                var transformAssemblyFilePath = Path.Combine(transformComponent.ComponentFolder, transformComponent.DllName);
                var assembly = Assembly.LoadFrom(transformAssemblyFilePath);
                _migrationVersionAssemblies[assembly.FullName] = assembly;
            }
        }

        private void LoadMigrationAssemblyContexts()
        {

        }
    }
}
