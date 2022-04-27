using System;
using PerkinElmer.Domain.Contracts.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition
{
    internal class SequenceInfo : ISequenceInfo
    {
        public string Name { get; set; }

        public Guid Guid { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public IUserInfo CreatedByUser { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public IUserInfo ModifiedByUser { get; set; }
	    
		public object Clone()
	    {
		    throw new NotImplementedException();
	    }
    }
}
