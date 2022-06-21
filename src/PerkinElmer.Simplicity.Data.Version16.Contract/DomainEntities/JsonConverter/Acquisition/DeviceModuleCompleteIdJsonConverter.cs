using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Acquisition
{
    public class DeviceModuleCompleteIdJsonConverter : IJsonConverter<DeviceModuleCompleteId>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string DeviceModuleIdKeyName = "DeviceModuleId";
        private const string IsDisplayDriverKeyName = "IsDisplayDriver";
        private const string DeviceDriverItemCompleteIdKeyName = "DeviceDriverItemCompleteId";
        private DeviceDriverItemCompleteIdJsonConverter _deviceDriverItemCompleteIdJsonConverter;
        private IdJsonConverter _idJsonConverter;

        public DeviceModuleCompleteIdJsonConverter()
        {
            _deviceDriverItemCompleteIdJsonConverter = new DeviceDriverItemCompleteIdJsonConverter();
            _idJsonConverter = new IdJsonConverter();
        }
        public JObject ToJson(DeviceModuleCompleteId instance)
        {
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {DeviceModuleIdKeyName, _idJsonConverter.ToJson(instance.DeviceModuleId)},
                {IsDisplayDriverKeyName, new JValue(instance.IsDisplayDriver)},
                {DeviceDriverItemCompleteIdKeyName, _deviceDriverItemCompleteIdJsonConverter.ToJson(instance.DeviceDriverItemCompleteId) },
            };
            return jObject;
        }

        public DeviceModuleCompleteId FromJson(JObject jObject)
        {
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            
            var deviceModuleIdObject = JObject.Parse(jObject[DeviceModuleIdKeyName].ToString());
            var deviceModuleId = _idJsonConverter.FromJson(deviceModuleIdObject);
            var deviceDriverItemCompleteIdJObject = JObject.Parse(jObject[DeviceDriverItemCompleteIdKeyName].ToString());
            var deviceDriverItemCompleteId = _deviceDriverItemCompleteIdJsonConverter.FromJson(deviceDriverItemCompleteIdJObject);
            var instance = new DeviceModuleCompleteId(deviceDriverItemCompleteId, deviceModuleId, (bool)jObject[IsDisplayDriverKeyName]);
            return instance;
        }
    }
}
