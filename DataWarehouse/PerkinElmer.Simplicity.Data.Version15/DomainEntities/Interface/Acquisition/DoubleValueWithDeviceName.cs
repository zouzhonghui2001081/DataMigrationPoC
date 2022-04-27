using System;
using System.Globalization;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    public class DoubleValueWithDeviceName : IEquatable<DoubleValueWithDeviceName>
    {
        public DoubleValueWithDeviceName(string deviceName, double value)
        {
            DeviceName = deviceName;
            Value = value;
        }

        public string DeviceName { get; }
        public double Value { get; set; }


        public bool Equals(DoubleValueWithDeviceName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(DeviceName, other.DeviceName) && Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DoubleValueWithDeviceName) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DeviceName != null ? DeviceName.GetHashCode() : 0) * 397) ^ Value.GetHashCode();
            }
        }
		public override string ToString()
		{
			return Value.ToString(CultureInfo.InvariantCulture);
		}
	}
}