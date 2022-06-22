using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
    public class DeviceDriverItemCompleteIdJsonConverter : IJsonConverter<DeviceDriverItemCompleteId>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InstrumentCompleteIdKeyName = "InstrumentCompleteId";
        private const string DeviceDriverItemIdKeyName = "DeviceDriverItemId";
        private InstrumentCompleteIdJsonConverter _instrumentCompleteIdJsonConverter;
        private IdJsonConverter _idJsonConverter;

        public DeviceDriverItemCompleteIdJsonConverter()
        {
            _instrumentCompleteIdJsonConverter = new InstrumentCompleteIdJsonConverter();
            _idJsonConverter = new IdJsonConverter();
        }
        public JObject ToJson(DeviceDriverItemCompleteId instance)
        {
            
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InstrumentCompleteIdKeyName, _instrumentCompleteIdJsonConverter.ToJson(instance.InstrumentCompleteId).ToString()},
                {DeviceDriverItemIdKeyName, _idJsonConverter.ToJson(instance.DeviceDriverItemId).ToString()},
            };
            return jObject;
        }

        public DeviceDriverItemCompleteId FromJson(JObject jObject)
        {
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instrumentCompleteIdJObject = JObject.Parse((string)jObject[InstrumentCompleteIdKeyName]);
            var instrumentCompleteId =  _instrumentCompleteIdJsonConverter.FromJson(instrumentCompleteIdJObject);
            var deviceDriverItemIdJObject = JObject.Parse((string)jObject[DeviceDriverItemIdKeyName]);
            var deviceDriverItemId =  _idJsonConverter.FromJson(deviceDriverItemIdJObject);
            var instance = new DeviceDriverItemCompleteId(instrumentCompleteId,deviceDriverItemId ); 
            return instance;
        }
    }
}
