using System;
using System.Configuration;
using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d.Binary
{
    public static class BinaryLayerFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static long InMemoryMaximumSize = 1024*1024*8; 
        static BinaryLayerFactory()
        {
            InMemoryMaximumSize = GetCacheSizeFromConfiguration();
        }

        public static void SetInMemoryMaximumSize(long sz)
        {
            InMemoryMaximumSize = sz;
        }

        private static long GetCacheSizeFromConfiguration()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var configPath = new Uri(currentAssembly.CodeBase).LocalPath;

            long cacheSize = 1024*1024*8;

            try
            {
                var configuration = ConfigurationManager.OpenExeConfiguration(configPath);
                cacheSize = long.Parse(configuration.AppSettings.Settings["MaxSizeInMem"].Value);
            }
            catch (Exception exception)
            {
                Log.Warn("MaxSizeInMem could not be read from configuration file, ignoring it", exception);
            }

            return cacheSize;
        }
        
        public static IBinaryLayer Create(string key, long size)
        {
            if(size < InMemoryMaximumSize)
                return new InMemoryBinaryLayer();
            else
                return new InFileBinaryLayer(key);
        }
    }
}