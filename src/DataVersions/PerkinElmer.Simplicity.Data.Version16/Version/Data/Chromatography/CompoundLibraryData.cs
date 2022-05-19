using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.Version.Data.Chromatography
{
    public class CompoundLibraryData : Version16DataBase
    {
        public override Version16DataTypes Version16DataTypes => Version16DataTypes.CompoundLibrary;

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
