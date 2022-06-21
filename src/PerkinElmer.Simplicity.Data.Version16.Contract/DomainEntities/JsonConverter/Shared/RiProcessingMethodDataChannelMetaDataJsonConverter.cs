using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
	internal class RiProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<RiProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";


		public JObject ToJson(RiProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},

			};
			return jObject;
		}

		public RiProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			return new RiProcessingMethodDataChannelMetaData();
		}
	}
}