﻿

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext
{
    public class FileSourceContext : SourceContextBase
    {
        public string FileLocation { get; set; }

        public override SourceTypes SourceType => SourceTypes.Files;
    }
}
