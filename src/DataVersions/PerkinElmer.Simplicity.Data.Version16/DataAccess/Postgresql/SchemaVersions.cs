using System;

namespace PerkinElmer.Simplicity.Data.Version15.DataAccess.Postgresql
{
    public class SchemaVersions
    {
        public static Version ChromatographySchemaVersion => new Version(1, 10);

        public static Version AuditTrailSchemaVersion => new Version(0, 6);

        public static Version SecurityVersion => new Version(1, 8);

    }
}
