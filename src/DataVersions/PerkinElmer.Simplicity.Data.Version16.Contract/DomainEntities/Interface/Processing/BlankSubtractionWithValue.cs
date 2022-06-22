using System;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public class BlankSubtractionWithValue : IEquatable<BlankSubtractionWithValue>
    {
        public BaselineCorrection Name { get; set; }
        public string Value { get; set; }
        public string BatchRunName { get; set; }
        public string BatchResultSetName { get; set; }
        public Guid BatchRunGuid { get; set; }
        public Guid BatchResultSetGuid { get; set; }
        public bool IsExternal { get; set; }

        public BlankSubtractionWithValue(BaselineCorrection name, string value, Guid batchRunGuid)
        {
            Name = name;
            IsExternal = (name == BaselineCorrection.Other);
            Value = value;
            BatchRunGuid = batchRunGuid;
        }



        public bool Equals(BlankSubtractionWithValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && BatchRunGuid.Equals(other.BatchRunGuid);
            //&&
            //       string.Equals(Value, other.Value, StringComparison.CurrentCultureIgnoreCase) && 
            //	string.Equals(BatchRunGuid,other.BatchRunGuid);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BlankSubtractionWithValue)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (!string.IsNullOrEmpty(Name.ToString()) ? Name.ToString().GetHashCode() : 0) * 397;
                hashCode += BatchRunGuid.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
