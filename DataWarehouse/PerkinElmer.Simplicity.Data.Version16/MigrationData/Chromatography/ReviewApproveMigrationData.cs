using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class ReviewApproveMigrationData : MigrationDataBase
    {
        public ReviewApproveMigrationData()
        {
            ReviewApprovableDataSubEntities = new List<ReviewApprovableDataEntitySubItem>();
        }

        public override ReleaseVersions ReleaseVersion => ReleaseVersions.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ReviewApprove;

        public Guid ProjectGuid { get; set; }

        public ReviewApprovableDataEntity ReviewApprovableDataEntity { get; set; }

        public IList<ReviewApprovableDataEntitySubItem> ReviewApprovableDataSubEntities { get; set; }
    }
}
