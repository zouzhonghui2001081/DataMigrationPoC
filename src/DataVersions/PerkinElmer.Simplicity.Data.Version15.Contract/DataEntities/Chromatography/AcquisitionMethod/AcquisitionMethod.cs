using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography.AcquisitionMethod
{
	public class AcquisitionMethod
	{
		public long Id { get; set; }
		public string MethodName { get; set; }
        public int VersionNumber { get; set; } = 1;
        public bool ReconciledRunTime { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime ModifyDate { get; set; }
		public string CreateUserId { get; set; }
		public string CreateUserName { get; set; }
		public string ModifyUserId { get; set; }
		public string ModifyUserName { get; set; }
		public string[] MethodDevices { get; set; }
		public Guid Guid { get; set; }
		public DeviceMethod[] DeviceMethods { get; set; }
		public short ReviewApproveState { get; set; }
    }
}
