using System.Threading.Tasks.Dataflow;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Transform.TransformContext
{
    public abstract class TransformContextBase
    {
        public ExecutionDataflowBlockOptions BlockOption { get; set; }
    }
}
