using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class FLProcessingMethodDataChannelMetaDataJSonConverter : IJsonConverter<FLProcessingMethodDataChannelMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string ProgrammedKeyName = "Programmed";
		private const string ExcitationInNanometersKeyName = "ExcitationInNanometers";
		private const string EmissionInNanometersKeyName = "EmissionInNanometers";

		public JObject ToJson(FLProcessingMethodDataChannelMetaData instance)
		{
			if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{ProgrammedKeyName, new JValue(instance.Programmed)},
				{ExcitationInNanometersKeyName, new JValue(instance.ExcitationInNanometers)},
				{EmissionInNanometersKeyName, new JValue(instance.EmissionInNanometers)},
			};
			
			return jObject;
		}

		public FLProcessingMethodDataChannelMetaData FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;

			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
			return new FLProcessingMethodDataChannelMetaData((double)jObject[ExcitationInNanometersKeyName], (double)jObject[EmissionInNanometersKeyName], (bool)jObject[ProgrammedKeyName]);
		}

		
	}
}