

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceHost
{
    public abstract class FileSourceHost : SourceHostBase
    {
        public override SourceTypes SourceType => SourceTypes.Files;
    }
}
