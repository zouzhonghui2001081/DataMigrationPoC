using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
	public interface IBatchRunInfo : IPersistable
    {
        IAcquisitionRunInfo AcquisitionRunInfo { get; set; }
		int RepeatIndex { get; set; }
		ISequenceSampleInfo SequenceSampleInfo { get; set; }
		DataSourceType DataSourceType { get; set; }
	}
}