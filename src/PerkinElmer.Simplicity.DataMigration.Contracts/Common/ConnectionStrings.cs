using System;
using System.Collections.Generic;
using System.Text;

namespace PerkinElmer.Simplicity.DataMigration.Contracts.Common
{
    public class ConnectionStrings
    {
        public string Chromatography { get; set; }
        public string AuditTrail { get; set; }
        public string Security { get; set; }

        public string System {  get; set; }
    }
}
