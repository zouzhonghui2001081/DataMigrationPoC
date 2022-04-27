using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
	internal class ProcessingDeviceMetaDataJsonConverter : IJsonConverter<IProcessingDeviceMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
	
		public JObject ToJson(IProcessingDeviceMetaData instance)
		{
            // TODO: there is no implementation for IProcessingDeviceMetaData.
            if (instance == null) return null;
            var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
			};

			return jObject;
		}

		public IProcessingDeviceMetaData FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			try
			{
				var postProcessingMetaData = DomainFactory.Create<IProcessingDeviceMetaData>();

				// TODO: there is no implementation for IProcessingDeviceMetaData.

				return postProcessingMetaData;
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}