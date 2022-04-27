using System.Threading.Tasks.Dataflow;

namespace PerkinElmer.Simplicity.Data.Contracts.Transform.TransformContext
{
    public abstract class TransformContextBase
    {
        public ExecutionDataflowBlockOptions BlockOption { get; set; }
    }
}
