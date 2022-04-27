using System;

namespace PerkinElmer.Simplicity.Data.Version16.DataEntities.AuditTrail
{
    public class AuditTrailLogEntry
    {
        public long Id { get; set; }

        public Guid UniqueId { get; set; }

        public DateTime LogTime { get; set; }

        public string ScopeType { get; set; }

        public string RecordType { get; set; }

        public string EntityId { get; set; }

        public string EntityType { get; set; }

        public string ActionTypeId { get; set; }

        public string ActionDescription { get; set; }

	    public string ActionType { get; set; }

        public string ItemId { get; set; }

        public string ItemName { get; set; }

        public string ItemType { get; set; }

        public string ItemVersionId { get; set; }

        public string UserId { get; set; }

        public string UserLogin { get; set; }

        public string UserFullName { get; set; }

        public string UserRoleId { get; set; }

        public string UserRole { get; set; }

        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string WorkstationId { get; set; }

        public string WorkstationName { get; set; }

        public string InstrumentId { get; set; }

        public string InstrumentName { get; set; }

		public string Justification { get; set; }

		public DateTime? JustificationTimestamp { get; set; }

        public string Comment { get; set; }

        public long FullCount { get; set; }

        public EntityVersionLogEntry VersionDiffEntry { get; set; }
    }
}
