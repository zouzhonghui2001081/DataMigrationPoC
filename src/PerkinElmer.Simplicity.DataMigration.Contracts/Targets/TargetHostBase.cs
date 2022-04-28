
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetHostBase
    {
        public abstract TargetTypes TargetType { get; }

        public abstract void PrepareTargetHost(TargetContextBase targetContext);
    }
}
