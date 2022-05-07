using System.Threading.Tasks.Dataflow;
using PerkinElmer.Simplicity.DataMigration.Contracts.Migration;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Source
{
    public abstract class SourceBlockCreatorBase
    {
        public abstract MigrationVersion SourceVersion { get; }

        public abstract SourceType SourceType { get; }

        public abstract IPropagatorBlock<SourceParamBase, MigrationDataBase> CreateSourceBlock(SourceContextBase sourceContext);
    }
}
