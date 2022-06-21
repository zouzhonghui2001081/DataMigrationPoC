using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
    class DeviceModuleDetailsJsonConverter : IJsonConverter<IDeviceModuleDetails>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string SettingsUserInterfaceSupportedKeyName = "SettingsUserInterfaceSupported";
        private const string DeivceModuleKeyName = "DeviceModule";
        private const string SimulationKeyName = "Simulation";
        private const string CommunicationTestedSuccessfullyKeyName = "CommunicationTestedSuccessfully";
        private const string DeviceInformationKeyName = "DeviceInformation";

        public JObject ToJson(IDeviceModuleDetails instance)
        {
            if (instance == null)
                return null;

            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {DeivceModuleKeyName, JsonConverterRegistry.GetConverter<IDeviceModule>().ToJson(instance)},
                {DeviceInformationKeyName,JsonConverterRegistry.GetConverter<IDeviceInformation>().ToJson(instance.DeviceInformation) },
                {SettingsUserInterfaceSupportedKeyName, new JValue(instance.SettingsUserInterfaceSupported)},
                {SimulationKeyName, new JValue(instance.Simulation)},
                {CommunicationTestedSuccessfullyKeyName, new JValue(instance.CommunicationTestedSuccessfully)}
            };
            return jObject;
        }

        public IDeviceModuleDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var deviceModuleDetails = DomainFactory.Create<IDeviceModuleDetailsModifiable>();
            var deviceInformation = (jObject[DeviceInformationKeyName].Type == JTokenType.Null)
                ? null
                : JsonConverterRegistry.GetConverter<IDeviceInformation>().FromJson((JObject)jObject[DeviceInformationKeyName]);

            var deviceModule = (jObject[DeivceModuleKeyName].Type == JTokenType.Null)
                ? null
                : JsonConverterRegistry.GetConverter<IDeviceModule>().FromJson((JObject)jObject[DeivceModuleKeyName]);

            deviceModuleDetails.SetCommunicationTestedSuccessfully((bool)jObject[CommunicationTestedSuccessfullyKeyName], deviceInformation);
            deviceModuleDetails.SetSettingsUserInterfaceSupported((bool)jObject[SettingsUserInterfaceSupportedKeyName]);
            deviceModuleDetails.SetSimulation((bool)jObject[SimulationKeyName]);
            deviceModuleDetails.Set(deviceModule);

            return deviceModuleDetails;
        }
    }
}
