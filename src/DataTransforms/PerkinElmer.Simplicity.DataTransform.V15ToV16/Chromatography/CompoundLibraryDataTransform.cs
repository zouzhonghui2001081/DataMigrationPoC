using System.Reflection;
using log4net;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using CompoundLibraryData = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.CompoundLibraryData;
using SnapshotCompoundLibraryData15 = PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography.SnapshotCompoundLibraryData;
using SnapshotCompoundLibraryData16 = PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography.SnapshotCompoundLibraryData;
namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    internal class CompoundLibraryDataTransform 
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static Data.Version16.Version.Data.Chromatography.CompoundLibraryData Transform(CompoundLibraryData compoundLibraryData)
        {
            return new Data.Version16.Version.Data.Chromatography.CompoundLibraryData
            {
                ProjectGuid = compoundLibraryData.ProjectGuid,
                ProjectCompoundLibrary = ProjectCompoundLibrary.Transform(compoundLibraryData.ProjectCompoundLibrary)
            };
        }

        public static SnapshotCompoundLibraryData16 TransformSnapshotCompoundLibary(
            SnapshotCompoundLibraryData15 snapshotCompoundLibraryData)
        {
            var snapshotCompoundLibraryData16 = new SnapshotCompoundLibraryData16
            {
                SnapshotCompoundLibrary = SnapshotCompoundLibrary.Transform(snapshotCompoundLibraryData.SnapshotCompoundLibrary)
            };
            foreach (var compoundLibraryItem in snapshotCompoundLibraryData.CompoundLibraryItems)
            {
                snapshotCompoundLibraryData16.CompoundLibraryItems.Add(CompoundLibraryItem.Transform(compoundLibraryItem));
            }
            return snapshotCompoundLibraryData16;
        }
       
    }
}
