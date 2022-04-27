
namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
	public interface IAcquisitionMethod
	{
		IAcquisitionMethodInfo Info { get; set; }
        bool ReconciledRunTime { get; set; }
        IDeviceMethod[] DeviceMethods { get; set; }
	}
}