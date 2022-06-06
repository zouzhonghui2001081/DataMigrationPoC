using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version16.Contract.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.Version.Chromatography
{
    public class ReviewApproveVersion16Data : Version16DataBase
    {
        public ReviewApproveVersion16Data()
        {
            ReviewApprovableDataSubEntities = new List<ReviewApprovableDataEntitySubItem>();
        }

        public override Version16DataTypes Version16DataTypes => Version16DataTypes.ReviewApprove;

        public Guid ProjectGuid { get; set; }

        public ReviewApprovableDataEntity ReviewApprovableDataEntity { get; set; }

        public IList<ReviewApprovableDataEntitySubItem> ReviewApprovableDataSubEntities { get; set; }
    }
}
