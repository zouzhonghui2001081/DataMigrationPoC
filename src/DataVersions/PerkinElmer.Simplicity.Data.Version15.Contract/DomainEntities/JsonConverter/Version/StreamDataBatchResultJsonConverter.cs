using System.Text;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class StreamDataBatchResultJsonConverter : IJsonConverter<StreamDataBatchResult>
    {
        private const string BatchRunIdKeyName = "BatchRunId";
        private const string IdKeyName = "Id";
        private const string StreamIndexKeyName = "StreamIndex";
        private const string MetaDataKeyName = "MetaData";
        private const string MetaDataTypeKeyName = "MetaDataType";
        private const string YDataKeyName = "YData";
        private const string DeviceIdKeyName = "DeviceId";
        private const string LargeObjectOidKeyName = "LargeObjectOid";
        private const string UseLargeObjectStreamKeyName = "UseLargeObjectStream";
        private const string FirmwareVersionKeyName = "FirmwareVersion";
        private const string SerialNumberKeyName = "SerialNumber";
        private const string ModelNameKeyName = "ModelName";
        private const string UniqueIdentifierKeyName = "UniqueIdentifier";
        private const string InterfaceAddressKeyName = "InterfaceAddress";

        public JObject ToJson(StreamDataBatchResult instance)
        {
            if (instance == null) return null;
            var jObject = new JObject
            {
                {BatchRunIdKeyName, instance.BatchRunId},
                {IdKeyName, instance.Id},
                {StreamIndexKeyName, instance.StreamIndex},
                {MetaDataKeyName, instance.MetaData},
                {MetaDataTypeKeyName, instance.MetaDataType},
                {YDataKeyName, instance.YData},
                {DeviceIdKeyName, instance.DeviceId},
                {LargeObjectOidKeyName, instance.LargeObjectOid},
                {UseLargeObjectStreamKeyName, instance.UseLargeObjectStream},
                {FirmwareVersionKeyName, instance.FirmwareVersion},
                {SerialNumberKeyName, instance.SerialNumber},
                {ModelNameKeyName, instance.ModelName},
                {UniqueIdentifierKeyName, instance.UniqueIdentifier},
                {InterfaceAddressKeyName, instance.InterfaceAddress},
            };
            return jObject;
        }

        public StreamDataBatchResult FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            return new StreamDataBatchResult
            {
                BatchRunId = (long) jObject[BatchRunIdKeyName],
                Id = (long) jObject[IdKeyName],
                StreamIndex = (int) jObject[StreamIndexKeyName],
                MetaData = (string) jObject[MetaDataKeyName],
                MetaDataType = (string) jObject[MetaDataTypeKeyName],
                YData = Encoding.ASCII.GetBytes((string) jObject[YDataKeyName]),
                DeviceId = (string) jObject[DeviceIdKeyName],
                LargeObjectOid = (long?) jObject[LargeObjectOidKeyName],
                UseLargeObjectStream = (bool) jObject[UseLargeObjectStreamKeyName],
                FirmwareVersion = (string) jObject[FirmwareVersionKeyName],
                SerialNumber = (string) jObject[SerialNumberKeyName],
                ModelName = (string) jObject[ModelNameKeyName],
                UniqueIdentifier = (string) jObject[UniqueIdentifierKeyName],
                InterfaceAddress = (string) jObject[InterfaceAddressKeyName],
            };
        }
    }
}
