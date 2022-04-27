using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
	internal class DeviceMethodJsonConverter : IJsonConverter<IDeviceMethod>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ContentKeyName = "Content";
        private const string DeviceDriverItemDetailsKeyName = "DeviceDriverItemDetails";
        private const string DeviceModuleDetailsKeyName = "DeviceModuleDetails";

        public IDeviceMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var deviceMethod = DomainFactory.Create<IDeviceMethod>();
            deviceMethod.Content = (string)jObject[ContentKeyName];
            deviceMethod.DeviceDriverItemDetails = (jObject[DeviceDriverItemDetailsKeyName].Type == JTokenType.Null) ? null :
                JsonConverterRegistry.GetConverter<IDeviceDriverItemDetails>()
                    .FromJson((JObject)jObject[DeviceDriverItemDetailsKeyName]);
            deviceMethod.DeviceModuleDetails = (jObject[DeviceModuleDetailsKeyName]==null || jObject[DeviceModuleDetailsKeyName].Type == JTokenType.Null) ? null :
                JsonConverterHelper.GetArrayPropertyFromJson<IDeviceModuleDetails>(jObject, DeviceModuleDetailsKeyName);

            return deviceMethod;
        }

        public JObject ToJson(IDeviceMethod instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ContentKeyName, new JValue(instance.Content)},
                {DeviceDriverItemDetailsKeyName, JsonConverterRegistry.GetConverter<IDeviceDriverItemDetails>().ToJson(instance.DeviceDriverItemDetails)},                
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<IDeviceModuleDetails>(jObject, instance.DeviceModuleDetails, DeviceModuleDetailsKeyName);
            return jObject;
        }
    }
}
