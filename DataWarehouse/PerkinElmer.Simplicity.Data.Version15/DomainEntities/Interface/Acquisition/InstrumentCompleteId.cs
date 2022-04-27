using System;
using System.Runtime.Serialization;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    [DataContract]
    public struct InstrumentCompleteId : IEquatable<InstrumentCompleteId>
    {
        [DataMember]
        private readonly Id _instrumentMasterId;
        [DataMember]
        private readonly Id _instrumentId;

        public InstrumentCompleteId(Id instrumentMasterId, Id instrumentId)
        {
            _instrumentMasterId = instrumentMasterId;
            _instrumentId = instrumentId;
        }

        public Id InstrumentMasterId => _instrumentMasterId;

        public Id InstrumentId => _instrumentId;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(InstrumentCompleteId other)
        {
            return InstrumentMasterId.Equals(other.InstrumentMasterId) && InstrumentId.Equals(other.InstrumentId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (InstrumentMasterId.GetHashCode() * 397) ^ InstrumentId.GetHashCode();
            }
        }

        public static bool operator ==(InstrumentCompleteId left, InstrumentCompleteId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(InstrumentCompleteId left, InstrumentCompleteId right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"{_instrumentMasterId.ToString()} - {_instrumentId.ToString()}";
        }
    }
}