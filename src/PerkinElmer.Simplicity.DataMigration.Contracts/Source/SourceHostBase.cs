using System.Collections.Generic;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source
{
    public abstract class SourceHostBase
    {
        public abstract SourceType SourceType { get; }

        public abstract IList<SourceParamBase> GetSourceBlockInputParams(SourceContextBase sourceContext);
    }
}
