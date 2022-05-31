using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{

    class DeviceInformationJsonConverter : IJsonConverter<IDeviceInformation>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string FirmwareVersionKeyName = "FirmwareVersion";
        private const string SerialNumberKeyName = "SerialNumber";
        private const string ModelNamenKeyName = "ModelName";
        private const string MacAddressKeyName = "MacAddress";
        private const string InterfaceAddressKeyName = "InterfaceAddress";

        public JObject ToJson(IDeviceInformation instance)
        {
            if (instance == null)
                return null;


            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {FirmwareVersionKeyName, new JValue(instance.FirmwareVersion)},
                {SerialNumberKeyName, new JValue(instance.SerialNumber)},
                {ModelNamenKeyName, new JValue(instance.ModelName)},
                {MacAddressKeyName, new JValue(instance.UniqueIdentifier)},
                {InterfaceAddressKeyName, new JValue(instance.InterfaceAddress)}
            };
            return jObject;
        }

        public IDeviceInformation FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var deviceInformation = (DeviceInformation)DomainFactory.Create<IDeviceInformation>();
            deviceInformation.FirmwareVersion = (string)jObject[FirmwareVersionKeyName];
            deviceInformation.SerialNumber = (string)jObject[SerialNumberKeyName];
            deviceInformation.ModelName = (string)jObject[ModelNamenKeyName];
            deviceInformation.UniqueIdentifier = (string)jObject[MacAddressKeyName];
            deviceInformation.InterfaceAddress = (string)jObject[InterfaceAddressKeyName];

            return deviceInformation;
        }
    }
}
