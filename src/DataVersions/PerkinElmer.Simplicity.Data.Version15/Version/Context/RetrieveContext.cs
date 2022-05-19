namespace PerkinElmer.Simplicity.Data.Version15.Version.Context
{
    internal class RetrieveContext : ContextBase
    {
        public string SourceFileLocation { get; set; }

        public override ContextTypes ContextType => ContextTypes.Retrieve;
    }
}
