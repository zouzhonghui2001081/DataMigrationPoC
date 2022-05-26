using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Newtonsoft.Json;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    internal class MigrationComponenetsFactory
    {
        private const string ComponentConfiguration = "MigrationComponents.json";

        private static readonly string ExecutingAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        static MigrationComponenetsFactory()
        {
            LoadMigrationComponents();
        }

        public static IList<VersionComponent> Versions { get; set; }

        public static IDictionary<string, IDictionary<string, TransformComponent>> Transforms { get; set; }

        public static void CreateVersionBlockInstance(CancellationToken cancellToken, VersionComponent versionComponent)
        {
            if (versionComponent == null) throw new ArgumentException("Version component should not be null!");
            if (versionComponent.VersionBlock != null) return;

            var assembly = Assembly.LoadFile(Path.Combine(ExecutingAssemblyPath, versionComponent.DllName));
            var typeInstance = assembly.GetType(versionComponent.VersionClassName);
            if (typeInstance != null)
                versionComponent.VersionBlock =  Activator.CreateInstance(typeInstance, cancellToken);
            else
                throw new ArgumentException("Component configuration is incorrect!");
        }

        public static void CreateTransformBlockInstance(CancellationToken cancellToken,
            TransformComponent transformComponent)
        {
            if (transformComponent == null) throw new ArgumentException("Transform component should not be null!");
            if (transformComponent.PropagatorBlock != null) return;

            var assembly = Assembly.LoadFile(Path.Combine(ExecutingAssemblyPath, transformComponent.DllName));
            var typeInstance = assembly.GetType(transformComponent.MigrationClassName);
            if (typeInstance != null)
                transformComponent.PropagatorBlock = Activator.CreateInstance(typeInstance, cancellToken);
            else
                throw new ArgumentException("Transform configuration is incorrect!");
        }

        public static MethodInfo GetSourceMethodInfo(VersionComponent versionComponent)
        {
            if (versionComponent.VersionBlock == null)
                throw new ArgumentException("Version block should create first!");

            var typeInstance = versionComponent.VersionBlock.GetType();
            return typeInstance.GetMethod(versionComponent.SourceVersionMethodName);
        }

        public static MethodInfo GetTargetMethodInfo(VersionComponent versionComponent)
        {
            if(versionComponent.VersionBlock == null)
                throw new ArgumentException("Version block should create first!");

            var typeInstance = versionComponent.VersionBlock.GetType();
            return typeInstance.GetMethod(versionComponent.TargetVersionMethodName);
        }

        private static void LoadMigrationComponents()
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(outputPath)) throw new ArgumentException("Output path is null!");
            var resourcePath = Path.Combine(outputPath, ComponentConfiguration);
            if (!File.Exists(resourcePath))
                throw new ArgumentException("The migration components configration not exist!");

            var migrationComponent = JsonConvert.DeserializeObject<MigrationComponents>(File.ReadAllText(resourcePath));
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
