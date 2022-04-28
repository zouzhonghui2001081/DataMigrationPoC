using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.ProcessingMethod;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class ProcessingMethodMigrationData : MigrationDataBase
    {
        public ProcessingMethodMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override MigrationVersions MigrationVersion => MigrationVersions.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ProcessingMethod;

        public Guid ProjectGuid { get; set; }

        public ProcessingMethod ProcessingMethod { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
