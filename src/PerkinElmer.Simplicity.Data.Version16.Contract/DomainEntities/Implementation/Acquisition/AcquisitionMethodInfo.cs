using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
	internal class AcquisitionMethodInfo : IAcquisitionMethodInfo
	{
		public AcquisitionMethodInfo()
		{
			Name = string.Empty;
            VersionNumber = 1;
            Guid = Guid.NewGuid();
			CreatedDateUtc = DateTime.UtcNow;
			CreatedByUser = new UserInfo();
			ModifiedDateUtc = DateTime.UtcNow;
			ModifiedByUser = new UserInfo();
			Devices = new string[] { };
            ReviewApproveState = ReviewApproveState.NeverSubmitted;
        }

		public string Name { get; set; }

        public int VersionNumber { get; set; } = 1;

        public Guid Guid { get; set; }

		public DateTime CreatedDateUtc { get; set; }

		public IUserInfo CreatedByUser { get; set; }

		public DateTime ModifiedDateUtc { get; set; }

		public IUserInfo ModifiedByUser { get; set; }

		public string[] Devices { get; set; }

        public ReviewApproveState ReviewApproveState { get; set; }
        public bool IsModifiedAfterSubmission { get; set; }

        public object Clone()
		{
			throw new NotImplementedException();
		}
	}
}