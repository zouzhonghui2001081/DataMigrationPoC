using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace PerkinElmer.Simplicity.DataMigration.Implementation.Common
{
    internal class MigrationComponenetsFactory
    {
         
        public MigrationComponenetsFactory()
        {
            LoadMigrationComponents();
        }

        public IList<VersionComponent> Versions { get; set; }

        public IDictionary<string, IDictionary<string, TransformComponent>> Transforms { get; set; }

        private void LoadMigrationComponents()
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var resourcePath = Path.Combine(outputPath, "MigrationComponents.json");
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

        public object CreateInstance(string dllName, string className)
        {
            var outputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assembly = Assembly.LoadFile(Path.Combine(outputPath, dllName));
            var typeInstance = assembly.GetType(className);
            if (typeInstance != null)
                return Activator.CreateInstance(typeInstance, null);

            throw new ArgumentException("Component configuration is incorrect!");
        }
    }
}
