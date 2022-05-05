

using System;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class PostgresqlSourceHost : SourceHostBase
    {
        public override SourceTypes SourceType => SourceTypes.Posgresql;
        public abstract Version AuditTrailSchemaVersion { get; }
        public abstract Version SecuritySchemaVersion { get; }
        public abstract Version ChromatographySchemaVersion { get; }
        public abstract int ChromatographyMajorDataVersion { get; }
        public abstract int ChromatographyMinorDataVersion { get; }
    }
}
 
