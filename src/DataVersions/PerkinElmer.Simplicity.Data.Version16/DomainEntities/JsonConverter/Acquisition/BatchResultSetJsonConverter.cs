using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
	internal class BatchResultSetJsonConverter : IJsonConverter<IBatchResultSet>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string InfoKeyName = "Info";
		private const string SequenceKeyName = "Sequence";
		private const string BatchRunsKeyName = "BatchRuns";
		private const string ExternalBaselineRunsKeyName = "ExternalBaselineRuns";
		private const string AcquisitionMethodsKeyName = "AcquisitionMethods";
		private const string ProcessingMethodsKeyName = "ProcessingMethods";

		public IBatchResultSet FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var batchResultSet = DomainFactory.Create<IBatchResultSet>();
            batchResultSet.Info = jObject[InfoKeyName].Type == JTokenType.Null ? null : JsonConverterRegistry.GetConverter<IBatchResultSetInfo>().FromJson((JObject)jObject[InfoKeyName]);
			batchResultSet.BatchRuns = JsonConverterHelper.GetArrayPropertyFromJson<IBatchRunWithRawData>(jObject, BatchRunsKeyName);
			batchResultSet.ExternalBaselineRuns = JsonConverterHelper.GetArrayPropertyFromJson<IBatchRunWithRawData>(jObject, ExternalBaselineRunsKeyName);
			batchResultSet.AcquisitionMethods = JsonConverterHelper.GetListPropertyFromJson<IAcquisitionMethod>(jObject, AcquisitionMethodsKeyName);
			batchResultSet.ProcessingMethods = JsonConverterHelper.GetListPropertyFromJson<IProcessingMethod>(jObject, ProcessingMethodsKeyName);

			return batchResultSet;
		}

		public JObject ToJson(IBatchResultSet instance)
		{
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InfoKeyName, JsonConverterRegistry.GetConverter<IBatchResultSetInfo>().ToJson(instance.Info)},
            };

			JsonConverterHelper.SetCollectionPropertyToJObject<IBatchRunWithRawData>(jObject, instance.BatchRuns, BatchRunsKeyName);
			JsonConverterHelper.SetCollectionPropertyToJObject<IBatchRunWithRawData>(jObject, instance.ExternalBaselineRuns, ExternalBaselineRunsKeyName);
			JsonConverterHelper.SetCollectionPropertyToJObject<IAcquisitionMethod>(jObject, instance.AcquisitionMethods, AcquisitionMethodsKeyName);
			JsonConverterHelper.SetCollectionPropertyToJObject<IProcessingMethod>(jObject, instance.ProcessingMethods, ProcessingMethodsKeyName);

			return jObject;
		}
	}
}
