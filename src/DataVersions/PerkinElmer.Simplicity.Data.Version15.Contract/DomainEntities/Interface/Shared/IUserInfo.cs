using System;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared
{
	public interface IUserInfo : ICloneable, IEquatable<IUserInfo>
	{
		string UserId { get; set; }

		string UserFullName { get; set; }

        string UserName { get; set; }
	}
}