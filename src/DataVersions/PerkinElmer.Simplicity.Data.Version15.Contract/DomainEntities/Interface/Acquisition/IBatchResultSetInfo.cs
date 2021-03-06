using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition
{
	public interface IBatchResultSetInfo : IPersistable
	{
		bool IsCompleted { get; set; }
		DataSourceType DataSourceType { get; set; }
	}
}