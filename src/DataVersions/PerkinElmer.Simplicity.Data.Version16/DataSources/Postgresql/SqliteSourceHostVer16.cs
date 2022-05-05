using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.Postgresql
{
    public class SqliteSourceHostVer16 : SqliteSourceHost
    {
        public override IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext)
        {
            throw new NotImplementedException();
        }
    }
}
