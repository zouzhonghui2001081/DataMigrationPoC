using SnapshotCompoundLibrary15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.SnapshotCompoundLibrary;
using SnapshotCompoundLibrary16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.SnapshotCompoundLibrary;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class SnapshotCompoundLibrary
    {
        public static SnapshotCompoundLibrary16 Transform(
            SnapshotCompoundLibrary15 snapshotCompoundLibrary)
        {
            if (snapshotCompoundLibrary == null) return null;
            return new SnapshotCompoundLibrary16
            {
                Id = snapshotCompoundLibrary.Id,
                AnalysisResultSetId = snapshotCompoundLibrary.AnalysisResultSetId,
                LibraryName = snapshotCompoundLibrary.LibraryName,
                LibraryGuid = snapshotCompoundLibrary.LibraryGuid,
                CreatedDate = snapshotCompoundLibrary.CreatedDate,
                ModifiedDate = snapshotCompoundLibrary.ModifiedDate
            };
        }
    }
}
