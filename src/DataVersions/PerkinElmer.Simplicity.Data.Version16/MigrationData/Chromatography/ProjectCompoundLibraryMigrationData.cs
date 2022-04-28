using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class ProjectCompoundLibraryMigrationData : MigrationDataBase
    {
        public override MigrationVersions MigrationVersion => MigrationVersions.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.CompoundLibrary;

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
