using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Acquisition
{
	internal class StreamDataInfoJsonConverter : IJsonConverter<IStreamDataInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MetaDataKeyName = "MetaData";
        private const string MetaDataTypeKeyName = "MetaDataType";
        private const string StreamIndexKeyName = "StreamIndex";
        private const string DeviceDriverIdKeyName = "DeviceDriverId";
        private const string UseLargeObjectStreamKeyName = "UseLargeObjectStream";
        private const string DeviceInformationKeyName = "DeviceInformation";

		public IStreamDataInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var streamDataInfo = DomainFactory.Create<IStreamDataInfo>();
            streamDataInfo.MetaData = (string)jObject[MetaDataKeyName];
            streamDataInfo.MetaDataType = (string)jObject[MetaDataTypeKeyName];
            streamDataInfo.StreamIndex = (int)jObject[StreamIndexKeyName]; ;
            streamDataInfo.DeviceDriverId = (string)jObject[DeviceDriverIdKeyName];
            streamDataInfo.UseLargeObjectStream = (bool)jObject[UseLargeObjectStreamKeyName];
            streamDataInfo.DeviceInformation = (jObject[DeviceInformationKeyName].Type == JTokenType.Null)
                ? null
                : JsonConverterRegistry.GetConverter<IDeviceInformation>()
                    .FromJson((JObject) jObject[DeviceInformationKeyName]);

            return streamDataInfo;
        }

        public JObject ToJson(IStreamDataInfo instance)
        {
            return instance == null
                ? null
                : new JObject
                {
                    {VersionKeyName, new JValue(CurrentVersion)},
                    {MetaDataKeyName, new JValue(instance.MetaData)},
                    {MetaDataTypeKeyName, new JValue(instance.MetaDataType)},
                    {StreamIndexKeyName, new JValue(instance.StreamIndex)},
                    {DeviceDriverIdKeyName, new JValue(instance.DeviceDriverId)},
                    {UseLargeObjectStreamKeyName, new JValue(instance.UseLargeObjectStream)},
                    {DeviceInformationKeyName, JsonConverterRegistry.GetConverter<IDeviceInformation>().ToJson(instance.DeviceInformation)},
                };
        }
    }
}
