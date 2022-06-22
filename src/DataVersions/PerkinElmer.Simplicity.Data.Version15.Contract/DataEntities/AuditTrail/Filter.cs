using System.Collections.Generic;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.AuditTrail
{
    public class RangeFilter : Filter
    {
        public RangeFilter()
        {
            FilterType = FilterType.Range;
        }
        public object FromValue { get; set; }
        public object ToValue { get; set; }
    }

    public class TermFilter : Filter
    {
        public TermFilter()
        {
            FilterType = FilterType.Term;
        }

        public IList<object> Values { get; set; }
    }

    public class QueryOperators
    {
        public const string Equal = "Equal";
        public const string NotEqual = "NotEqual";
    }

    public abstract class Filter
    {
        public FilterType FilterType { get; protected set; }
        public string ColumnName { get; set; }
        public string Operator { get; set; }
    }
}
