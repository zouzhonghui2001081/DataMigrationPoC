using System;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    public class AcquisitionMethodWithValue : IEquatable<AcquisitionMethodWithValue>, ICloneable
    {
        public string Value { get; }
        public int Version { get; }
        public string VersionState { get; }
        public string Name { get; }

        private readonly bool _showVersionInfo;

        public AcquisitionMethodWithValue( string value,bool showVersion = false,int version = 0, string status = "")
        {
            Name = value;
            Version = version;
            VersionState = status;
            _showVersionInfo = showVersion;
            Value = _showVersionInfo ? GetVersionValue() : Name;
        }

        public bool Equals(AcquisitionMethodWithValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Version == other.Version && _showVersionInfo == other._showVersionInfo &&
                   string.Equals(VersionState, other.VersionState, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(Name, other.Name) &&
                   string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AcquisitionMethodWithValue)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (!string.IsNullOrEmpty(Value) ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Version.GetHashCode();
                hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(VersionState) ? VersionState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(Name) ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _showVersionInfo.GetHashCode();
                return hashCode;
            }
        }

        public object Clone()
        {
            var cloneAcquisitionMethodWithValue = (AcquisitionMethodWithValue)this.MemberwiseClone();
            return cloneAcquisitionMethodWithValue;
        }

        private string GetVersionValue()
        {
            return Version == 0 ?  Name :  ( string.IsNullOrEmpty(VersionState)
                ? $"{Name} Ver {Version}"
                : $"{Name} Ver {Version} ({VersionState})");
        }

        public override string ToString()
        {
            return Value;
        }

    }
}
