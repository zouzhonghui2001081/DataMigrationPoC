using System;

namespace PerkinElmer.Simplicity.Data.Common.Postgresql
{
    public class SchemaVersions
    {
        public static Version ChromatographySchemaVersion15 => new Version(1, 7);

        public static Version AuditTrailSchemaVersion15 => new Version(0, 5);

        public static Version ChromatographySchemaVersion16 => new Version(1, 10);

        public static Version AuditTrailSchemaVersion16 => new Version(0, 6);
    }
}
