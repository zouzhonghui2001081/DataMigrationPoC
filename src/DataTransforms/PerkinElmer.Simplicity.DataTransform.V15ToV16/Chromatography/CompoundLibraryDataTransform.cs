using System;
using System.Reflection;
using System.Threading.Tasks.Dataflow;
using log4net;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform;
using PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext;
using PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography;
using PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography;
using SnapshotCompoundLibraryData15 = PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography.SnapshotCompoundLibraryData;
using SnapshotCompoundLibraryData16 = PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography.SnapshotCompoundLibraryData;
namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.Chromatography
{
    public class CompoundLibraryDataTransform : TransformBlockCreatorBase
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override MigrationVersions FromVersion => MigrationVersions.Version15;

        public override MigrationVersions ToVersion => MigrationVersions.Version16;

        public override TransformBlock<MigrationDataBase, MigrationDataBase> CreateTransform(TransformContextBase transformContext)
        {
            var projectCompoundLibraryTransformBlock = new TransformBlock<MigrationDataBase, MigrationDataBase>(
                fromVersionData =>
                {
                    if (fromVersionData.MigrationVersion != MigrationVersions.Version15 ||
                        !(fromVersionData is Data.Version15.MigrationData.Chromatography.ProjectCompoundLibraryMigrationData compoundLibraryData))
                        throw new ArgumentException("From version data is incorrect!");
                    return Transform(compoundLibraryData);
                }, transformContext.BlockOption);
            projectCompoundLibraryTransformBlock.Completion.ContinueWith(_ =>
            {
                Log.Info($"project compound library transform complete with State{_.Status}");
            });
            return projectCompoundLibraryTransformBlock;
        }

        internal static ProjectCompoundLibraryMigrationData Transform(Data.Version15.MigrationData.Chromatography.ProjectCompoundLibraryMigrationData projectProjectCompoundLibraryMigrationData)
        {
            return new ProjectCompoundLibraryMigrationData
            {
                ProjectGuid = projectProjectCompoundLibraryMigrationData.ProjectGuid,
                ProjectCompoundLibrary = ProjectCompoundLibrary.Transform(projectProjectCompoundLibraryMigrationData.ProjectCompoundLibrary)
            };
        }

        internal static SnapshotCompoundLibraryData16 TransformSnapshotCompoundLibary(
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
