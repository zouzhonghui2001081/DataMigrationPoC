using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Acquisition
{
	internal class AcquisitionMethod : IAcquisitionMethod
	{
		public AcquisitionMethod()
		{
			Info = new AcquisitionMethodInfo();
			DeviceMethods = new IDeviceMethod[] {};
		}

		public IAcquisitionMethodInfo Info { get; set; }
        public bool ReconciledRunTime { get; set; }

        public IDeviceMethod[] DeviceMethods { get; set; }
	}
}