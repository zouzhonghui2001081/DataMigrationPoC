using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography.AcquisitionMethod;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class AcqusitionMethodMigrationData : MigrationDataBase
    {
        public AcqusitionMethodMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override MigrationVersion MigrationVersion => MigrationVersion.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.AcqusitionMethod;

        public Guid ProjectGuid { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
