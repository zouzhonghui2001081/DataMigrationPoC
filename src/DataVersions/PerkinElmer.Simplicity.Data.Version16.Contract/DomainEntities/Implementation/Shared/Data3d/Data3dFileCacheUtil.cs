using System;
using System.IO;
using System.Reflection;
using log4net;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared.Data3d
{
    /// <summary>
    /// This utility encapsulates the location of the file cache, and the file naming pattern for files in this location.
    /// </summary>
    public static class Data3dFileCacheUtil
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string CacheFolderName = "$$$SC.FileData$$$";

        static Data3dFileCacheUtil()
        {
            Initialize3dFileCache();
        }

        /// <summary>
        /// Creates the cache folder, if it does not exist. If it exists and has old files in it, cleans it.
        /// </summary>
        internal static void Initialize3dFileCache()
        {
            var cacheFolderPath = Path.Combine(Path.GetTempPath(), CacheFolderName);

            if (!CreateFolderIfDoesNotExist(cacheFolderPath))
                CleanFolder(cacheFolderPath);
        }

        /// <summary>
        /// Returns the full path to the two files where the 3D cache for given batch run should be written.
        /// One file for spectra-wise storage, and another for chromatogram-wise storage (aka transposed). 
        /// Side effect: if the folder for cache does not exist, it will be created.
        /// </summary>
        public static (string ForSpectra, string ForChromatograms) GetFileNamesForBatchRun(Guid origBrGuid)
        {
            //Filename Format: <batchRunGuid>_<fileUniqueGuid>[<_T for per-chromatogram files>].bin
            //file-unique-Guid part of the name is added in case a previous instance of the same BatchRun cache file is there 
            var perFileGuid = Guid.NewGuid();
            var fileNameSpectra = $"{origBrGuid}_{perFileGuid}.bin";
            var fileNameChromatograms = $"{origBrGuid}_{perFileGuid}_T.bin";
            
            // We make sure the folder exists. Needed if user deleted the folder, or changed TEMP variable, in the middle of application run 
            var cacheFolderPath = Path.Combine(Path.GetTempPath(), CacheFolderName);
            CreateFolderIfDoesNotExist(cacheFolderPath);
            
            var fullFileNameSpectra = Path.Combine(cacheFolderPath, fileNameSpectra);
            var fullFileNameChromatogram = Path.Combine(cacheFolderPath, fileNameChromatograms);

            return (fullFileNameSpectra, fullFileNameChromatogram);
        }

        /// <summary>
        /// Returns true, if the folder was created.
        /// </summary>
        private static bool CreateFolderIfDoesNotExist(string cacheFolderPath)
        {
            if (Directory.Exists(cacheFolderPath))
                return false;
            
            var dirInfo = new DirectoryInfo(cacheFolderPath);
            dirInfo.Create();
            return true;
        }

        private static void CleanFolder(string cacheFolderPath)
        {
            var dirInfo = new DirectoryInfo(cacheFolderPath);
            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception exc)
                {
                    Log.Warn($"Unable to delete '{file.FullName}'", exc);
                }
            }
        }
    }
}