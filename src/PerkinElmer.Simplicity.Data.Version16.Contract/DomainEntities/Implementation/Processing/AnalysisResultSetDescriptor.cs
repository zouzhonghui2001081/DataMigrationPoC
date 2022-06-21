using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Processing
{
	class AnalysisResultSetDescriptor : IAnalysisResultSetDescriptor
	{
		public object Clone()
		{
			var cloned = new AnalysisResultSetDescriptor
			{
				Name = Name,
				Guid = Guid,
				CreatedDateUtc = CreatedDateUtc,
				CreatedByUser = (IUserInfo)CreatedByUser.Clone(),
				ModifiedDateUtc = ModifiedDateUtc,
				ModifiedByUser = (IUserInfo)ModifiedByUser.Clone(),
				OriginalDescriptor = (IOriginalAnalysisResultSetDescriptor)OriginalDescriptor.Clone(),
				OnlyOriginalExists = OnlyOriginalExists,
				Partial = Partial,
				ReviewApproveState = ReviewApproveState,
				IsCopy = IsCopy,
				DataIntegrated = DataIntegrated
			};

			return cloned;
		}

		public string Name { get; set; }
		public Guid Guid { get; set; }
		public DateTime CreatedDateUtc { get; set; }
		public IUserInfo CreatedByUser { get; set; }
		public DateTime ModifiedDateUtc { get; set; }
		public IUserInfo ModifiedByUser { get; set; }
		public IOriginalAnalysisResultSetDescriptor OriginalDescriptor { get; set; }
		public bool OnlyOriginalExists { get; set; }
		public bool Partial { get; set; }
		public ReviewApproveState ReviewApproveState { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedTimeStamp { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedTimeStamp { get; set; }
        public bool IsCopy { get; set; }
		public string DataIntegrated { get; set; }
        public DataSourceType DataSourceType { get; set; }

        public bool Equals(IAnalysisResultSetDescriptor other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Guid.Equals(other.Guid) &&
			       CreatedDateUtc.Equals(other.CreatedDateUtc) &&
			       Equals(CreatedByUser, other.CreatedByUser) && ModifiedDateUtc.Equals(other.ModifiedDateUtc) &&
			       Equals(ModifiedByUser, other.ModifiedByUser) &&
			       Equals(OriginalDescriptor, other.OriginalDescriptor) &&
			       Equals(OnlyOriginalExists, other.OnlyOriginalExists) && Equals(Partial, other.Partial) &&
			       Equals(ReviewApproveState, other.ReviewApproveState) &&
			       Equals(IsCopy, other.IsCopy)&&Equals(DataIntegrated,other.DataIntegrated);
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((IAnalysisResultSetDescriptor)obj);
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
				hashCode = (hashCode * 397) ^ (OriginalDescriptor != null ? OriginalDescriptor.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ OnlyOriginalExists.GetHashCode();
				hashCode = (hashCode * 397) ^ Partial.GetHashCode();
				hashCode = (hashCode * 397) ^ ReviewApproveState.GetHashCode();
				hashCode = (hashCode * 397) ^ IsCopy.GetHashCode();
				hashCode = (hashCode * 397) ^ DataIntegrated.GetHashCode();
				return hashCode;
			}
		}
	}
}
