using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class ProcessingMethodInfo : IProcessingMethodInfo
	{
		public string Name { get; set; } = "";

        public int? VersionNumber { get; set; }

		public Guid Guid { get; set; } = Guid.NewGuid();

		public DateTime CreatedDateUtc { get; set; }

		public IUserInfo CreatedByUser { get; set; }

		public DateTime ModifiedDateUtc { get; set; }

		public IUserInfo ModifiedByUser { get; set; }
		public string Description { get; set; }
	    public bool IsDefault { get; set; }
        public ReviewApproveState ReviewApproveState { get; set; }

        public object Clone()
		{
			var cloned = new ProcessingMethodInfo
			{
				Name = Name,
				Guid = Guid,
                VersionNumber = VersionNumber,
                Description = Description,
                IsDefault = IsDefault,
				CreatedDateUtc = CreatedDateUtc,
				CreatedByUser = (IUserInfo) CreatedByUser?.Clone(),
				ModifiedDateUtc = ModifiedDateUtc,
				ModifiedByUser = (IUserInfo) ModifiedByUser?.Clone(),
                ReviewApproveState = ReviewApproveState.NeverSubmitted,
            };

            return cloned;
		}

		public bool Equals(IProcessingMethodInfo other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
		    return string.Equals(Name, other.Name) && Guid.Equals(other.Guid) &&
                   VersionNumber.Equals(other.VersionNumber) &&
		           CreatedDateUtc.Equals(other.CreatedDateUtc) &&
		           Equals(CreatedByUser, other.CreatedByUser) && ModifiedDateUtc.Equals(other.ModifiedDateUtc) &&
		           Equals(ModifiedByUser, other.ModifiedByUser) &&
		           Equals(IsDefault, other.IsDefault) &&
                   Equals(ReviewApproveState, other.ReviewApproveState);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((IProcessingMethodInfo) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Name != null ? Name.GetHashCode() : 0;
				hashCode = (hashCode * 397) ^ Guid.GetHashCode();
				hashCode = (hashCode * 397) ^ CreatedDateUtc.GetHashCode();
				hashCode = (hashCode * 397) ^ (CreatedByUser != null ? CreatedByUser.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ ModifiedDateUtc.GetHashCode();
				hashCode = (hashCode * 397) ^ (ModifiedByUser != null ? ModifiedByUser.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ ReviewApproveState.GetHashCode();
			    hashCode = (hashCode * 397) ^ ( VersionNumber != null ? VersionNumber.GetHashCode() : 0);
                return hashCode;
			}
		}

		
	}
}