using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Acquisition;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Acquisition
{
	internal class AcquisitionMethodJsonConverter : IJsonConverter<IAcquisitionMethod>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InfoKeyName = "Info";
        private const string DeviceMethodsKeyName = "DeviceMethods";
        private const string ReconciledRunTimeKeyName = "ReconciledRunTime";

        public IAcquisitionMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var acquisitionMethod = DomainFactory.Create<IAcquisitionMethod>();
            acquisitionMethod.Info = JsonConverterRegistry.GetConverter<IAcquisitionMethodInfo>().FromJson((JObject)jObject[InfoKeyName]);
			acquisitionMethod.DeviceMethods = JsonConverterHelper.GetArrayPropertyFromJson<IDeviceMethod>(jObject, DeviceMethodsKeyName);
            acquisitionMethod.ReconciledRunTime= (bool)jObject[ReconciledRunTimeKeyName];

            return acquisitionMethod;
        }

        public JObject ToJson(IAcquisitionMethod instance)
        {
            if (instance == null)
                return null;
	        var jObject = new JObject
	        {
		        {VersionKeyName, new JValue(CurrentVersion)},
                {ReconciledRunTimeKeyName, new JValue(instance.ReconciledRunTime)},
                {InfoKeyName, JsonConverterRegistry.GetConverter<IAcquisitionMethodInfo>().ToJson(instance.Info)},
	        };

			var deviceMethods = new JArray();
	        if (instance.DeviceMethods != null)
	        {
		        foreach (var method in instance.DeviceMethods)
		        {
			        deviceMethods.Add(JsonConverterRegistry.GetConverter<IDeviceMethod>().ToJson(method));
		        }
		        jObject.Add(DeviceMethodsKeyName, deviceMethods);
			}
	        else
	        {
		        jObject.Add(DeviceMethodsKeyName, new JValue(instance.DeviceMethods));
			}

			return jObject;
        }
    }
}
