using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class ReportTemplateMigrationData : MigrationDataBase
    {
        public ReportTemplateMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override MigrationVersions MigrationVersion => MigrationVersions.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ReportTemplate;

        public Guid ProjectGuid { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
