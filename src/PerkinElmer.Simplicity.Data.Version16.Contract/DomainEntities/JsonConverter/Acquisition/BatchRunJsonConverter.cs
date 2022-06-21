using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Acquisition
{
    internal class BatchRunJsonConverter : IJsonConverter<IBatchRun>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InfoKeyName = "Info";
        private const string AcquisitionMethodGuidKeyName = "AcquisitionMethodGuid";
        private const string ProcessingMethodGuidKeyName = "ProcessingMethodGuid";
        private const string CalibrationMethodGuidKeyName = "CalibrationMethodGuid";
        private const string ChannelDataKeyName = "ChannelData";

        public IBatchRun FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var batchRun = DomainFactory.Create<IBatchRun>();
            batchRun.Info = jObject[InfoKeyName].Type == JTokenType.Null ? null :
            JsonConverterRegistry.GetConverter<IBatchRunInfo>().FromJson((JObject)jObject[InfoKeyName]);
            batchRun.AcquisitionMethodGuid = (Guid)jObject[AcquisitionMethodGuidKeyName];
            batchRun.ProcessingMethodGuid = (Guid)jObject[ProcessingMethodGuidKeyName];
            batchRun.CalibrationMethodGuid = (Guid)jObject[CalibrationMethodGuidKeyName];
            batchRun.BatchRunChannelGuids = JsonConverterHelper.GetArrayPropertyFromJson<string>(jObject, ChannelDataKeyName)?.Select(s => new Guid(s)).ToArray();
            return batchRun;
        }

        public JObject ToJson(IBatchRun instance)
        {
			if (instance == null) return null;            
			
			var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InfoKeyName, JsonConverterRegistry.GetConverter<IBatchRunInfo>().ToJson(instance.Info)},
                {AcquisitionMethodGuidKeyName, new JValue(instance.AcquisitionMethodGuid)},
                {ProcessingMethodGuidKeyName, new JValue(instance.ProcessingMethodGuid)},
                {CalibrationMethodGuidKeyName, new JValue(instance.CalibrationMethodGuid)},
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<string>(jObject,
                instance.BatchRunChannelGuids?.Select(g => g.ToString()).ToArray(), ChannelDataKeyName);
            return jObject;
        }
    }
}
