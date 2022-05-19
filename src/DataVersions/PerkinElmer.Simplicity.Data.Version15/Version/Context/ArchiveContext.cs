using System;

namespace PerkinElmer.Simplicity.Data.Version15.Version.Context
{
    internal class ArchiveContext : ContextBase
    {
        public Guid ArchiveProjectGuid { get; set; }

        public string DestinationFileLocation { get; set; }

        public override ContextTypes ContextType => ContextTypes.Archive;
    }
}
