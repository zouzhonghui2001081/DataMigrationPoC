using System;
using System.Runtime.Serialization;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition
{
    [DataContract]
    public struct Id : IEquatable<Id>
    {
        [DataMember]
        private readonly string _id;

        public Id(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            _id = id;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Id other)
        {
            return string.Equals(_id, other._id);
        }

        public override int GetHashCode()
        {
            return (_id != null ? _id.GetHashCode() : 0);
        }

        public static bool operator ==(Id left, Id right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Id left, Id right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return _id;
        }
    }
}