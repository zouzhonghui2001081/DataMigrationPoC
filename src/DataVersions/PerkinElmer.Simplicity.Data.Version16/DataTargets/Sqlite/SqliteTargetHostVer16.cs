using System;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;

namespace PerkinElmer.Simplicity.Data.Version16.DataTargets.Sqlite
{
    public class SqliteTargetHostVer16 : SqliteTargetHost
    {
        public override TargetType TargetType { get; }

        public override void PrepareTargetHost(TargetContextBase targetContext)
        {
            throw new NotImplementedException();
        }
    }
}
