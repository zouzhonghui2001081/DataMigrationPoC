﻿using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography.AcquisitionMethod;
namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class AcqusitionMethodMigrationData : MigrationDataBase
    {
        public AcqusitionMethodMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override MigrationVersion MigrationVersion => MigrationVersion.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.AcqusitionMethod;

        public Guid ProjectGuid { get; set; }

        public AcquisitionMethod AcquisitionMethod { get; set; }

        public ReviewApproveMigrationData ReviewApproveData { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
        
    }
}
