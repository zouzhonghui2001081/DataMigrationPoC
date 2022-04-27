using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
	internal class MultiUvProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<MultiUvProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string ProgrammedKeyName = "Programmed";
		private const string WavelengthInNanometersKeyName = "WavelengthInNanometers";
		

		public JObject ToJson(MultiUvProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{ProgrammedKeyName, new JValue(instance.Programmed)},
				{WavelengthInNanometersKeyName, new JValue(instance.WavelengthInNanometers)},
				
			};
			
			return jObject;
		}

		public MultiUvProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
			return new MultiUvProcessingMethodDataChannelMetaData((double)jObject[WavelengthInNanometersKeyName],  (bool)jObject[ProgrammedKeyName]);
		}

		
	}
}