using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Acquisition.Devices;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
    internal class DeviceDriverItemDetailsJsonConverter: IJsonConverter<IDeviceDriverItemDetails>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ConfigurationKeyName = "Configuration";
        private const string DeviceTypeKeyName = "DeviceType";
        private const string IsDisplayDriverKeyName = "IsDisplayDriver";
        private const string IdKeyName = "Id";
        private const string NameKeyName = "Name";
        private DeviceDriverItemCompleteIdJsonConverter _deviceDriverItemCompleteIdJsonConverter = new DeviceDriverItemCompleteIdJsonConverter();

        public JObject ToJson(IDeviceDriverItemDetails instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ConfigurationKeyName, new JValue(instance.Configuration) },
                {NameKeyName, new JValue(instance.Name) },
                {DeviceTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.DeviceType, new StringEnumConverter())) },
                {IsDisplayDriverKeyName, new JValue(instance.IsDisplayDriver) },
                {IdKeyName, _deviceDriverItemCompleteIdJsonConverter.ToJson(instance.Id) },
            };
            return jObject;
        }

        public IDeviceDriverItemDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IDeviceDriverItemDetailsModifiable>();
            instance.SetConfiguration((string) jObject[ConfigurationKeyName]);
            DeviceType? type = null;
            if (jObject[DeviceTypeKeyName].ToString().ToLower() != "null")
                type = JsonConvert.DeserializeObject<DeviceType>((string)jObject[DeviceTypeKeyName]);
            instance.SetDeviceType(type, (bool)jObject[IsDisplayDriverKeyName]);
            if (jObject[IdKeyName].Type != JTokenType.Null)
            {
                var deviceDriverItemCompleteIdObject = JObject.Parse(jObject[IdKeyName].ToString());
                var deviceDriverItemCompleteIdInstance = _deviceDriverItemCompleteIdJsonConverter.FromJson(deviceDriverItemCompleteIdObject);
                instance.Set(deviceDriverItemCompleteIdInstance, (string)jObject[NameKeyName]);
            }
            return instance;
        }
    }
}
