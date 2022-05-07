
namespace PerkinElmer.Simplicity.DataMigration.Contracts.Targets.TargetContext
{
    public class FileTargetContext : TargetContextBase
    {
        public string FilePath { get; set; }

        public override TargetType TargetType => TargetType.Files;
    }
}
