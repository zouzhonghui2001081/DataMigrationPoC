using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Sequence
{
    [Serializable]
    public class ReportGroup : IReportGroup, IEquatable<ReportGroup>
    {
        public ReportGroup() { }
        
        public ReportGroup(string groupName, string templateName)
        {
            GroupName = groupName;
            TemplateName = templateName;
        }

        public string GroupName { get; set; }

        public string TemplateName { get; set; }

        public bool IsGroupNameEditable { get; set; }

        public bool Equals(ReportGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(GroupName, other.GroupName) && string.Equals(TemplateName, other.TemplateName);                  
        }

        public int GetHashCode(object obj)
        {
            var hashCode = GroupName != null ? GroupName.GetHashCode() : 0;
            hashCode = (hashCode * 397) ^ (TemplateName != null ? TemplateName.GetHashCode() : 0);
            return hashCode;
        }
    }
}
