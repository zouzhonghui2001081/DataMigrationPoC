using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
	internal class BatchRunWithRawDataJsonConverter : IJsonConverter<IBatchRunWithRawData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string InfoKeyName = "Info";
		private const string AcquisitionMethodGuidKeyName = "AcquisitionMethodGuid";
		private const string ProcessingMethodGuidKeyName = "ProcessingMethodGuid";
		private const string CalibrationMethodGuidKeyName = "CalibrationMethodGuid";
		private const string StreamDataKeyName = "StreamData";

		public IBatchRunWithRawData FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var batchResultSetInfo = DomainFactory.Create<IBatchRunWithRawData>();
			batchResultSetInfo.Info = JsonConverterRegistry.GetConverter<IBatchRunInfo>().FromJson((JObject)jObject[InfoKeyName]);
			batchResultSetInfo.AcquisitionMethodGuid = (Guid)jObject[AcquisitionMethodGuidKeyName];
			batchResultSetInfo.ProcessingMethodGuid = (Guid)jObject[ProcessingMethodGuidKeyName];
			batchResultSetInfo.CalibrationMethodGuid = (Guid)jObject[CalibrationMethodGuidKeyName];
			batchResultSetInfo.StreamData = JsonConverterHelper.GetArrayPropertyFromJson<IStreamData>(jObject, StreamDataKeyName);

			return batchResultSetInfo;
		}

		public JObject ToJson(IBatchRunWithRawData instance)
		{
            if (instance == null)
                return null;

            var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{InfoKeyName, JsonConverterRegistry.GetConverter<IBatchRunInfo>().ToJson(instance.Info)},
				{AcquisitionMethodGuidKeyName, new JValue(instance.AcquisitionMethodGuid)},
				{ProcessingMethodGuidKeyName, new JValue(instance.ProcessingMethodGuid)},
				{CalibrationMethodGuidKeyName, new JValue(instance.CalibrationMethodGuid)},
			};

			JsonConverterHelper.SetCollectionPropertyToJObject<IStreamData>(jObject, instance.StreamData, StreamDataKeyName);

			return jObject;
		}
	}
}
