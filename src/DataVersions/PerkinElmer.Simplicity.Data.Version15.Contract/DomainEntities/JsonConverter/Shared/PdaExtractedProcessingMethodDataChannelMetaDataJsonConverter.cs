using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Shared
{
	internal class PdaExtractedProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<PdaExtractedProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string WavelengthInNanometersKeyName = "WavelengthInNanometers";
		private const string ProgrammedKeyName = "Programmed";



		public JObject ToJson(PdaExtractedProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{WavelengthInNanometersKeyName, new JValue(instance.WavelengthInNanometers)},
				{ProgrammedKeyName, new JValue(instance.Programmed)},
				
			};
			
			return jObject;
		}

		public PdaExtractedProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			return new PdaExtractedProcessingMethodDataChannelMetaData((double)jObject[WavelengthInNanometersKeyName], (bool)jObject[ProgrammedKeyName]);
		}
	}
}