using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared
{
	public class UserInfo : IUserInfo
	{
		public string UserId { get; set; } = string.Empty;

		public string UserFullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public object Clone()
		{
			return MemberwiseClone();
		}

		public bool Equals(IUserInfo other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(UserId, other.UserId) && string.Equals(UserFullName, other.UserFullName);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((IUserInfo) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((UserId != null ? UserId.GetHashCode() : 0) * 397) ^ (UserFullName != null ? UserFullName.GetHashCode() : 0);
			}
		}
	}
}