using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public class IntValueWithDeviceName : IEquatable<IntValueWithDeviceName>
    {
        public IntValueWithDeviceName(string deviceName, int value)
        {
            DeviceName = deviceName;
            Value = value;
        }

        public string DeviceName { get; }
        public int Value { get; set; }

        public bool Equals(IntValueWithDeviceName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DeviceName, other.DeviceName) && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((IntValueWithDeviceName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DeviceName != null ? DeviceName.GetHashCode() : 0) * 397) ^ Value;
            }
        }
	    public override string ToString()
	    {
		    return Value.ToString();
	    }
	}
}