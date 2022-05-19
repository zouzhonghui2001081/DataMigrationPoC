using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ReviewApprove;

namespace PerkinElmer.Simplicity.Data.Version15.Version.Data.Chromatography
{
    public class ReviewApproveData : Version15DataBase
    {
        public ReviewApproveData()
        {
            ReviewApprovableDataSubEntities = new List<ReviewApprovableDataEntitySubItem>();
        }

        public override Version15DataTypes Version15DataTypes => Version15DataTypes.ReviewApprove;

        public Guid ProjectGuid { get; set; }

        public ReviewApprovableDataEntity ReviewApprovableDataEntity { get; set; }

        public IList<ReviewApprovableDataEntitySubItem> ReviewApprovableDataSubEntities { get; set; }
    }
}
