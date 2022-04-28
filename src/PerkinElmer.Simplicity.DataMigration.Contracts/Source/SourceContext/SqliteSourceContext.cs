﻿namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source.SourceContext
{
    public class SqliteSourceContext : SourceContextBase
    {
        public string SqliteFileLocation { get; set; }

        public override SourceTypes SourceType => SourceTypes.Sqlite;
    }
}
