using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Shared
{
	internal class AToDProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<AToDProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";

		public JObject ToJson(AToDProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
			};
			return jObject;
		}

		public AToDProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			return new AToDProcessingMethodDataChannelMetaData();
		
		}
	}
}