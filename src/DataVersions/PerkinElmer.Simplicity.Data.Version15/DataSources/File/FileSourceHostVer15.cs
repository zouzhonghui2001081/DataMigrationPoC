using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version15.DataSources.Postgresql
{
    public class FileSourceHostVer15 : FileSourceHost
    {
        public override IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext)
        {
            throw new NotImplementedException();
        }
    }
}
