using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
    internal class BatchResultSetInfo : IBatchResultSetInfo
    {
        public string Name { get; set; }

        public Guid Guid { get; set; } = Guid.NewGuid();

        public DateTime CreatedDateUtc { get; set; }

        public IUserInfo CreatedByUser { get; set; }

        public DateTime ModifiedDateUtc { get; set; }

        public IUserInfo ModifiedByUser { get; set; }

	    public bool IsCompleted { get; set; }
	    public DataSourceType DataSourceType { get; set; }
	    
		public object Clone()
	    {
		    throw new NotImplementedException();
	    }
    }
}
