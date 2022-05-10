using PerkinElmer.Simplicity.DataMigration.Contracts.Targets;
using PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetHost;

namespace PerkinElmer.Simplicity.Data.Version15.DataTargets.File
{
    public class FileTargetHostVer15 : FileTargetHost
    {
        public override TargetType TargetType { get; }

        public override void PrepareTargetHost(TargetContextBase targetContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
