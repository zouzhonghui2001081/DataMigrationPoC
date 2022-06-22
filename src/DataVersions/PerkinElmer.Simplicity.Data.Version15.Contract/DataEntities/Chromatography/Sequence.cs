using System;
using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography
{
    public class Sequence
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedUserId { get; set; }
        public string CreatedUserName { get; set; }
        
        public DateTime ModifiedDate { get; set; }

        public string ModifiedUserId { get; set; }
        public string ModifiedUserName { get; set; }
        public long ProjectId { get; set; }

	    public IList<SequenceSampleInfo> SequenceSampleInfos { get; set; }
    }
}
