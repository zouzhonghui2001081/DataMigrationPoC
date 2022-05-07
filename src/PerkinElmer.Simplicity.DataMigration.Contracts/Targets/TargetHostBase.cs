
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets
{
    public abstract class TargetHostBase
    {
        public abstract TargetType TargetType { get; }

        public abstract void PrepareTargetHost(TargetContextBase targetContext);
    }
}
