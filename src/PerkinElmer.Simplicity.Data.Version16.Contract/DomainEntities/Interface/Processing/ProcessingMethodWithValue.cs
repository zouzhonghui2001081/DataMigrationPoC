using System;
using System.ComponentModel;
using System.Reflection;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing
{
    public class ProcessingMethodWithValue : IEquatable<ProcessingMethodWithValue> ,ICloneable
    {
        public ProcessingMethodType ProcessingMethodType { get; set; }
		public string GroupName { get; set; }
        public string Value { get; }
        public int Version { get; }
        public string VersionState { get; }
        public string Name { get; }
        private readonly bool _showVersionInfo;

        public ProcessingMethodWithValue(ProcessingMethodType processingMethodType, string value,bool showVersion = false, int version = 0, string status ="")
        {
	        ProcessingMethodType = processingMethodType;
	        GroupName = GetEnumDescription(processingMethodType);
            Name = value;
            _showVersionInfo = showVersion;
            Version = version;
            VersionState = status;
            Value = _showVersionInfo ? GetVersonValue() : Name;
           
        }

        private string GetEnumDescription(Enum value)
        {
	        FieldInfo fi = value.GetType().GetField(value.ToString());

	        DescriptionAttribute[] attributes =
		        (DescriptionAttribute[]) fi.GetCustomAttributes(
			        typeof(DescriptionAttribute),
			        false);

	        if (attributes?.Length > 0)
		        return attributes[0].Description;
	        else
		        return value.ToString();
        }

        public bool Equals(ProcessingMethodWithValue other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(ProcessingMethodType, other.ProcessingMethodType) && Version == other.Version &&
                   _showVersionInfo == other._showVersionInfo &&
                   string.Equals(VersionState, other.VersionState, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(Name, other.Name, StringComparison.CurrentCultureIgnoreCase) &&
                   string.Equals(Value, other.Value, StringComparison.CurrentCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProcessingMethodWithValue) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (!string.IsNullOrEmpty(Value) ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Version.GetHashCode() ;
                hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(VersionState) ? VersionState.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (!string.IsNullOrEmpty(Name) ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _showVersionInfo.GetHashCode();
                return hashCode;
            }
        }

        public object Clone()
        {
            var clonedProcessingMethodWithValue = (ProcessingMethodWithValue)this.MemberwiseClone();
            return clonedProcessingMethodWithValue;
        }

        private string GetVersonValue()
        {
            return Version == 0 ? Name : (string.IsNullOrEmpty(VersionState)
                ? $"{Name} Ver {Version}"
                : $"{Name} Ver {Version} ({VersionState})");
        }

        public override string ToString()
        {
            return  Value;
        }

    }
}
