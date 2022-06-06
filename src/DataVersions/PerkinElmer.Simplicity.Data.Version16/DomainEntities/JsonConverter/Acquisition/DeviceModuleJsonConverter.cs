using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
    public class DeviceModuleJsonConverter : IJsonConverter<IDeviceModule>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string IdKeyName = "Id";
        private const string NameKeyName = "Name";
        private const string DeviceTypeKeyName = "DeviceType";
        
        public JObject ToJson(IDeviceModule instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {IdKeyName, JsonConverterRegistry.GetConverter<DeviceModuleCompleteId>().ToJson(instance.Id)},
                {NameKeyName, new JValue(instance.Name)},
                {DeviceTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.DeviceType, new StringEnumConverter()))},
            };
            return jObject;
        }

        public IDeviceModule FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IDeviceModuleModifiable>();
            var completeId = JsonConverterRegistry.GetConverter<DeviceModuleCompleteId>().FromJson((JObject)jObject[IdKeyName]);
            var deviceType = JsonConvert.DeserializeObject<DeviceType>((string)jObject[DeviceTypeKeyName]);
            var name = (string)jObject[NameKeyName];
            instance.Set(completeId,name, deviceType );
            return instance;
        }
    }
}
