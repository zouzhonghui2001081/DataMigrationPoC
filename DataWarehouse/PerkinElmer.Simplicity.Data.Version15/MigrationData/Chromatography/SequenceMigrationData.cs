﻿using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.Data.Common;
using PerkinElmer.Simplicity.Data.Contracts.Migration;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.AuditTrail;
using PerkinElmer.Simplicity.Data.Version15.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.MigrationData.Chromatography
{
    public class SequenceMigrationData : MigrationDataBase
    {
        public SequenceMigrationData()
        {
            AuditTrailLogs = new List<AuditTrailLogEntry>();
        }

        public override ReleaseVersions ReleaseVersion => ReleaseVersions.Version15;

        public override MigrationDataTypes MigrationDataTypes => MigrationDataTypes.Sequence;

        public Guid ProjectGuid { get; set; }

        //TODO: Consider List<Sequence> Sequences
        public Sequence Sequence { get; set; }

        public IList<AuditTrailLogEntry> AuditTrailLogs { get; set; }
    }
}
