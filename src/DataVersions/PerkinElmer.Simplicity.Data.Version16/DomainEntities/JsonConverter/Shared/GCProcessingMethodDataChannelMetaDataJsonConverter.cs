using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
	internal class GCProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<GCProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DetectorTypeKeyName = "DetectorType";
		
		public JObject ToJson(GCProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{DetectorTypeKeyName, new JValue(instance.DetectorType)},
				
		};
			return jObject;
		}

		public GCProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
		
			return new GCProcessingMethodDataChannelMetaData((string)jObject[DetectorTypeKeyName]);
			
		}
	}
}