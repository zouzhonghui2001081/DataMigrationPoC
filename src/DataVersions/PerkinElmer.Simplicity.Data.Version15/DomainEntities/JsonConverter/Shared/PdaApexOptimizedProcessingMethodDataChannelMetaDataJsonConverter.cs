using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class PdaApexOptimizedProcessingMethodDataChannelMetaDataJsonConverter : IJsonConverter<PdaApexOptimizedProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string WavelengthInNanometersKeyName = "WavelengthInNanometers";
		private const string ProgrammedKeyName = "Programmed";

		public JObject ToJson(PdaApexOptimizedProcessingMethodDataChannelMetaData instance)
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

		public PdaApexOptimizedProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
			
			return new PdaApexOptimizedProcessingMethodDataChannelMetaData((double)jObject[WavelengthInNanometersKeyName], (bool)jObject[ProgrammedKeyName]);
		}
	}
}