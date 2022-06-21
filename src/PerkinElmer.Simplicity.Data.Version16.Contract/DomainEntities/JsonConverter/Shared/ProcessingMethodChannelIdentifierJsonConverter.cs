using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
	internal class ProcessingMethodChannelIdentifierJsonConverter : IJsonConverter<IProcessingMethodChannelIdentifier>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DeviceClassKeyName = "DeviceClass";
		private const string DeviceIndexKeyName = "DeviceIndex";
		private const string ProcessingMethodDataChannelDescriptorKeyName = "ProcessingMethodDataChannelDescriptor";
		private const string ProcessingMethodChannelIndexKeyName = "ProcessingMethodChannelIndex";


		public JObject ToJson(IProcessingMethodChannelIdentifier instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{DeviceClassKeyName, new JValue((string)instance.DeviceClass)},
				{DeviceIndexKeyName, new JValue((int)instance.DeviceIndex)},
				{ProcessingMethodDataChannelDescriptorKeyName, JsonConverterRegistry.GetConverter<ProcessingMethodDataChannelDescriptor>().ToJson((ProcessingMethodDataChannelDescriptor)instance.ProcessingMethodDataChannelDescriptor)},
				{ProcessingMethodChannelIndexKeyName, new JValue((int)instance.ProcessingMethodChannelIndex)},
			};
			return jObject;
		}

		public IProcessingMethodChannelIdentifier FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
		
			IProcessingMethodDataChannelDescriptor processingMethodDataChannelDescriptor = JsonConverterRegistry.GetConverter<ProcessingMethodDataChannelDescriptor>().FromJson((JObject)jObject[ProcessingMethodDataChannelDescriptorKeyName]);
			var processingMethodChannelIdentifier = new ProcessingMethodChannelIdentifier((string)jObject[DeviceClassKeyName], (int)jObject[DeviceIndexKeyName],
				processingMethodDataChannelDescriptor, (int)jObject[ProcessingMethodChannelIndexKeyName]);


			return processingMethodChannelIdentifier;
		}
	}
}