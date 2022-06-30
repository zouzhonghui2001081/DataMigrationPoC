using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DataEntities.Chromatography;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.Contract.Version.Chromatography;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Version
{
    internal class BatchResultSetDataJsonConverter : IJsonConverter<BatchResultSetData>
    {
        private const string ProjectGuidKeyName = "ProjectGuid";
        private const string BatchResultSetKeyName = "BatchResultSet";
        private const string DeviceModuleDetailsKeyName = "DeviceModuleDetails";
        private const string DeviceDriverItemDetailsKeyName = "DeviceDriverItemDetails";
        private const string BatchRunsKeyName = "BatchRuns";

        public JObject ToJson(BatchResultSetData instance)
        {
            if (instance == null) return null;

            var jObject = new JObject
            {
                { ProjectGuidKeyName, instance.ProjectGuid},
                { BatchResultSetKeyName, JsonConverterRegistry.GetConverter<BatchResultSet>().ToJson(instance.BatchResultSet)}
            };

            JsonConverterHelper.SetCollectionPropertyToJObject<BatchResultDeviceModuleDetails>(jObject, instance.DeviceModuleDetails, DeviceModuleDetailsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<DeviceDriverItemDetails>(jObject, instance.DeviceDriverItemDetails, DeviceDriverItemDetailsKeyName);
            JsonConverterHelper.SetCollectionPropertyToJObject<BatchRunData>(jObject, instance.BatchRuns, BatchRunsKeyName);
            return jObject;
        }

        public BatchResultSetData FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var batchResultSetData = new BatchResultSetData
            {
                ProjectGuid = (Guid) jObject[ProjectGuidKeyName],
                BatchResultSet = jObject[BatchResultSetKeyName].Type == JTokenType.Null
                    ? null : JsonConverterRegistry.GetConverter<BatchResultSet>().FromJson((JObject) jObject[BatchResultSetKeyName]),
                DeviceModuleDetails = JsonConverterHelper.GetListPropertyFromJson<BatchResultDeviceModuleDetails>(jObject, DeviceModuleDetailsKeyName),
                DeviceDriverItemDetails = JsonConverterHelper.GetListPropertyFromJson<DeviceDriverItemDetails>(jObject, DeviceDriverItemDetailsKeyName),
                BatchRuns = JsonConverterHelper.GetListPropertyFromJson<BatchRunData>(jObject, BatchRunsKeyName)
            };

            return batchResultSetData;
        }
    }
}
