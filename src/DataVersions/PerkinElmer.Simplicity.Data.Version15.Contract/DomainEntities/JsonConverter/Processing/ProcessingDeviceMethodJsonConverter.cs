using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
	internal class ProcessingDeviceMethodJsonConverter : IJsonConverter<IProcessingDeviceMethod>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DeviceIdentifierKeyName = "DeviceIdentifier";
		private const string MetaDataKeyName = "MetaData";

		public IProcessingDeviceMethod FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			try
			{
				var processingDeviceMethod = DomainFactory.Create<IProcessingDeviceMethod>();
				processingDeviceMethod.DeviceIdentifier = JsonConverterRegistry.GetConverter<IDeviceIdentifier>()
					.FromJson((JObject) jObject[DeviceIdentifierKeyName]);
				processingDeviceMethod.MetaData = JsonConverterRegistry.GetConverter<IProcessingDeviceMetaData>()
					.FromJson((JObject) jObject[MetaDataKeyName]);

				return processingDeviceMethod;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public JObject ToJson(IProcessingDeviceMethod instance)
		{
			// TODO: there is no implementation for IProcessingDeviceMethod.

			if (instance == null)
			{
				var nullObj = new JObject
				{
					{VersionKeyName, new JValue(CurrentVersion)}
				};

				return nullObj;
			}

			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{DeviceIdentifierKeyName, JsonConverterRegistry.GetConverter<IDeviceIdentifier>().ToJson(instance.DeviceIdentifier)},
				{MetaDataKeyName, JsonConverterRegistry.GetConverter<IProcessingDeviceMetaData>().ToJson(instance.MetaData)},
			};

			return jObject;
		}
	}
}
