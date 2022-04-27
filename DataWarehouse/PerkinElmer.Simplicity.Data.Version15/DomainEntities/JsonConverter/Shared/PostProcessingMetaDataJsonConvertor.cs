using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class PostProcessingMetaDataJsonConvertor : IJsonConverter<IPostProcessingMetaData>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string SubtractedBlankOrigBatchRunGuidKeyName = "SubtractedBlankOrigBatchRunGuid";
	
		public JObject ToJson(IPostProcessingMetaData instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{SubtractedBlankOrigBatchRunGuidKeyName, new JValue(instance.SubtractedBlankOrigBatchRunGuid)}, 
			};
		}

		public IPostProcessingMetaData FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var postProcessingMetaData = DomainFactory.Create<IPostProcessingMetaData>();
			postProcessingMetaData.SubtractedBlankOrigBatchRunGuid = (Guid) jObject[SubtractedBlankOrigBatchRunGuidKeyName];

			return postProcessingMetaData;
		}
	}
}