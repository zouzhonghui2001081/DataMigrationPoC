

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext
{
    public class SqliteTargetContext : TargetContextBase
    {
        public string SqliteFileLocation { get; set; }

        public override TargetTypes TargetType => TargetTypes.Sqlite;
    }
}
