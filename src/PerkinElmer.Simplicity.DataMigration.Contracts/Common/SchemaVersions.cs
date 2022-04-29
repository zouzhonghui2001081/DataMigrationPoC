﻿using System;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Common
{
    public class SchemaVersions
    {
        public static Version ChromatographySchemaVersion15 => new Version(1, 7);

        public static Version AuditTrailSchemaVersion15 => new Version(0, 5);

        public static Version SecurityVersion15 => new Version(1, 8);

        public static Version ChromatographySchemaVersion16 => new Version(1, 10);

        public static Version AuditTrailSchemaVersion16 => new Version(0, 6);

        public static Version SecurityVersion16 => new Version(1, 8);
    }
}