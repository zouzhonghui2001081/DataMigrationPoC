using System;
using System.Text;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
	internal class StreamDataJsonConverter : IJsonConverter<IStreamData>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MetaDataKeyName = "MetaData";
        private const string MetaDataTypeKeyName = "MetaDataType";
        private const string StreamIndexKeyName = "StreamIndex";
        private const string DataKeyName = "Data";
        private const string DeviceDriverIdKeyName = "DeviceDriverId";
        private const string UseLargeObjectStreamKeyName = "UseLargeObjectStream";
        private const string DeviceInformationKeyName = "DeviceInformation";

		public IStreamData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var streamData = DomainFactory.Create<IStreamData>();
            streamData.MetaData = (string)jObject[MetaDataKeyName];
            streamData.MetaDataType = (string)jObject[MetaDataTypeKeyName];
            streamData.StreamIndex = (int)jObject[StreamIndexKeyName]; ;
            streamData.DeviceDriverId = (string)jObject[DeviceDriverIdKeyName];
            streamData.UseLargeObjectStream = (bool)jObject[UseLargeObjectStreamKeyName];
            streamData.Data = ((string)jObject[DataKeyName] == null)? null:
                Encoding.ASCII.GetBytes((string)jObject[DataKeyName]);
            streamData.DeviceInformation = (jObject[DeviceInformationKeyName].Type == JTokenType.Null)
                ? null
                : JsonConverterRegistry.GetConverter<IDeviceInformation>()
                    .FromJson((JObject)jObject[DeviceInformationKeyName]);

            return streamData;
        }

        public JObject ToJson(IStreamData instance)
        {
            if (instance == null)
                return null;
            string content = (instance.Data == null) ? null:
                Encoding.ASCII.GetString(instance.Data, 0, instance.Data.Length);
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MetaDataKeyName, new JValue(instance.MetaData)},
                {MetaDataTypeKeyName, new JValue(instance.MetaDataType)},
                {StreamIndexKeyName, new JValue(instance.StreamIndex)},
                {DeviceDriverIdKeyName, new JValue(instance.DeviceDriverId)},
                {UseLargeObjectStreamKeyName, new JValue(instance.UseLargeObjectStream)},
                {DataKeyName, new JValue(content)},
                {DeviceInformationKeyName, JsonConverterRegistry.GetConverter<IDeviceInformation>().ToJson(instance.DeviceInformation)},
            };

			return jObject;
        }
    }
}
