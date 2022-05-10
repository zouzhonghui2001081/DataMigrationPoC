namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IAcquisitionMethod
	{
		IAcquisitionMethodInfo Info { get; set; }
        bool ReconciledRunTime { get; set; }
        IDeviceMethod[] DeviceMethods { get; set; }
	}
}