using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Acquisition
{
    internal class DoubleValueWithDeviceNameJsonConverter: IJsonConverter<DoubleValueWithDeviceName>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string DeviceNameKeyName = "DeviceName";
        private const string ValueKeyName = "Value";

        public DoubleValueWithDeviceName FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var doubleValueWithDeviceName = new DoubleValueWithDeviceName((string)jObject[DeviceNameKeyName],
                (double)jObject[ValueKeyName]);
            return doubleValueWithDeviceName;
        }

        public JObject ToJson(DoubleValueWithDeviceName instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {DeviceNameKeyName, new JValue(instance.DeviceName)},
                {ValueKeyName, new JValue(instance.Value)},                
            };
            return jObject;
        }

    }
}
