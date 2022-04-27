using System;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition
{
	internal class BatchRunInfo : IBatchRunInfo
	{
		public string Name { get; set; }

		public Guid Guid { get; set; }

		public DateTime CreatedDateUtc { get; set; }

		public IUserInfo CreatedByUser { get; set; }

		public DateTime ModifiedDateUtc { get; set; }

		public IUserInfo ModifiedByUser { get; set; }

        public IAcquisitionRunInfo AcquisitionRunInfo { get; set; }
        public int RepeatIndex { get; set; }

		public ISequenceSampleInfo SequenceSampleInfo { get; set; }
		
		public DataSourceType DataSourceType { get; set; }

		public object Clone()
		{
			throw new NotImplementedException();
		}
	}
}