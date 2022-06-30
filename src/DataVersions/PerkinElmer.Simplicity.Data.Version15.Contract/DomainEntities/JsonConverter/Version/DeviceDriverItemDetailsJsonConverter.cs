
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class DeviceDriverItemDetailsJsonConverter : IJsonConverter<DeviceDriverItemDetails>
    {
        private const string IdKeyName = "Id";
        private const string BatchResultSetIdKeyName = "BatchResultSetId";
        private const string ConfigurationKeyName = "Configuration";
        private const string DeviceTypeKeyName = "DeviceType";
        private const string NameKeyName = "Name";
        private const string IsDisplayDriverKeyName = "IsDisplayDriver";
        private const string InstrumentMasterIdKeyName = "InstrumentMasterId";
        private const string InstrumentIdKeyName = "InstrumentId";
        private const string DeviceDriverItemIdKeyName = "DeviceDriverItemId";

        public JObject ToJson(DeviceDriverItemDetails instance)
        {
            if (instance == null) return null;

            return new JObject
            {
                {IdKeyName, instance.Id},
                {BatchResultSetIdKeyName, instance.BatchResultSetId},
                {ConfigurationKeyName, instance.Configuration},
                {DeviceTypeKeyName, instance.DeviceType},
                {NameKeyName, instance.Name},
                {IsDisplayDriverKeyName, instance.IsDisplayDriver},
                {InstrumentMasterIdKeyName, instance.InstrumentMasterId},
                {InstrumentIdKeyName, instance.InstrumentId},
                {DeviceDriverItemIdKeyName, instance.DeviceDriverItemId},
            };

        }

        public DeviceDriverItemDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            return new DeviceDriverItemDetails
            {
                Id = (long)jObject[IdKeyName],
                BatchResultSetId = (long)jObject[BatchResultSetIdKeyName],
                Configuration = (string)jObject[ConfigurationKeyName],
                DeviceType = (short?)jObject[DeviceTypeKeyName],
                Name = (string)jObject[NameKeyName],
                IsDisplayDriver = (bool)jObject[IsDisplayDriverKeyName],
                InstrumentMasterId = (string)jObject[InstrumentMasterIdKeyName],
                InstrumentId = (string)jObject[InstrumentIdKeyName],
                DeviceDriverItemId = (string)jObject[DeviceDriverItemIdKeyName],
              
            };
        }
    }
}
