using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
	public interface IPersistable: ICloneable
	{
		string Name { get; set; }

		Guid Guid { get; set; }

		DateTime CreatedDateUtc { get; set; }

		IUserInfo CreatedByUser { get; set; }

		DateTime ModifiedDateUtc { get; set; }

		IUserInfo ModifiedByUser { get; set; }
	}
}