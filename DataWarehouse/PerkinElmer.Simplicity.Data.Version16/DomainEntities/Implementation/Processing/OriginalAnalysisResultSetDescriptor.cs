using System;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing
{
	internal class OriginalAnalysisResultSetDescriptor : IOriginalAnalysisResultSetDescriptor
	{
		public object Clone()
		{
			var cloned = new OriginalAnalysisResultSetDescriptor
			{
				Name = Name,
				Guid = Guid,
				CreatedDateUtc = CreatedDateUtc,
				CreatedByUser = (IUserInfo)CreatedByUser.Clone(),
				ModifiedDateUtc = ModifiedDateUtc,
				ModifiedByUser = (IUserInfo)ModifiedByUser.Clone(),
				SystemName = SystemName,
				Imported = Imported,
				AutoProcessed = AutoProcessed
			};

			return cloned;
		}

		public string Name { get; set; }
		public Guid Guid { get; set; }
		public DateTime CreatedDateUtc { get; set; }
		public IUserInfo CreatedByUser { get; set; }
		public DateTime ModifiedDateUtc { get; set; }
		public IUserInfo ModifiedByUser { get; set; }
		public bool Imported { get; set; }
		public string SystemName { get; set; }
		public bool AutoProcessed { get; set; }
        public string ReviewedBy { get; set; }
        public DateTime? ReviewedTimeStamp { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedTimeStamp { get; set; }
        public bool Equals(IOriginalAnalysisResultSetDescriptor other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return string.Equals(Name, other.Name) && Guid.Equals(other.Guid) && CreatedDateUtc.Equals(other.CreatedDateUtc) &&
			       Equals(CreatedByUser, other.CreatedByUser) && ModifiedDateUtc.Equals(other.ModifiedDateUtc) &&
			       Equals(ModifiedByUser, other.ModifiedByUser) && Equals(SystemName, other.SystemName) &&
			       Equals(Imported, other.Imported) && Equals(AutoProcessed, other.AutoProcessed);
		}
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((IOriginalAnalysisResultSetDescriptor)obj);
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
				hashCode = (hashCode * 397) ^ (SystemName != null ? SystemName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ Imported.GetHashCode();
				hashCode = (hashCode * 397) ^ AutoProcessed.GetHashCode();
				return hashCode;
			}
		}
	}
}
