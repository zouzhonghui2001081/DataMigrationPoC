using System.Collections.Generic;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source
{
    public abstract class SourceHostBase
    {
        public abstract SourceTypes SourceType { get; }

        public abstract IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext);
    }
}
