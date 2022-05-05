using System;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.Postgresql
{
    public class SqliteTargetHostVer15 : SqliteTargetHost
    {
        public override TargetTypes TargetType { get; }

        public override void PrepareTargetHost(TargetContextBase targetContext)
        {
            throw new NotImplementedException();
        }
    }
}
