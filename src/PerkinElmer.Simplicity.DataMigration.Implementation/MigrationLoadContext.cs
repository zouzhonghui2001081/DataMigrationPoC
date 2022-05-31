using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;

namespace PerkinElmer.Simplicity.DataMigration.Implementation
{
    public class MigrationLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;
        private readonly IDictionary<string, Assembly> _sharedVersionComponents;

        public MigrationLoadContext(string versionLocation, IDictionary<string, Assembly> versionComponents)
        {
            _sharedVersionComponents = versionComponents;
            _resolver = new AssemblyDependencyResolver(versionLocation);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            if (_sharedVersionComponents.ContainsKey(assemblyName.FullName))
                return _sharedVersionComponents[assemblyName.FullName];

            var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
            return assemblyPath != null ? LoadFromAssemblyPath(assemblyPath) : null;
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            string libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return libraryPath != null ? LoadUnmanagedDllFromPath(libraryPath) : IntPtr.Zero;
        }
    }
}
