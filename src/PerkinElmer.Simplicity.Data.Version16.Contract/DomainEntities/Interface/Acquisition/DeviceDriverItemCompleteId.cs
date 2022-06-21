using System;
using System.Runtime.Serialization;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition
{
    [DataContract]
    public struct DeviceDriverItemCompleteId : IEquatable<DeviceDriverItemCompleteId>
    {
        [DataMember]
        private readonly InstrumentCompleteId _instrumentCompleteId;
        [DataMember]
        private readonly Id _deviceDriverItemId;

        public DeviceDriverItemCompleteId(InstrumentCompleteId instrumentCompleteId, Id deviceDriverItemId)
        {
            _instrumentCompleteId = instrumentCompleteId;
            _deviceDriverItemId = deviceDriverItemId;
        }

        public InstrumentCompleteId InstrumentCompleteId => _instrumentCompleteId;

        public Id DeviceDriverItemId => _deviceDriverItemId;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(DeviceDriverItemCompleteId other)
        {
            return InstrumentCompleteId.Equals(other.InstrumentCompleteId) && DeviceDriverItemId.Equals(other.DeviceDriverItemId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (InstrumentCompleteId.GetHashCode() * 397) ^ DeviceDriverItemId.GetHashCode();
            }
        }

        public static bool operator ==(DeviceDriverItemCompleteId left, DeviceDriverItemCompleteId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DeviceDriverItemCompleteId left, DeviceDriverItemCompleteId right)
        {
            return !left.Equals(right);
        }
    }
}