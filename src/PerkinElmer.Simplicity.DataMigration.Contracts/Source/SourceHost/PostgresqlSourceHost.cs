

using PerkinElmer.Simplicity.DataMigration.Contracts.Common;
using System;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class PostgresqlSourceHost : SourceHostBase
    {
        public override SourceType SourceType => SourceType.Posgresql;
        public abstract Version AuditTrailSchemaVersion { get; }
        public abstract Version SecuritySchemaVersion { get; }
        public abstract Version ChromatographySchemaVersion { get; }
        public abstract int ChromatographyMajorDataVersion { get; }
        public abstract int ChromatographyMinorDataVersion { get; }
        protected abstract string ConnectionStringResourceName { get; }

        private ConnectionStrings _connectionStrings = null;
        public ConnectionStrings ConnectionStrings
        {
            get
            {
                if (_connectionStrings == null)
                {
                    _connectionStrings = GetConnectionStrings();
                }
                return _connectionStrings;
            }
        }

        protected abstract ConnectionStrings GetConnectionStrings();
    }
}
 
