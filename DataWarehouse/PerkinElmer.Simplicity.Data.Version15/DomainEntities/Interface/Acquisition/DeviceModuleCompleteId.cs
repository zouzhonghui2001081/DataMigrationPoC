using System;
using System.Runtime.Serialization;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    [DataContract]
    public struct DeviceModuleCompleteId : IEquatable<DeviceModuleCompleteId>
    {
        [DataMember]
        private readonly DeviceDriverItemCompleteId _deviceDriverItemCompleteId;
        [DataMember]
        private readonly Id _deviceModuleId;
        [DataMember]
        private readonly bool _isDisplayDriver;

        public DeviceModuleCompleteId(DeviceDriverItemCompleteId deviceDriverItemCompleteId, Id deviceModuleId, bool isDisplayDriver = false)
        {
            _deviceDriverItemCompleteId = deviceDriverItemCompleteId;
            _deviceModuleId = deviceModuleId;
            _isDisplayDriver = isDisplayDriver;
        }

        public DeviceDriverItemCompleteId DeviceDriverItemCompleteId => _deviceDriverItemCompleteId;

        public Id DeviceModuleId => _deviceModuleId;

        public bool IsDisplayDriver => _isDisplayDriver;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(DeviceModuleCompleteId other)
        {
            return DeviceDriverItemCompleteId.Equals(other.DeviceDriverItemCompleteId) && DeviceModuleId.Equals(other.DeviceModuleId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (DeviceDriverItemCompleteId.GetHashCode() * 397) ^ DeviceModuleId.GetHashCode();
            }
        }

        public static bool operator ==(DeviceModuleCompleteId left, DeviceModuleCompleteId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceModuleCompleteId left, DeviceModuleCompleteId right)
        {
            return !left.Equals(right);
        }
    }
}