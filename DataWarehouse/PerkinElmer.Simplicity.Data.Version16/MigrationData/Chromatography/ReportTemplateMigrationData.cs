﻿using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version16.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version16.MigrationData.Chromatography
{
    public class ReportTemplateMigrationData : MigrationDataBase
    {
        public ReportTemplateMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override ReleaseVersions ReleaseVersion => ReleaseVersions.Version16;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.ReportTemplate;

        public Guid ProjectGuid { get; set; }

        public ReportTemplate ReportTemplate { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
