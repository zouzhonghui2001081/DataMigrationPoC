using CompoundLibrary15 = PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.ProjectCompoundLibrary;
using CompoundLibrary16 = PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ProjectCompoundLibrary;

namespace PerkinElmer.Simplicity.DataTransform.V15ToV16.TansformEntities.Chromatography
{
    public class ProjectCompoundLibrary
    {
        public static CompoundLibrary16 Transform(CompoundLibrary15 compoundLibrary)
        {
            var compoundLibrary16 = new CompoundLibrary16
            {
                Id = compoundLibrary.Id,
                ProjectId = compoundLibrary.ProjectId,
                LibraryName = compoundLibrary.LibraryName,
                LibraryGuid = compoundLibrary.LibraryGuid,
                Description = compoundLibrary.Description,
                CreatedDate = compoundLibrary.CreatedDate,
                CreatedUserId = compoundLibrary.CreatedUserId,
                CreatedUserName = compoundLibrary.CreatedUserName,
                ModifiedDate = compoundLibrary.ModifiedDate,
                ModifiedUserId = compoundLibrary.ModifiedUserId,
                ModifiedUserName = compoundLibrary.ModifiedUserName
            };
            foreach (var compoundLibaryItem in compoundLibrary.CompoundLibraryItems)
                compoundLibrary16.CompoundLibraryItems.Add(CompoundLibraryItem.Transform(compoundLibaryItem));
            return compoundLibrary16;
        }
    }
}
