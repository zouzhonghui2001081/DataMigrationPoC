using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition
{
	public interface IBatchResultSetInfo : IPersistable
	{
		bool IsCompleted { get; set; }
		DataSourceType DataSourceType { get; set; }
	}
}