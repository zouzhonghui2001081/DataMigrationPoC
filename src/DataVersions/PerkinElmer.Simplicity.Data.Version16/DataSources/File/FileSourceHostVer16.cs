using System;
using System.Collections.Generic;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source;
using PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost;

namespace PerkinElmer.Simplicity.Data.Version16.DataSources.File
{
    public class FileSourceHostVer16 : FileSourceHost
    {
        public override IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext)
        {
            throw new NotImplementedException();
        }
    }
}
