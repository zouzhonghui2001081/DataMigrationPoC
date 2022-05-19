using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography
{
    public class CompoundLibraryData : Version15DataBase
    {
        public override Version15DataTypes Version15DataTypes => Version15DataTypes.CompoundLibrary;

        public Guid ProjectGuid { get; set; }

        public ProjectCompoundLibrary ProjectCompoundLibrary { get; set; }
    }

    public class SnapshotCompoundLibraryData
    {
        public SnapshotCompoundLibraryData()
        {
            CompoundLibraryItems = new List<CompoundLibraryItem>();
        }

        public SnapshotCompoundLibrary SnapshotCompoundLibrary { get; set; }

        public IList<CompoundLibraryItem> CompoundLibraryItems { get; set; }
    }
}
