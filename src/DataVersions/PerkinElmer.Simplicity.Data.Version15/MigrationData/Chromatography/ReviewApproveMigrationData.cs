using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class ReviewApproveMigrationData : MigrationDataBase
    {
        public ReviewApproveMigrationData()
        {
            ReviewApprovableDataSubEntities = new List<ReviewApprovableDataEntitySubItem>();
        }

        public override MigrationVersions MigrationVersion => MigrationVersions.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ReviewApprove;

        public Guid ProjectGuid { get; set; }

        public ReviewApprovableDataEntity ReviewApprovableDataEntity { get; set; }

        public IList<ReviewApprovableDataEntitySubItem> ReviewApprovableDataSubEntities { get; set; }
    }
}
