
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchResultDeviceModuleDetailsJsonConverter : IJsonConverter<BatchResultDeviceModuleDetails>
    {
        private const string BatchResultSetIdKeyName = "BatchResultSetId";
        private const string IdKeyName = "Id";
        private const string NameKeyName = "Name";
        private const string IsDisplayDriverKeyName = "IsDisplayDriver";
        private const string DeviceTypeKeyName = "DeviceType";
        private const string DeviceModuleIdKeyName = "DeviceModuleId";
        private const string InstrumentMasterIdKeyName = "InstrumentMasterId";
        private const string InstrumentIdKeyName = "InstrumentId";
        private const string DeviceDriverItemIdKeyName = "DeviceDriverItemId";
        private const string SettingsUserInterfaceSupportedKeyName = "SettingsUserInterfaceSupported";
        private const string SimulationIdKeyName = "Simulation";
        private const string CommunicationTestedSuccessfullyKeyName = "CommunicationTestedSuccessfully";
        private const string FirmwareVersionKeyName = "FirmwareVersion";
        private const string SerialNumberKeyName = "SerialNumber";
        private const string ModelNameKeyName = "ModelName";
        private const string UniqueIdentifierKeyName = "UniqueIdentifier";
        private const string InterfaceAddressKeyName = "InterfaceAddress";

        public JObject ToJson(BatchResultDeviceModuleDetails instance)
        {
            if (instance == null) return null;

            return new JObject
            {
                {BatchResultSetIdKeyName, instance.BatchResultSetId},
                {IdKeyName, instance.Id},
                {NameKeyName, instance.Name},
                {IsDisplayDriverKeyName, instance.IsDisplayDriver},
                {DeviceTypeKeyName, instance.DeviceType},
                {DeviceModuleIdKeyName, instance.DeviceModuleId},
                {InstrumentMasterIdKeyName, instance.InstrumentMasterId},
                {InstrumentIdKeyName, instance.InstrumentId},
                {DeviceDriverItemIdKeyName, instance.DeviceDriverItemId},
                {SettingsUserInterfaceSupportedKeyName, instance.SettingsUserInterfaceSupported},
                {SimulationIdKeyName, instance.Simulation},
                {CommunicationTestedSuccessfullyKeyName, instance.CommunicationTestedSuccessfully},
                {FirmwareVersionKeyName, instance.FirmwareVersion},
                {SerialNumberKeyName, instance.SerialNumber},
                {ModelNameKeyName, instance.ModelName},
                {UniqueIdentifierKeyName, instance.UniqueIdentifier},
                {InterfaceAddressKeyName, instance.InterfaceAddress},
            };

        }

        public BatchResultDeviceModuleDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            return new BatchResultDeviceModuleDetails
            {
                BatchResultSetId = (long) jObject[BatchResultSetIdKeyName],
                Id = (long) jObject[IdKeyName],
                Name = (string) jObject[NameKeyName],
                IsDisplayDriver = (bool) jObject[IsDisplayDriverKeyName],
                DeviceType = (short) jObject[DeviceTypeKeyName],
                DeviceModuleId = (string) jObject[DeviceModuleIdKeyName],
                InstrumentMasterId = (string) jObject[InstrumentMasterIdKeyName],
                InstrumentId = (string) jObject[InstrumentIdKeyName],
                DeviceDriverItemId = (string) jObject[DeviceDriverItemIdKeyName],
                SettingsUserInterfaceSupported = (bool) jObject[SettingsUserInterfaceSupportedKeyName],
                Simulation = (bool) jObject[SimulationIdKeyName],
                CommunicationTestedSuccessfully = (bool) jObject[CommunicationTestedSuccessfullyKeyName],
                FirmwareVersion = (string) jObject[FirmwareVersionKeyName],
                SerialNumber = (string) jObject[SerialNumberKeyName],
                ModelName = (string) jObject[ModelNameKeyName],
                UniqueIdentifier = (string) jObject[UniqueIdentifierKeyName],
                InterfaceAddress = (string) jObject[InterfaceAddressKeyName],
            };
        }
    }
}
