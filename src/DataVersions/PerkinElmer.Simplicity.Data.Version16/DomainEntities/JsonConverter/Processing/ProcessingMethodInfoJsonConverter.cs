using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
	internal class ProcessingMethodInfoJsonConverter : IJsonConverter<IProcessingMethodInfo>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string GuidKeyName = "Guid";
		private const string NameKeyName = "Name";
		private const string CreatedDateUtcKeyName = "CreatedDateUtc";
		private const string CreatedByUserKeyName = "CreatedByUser";
		private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
		private const string ModifiedByUserKeyName = "ModifiedByUser";
		private const string DescriptionKeyName = "Description";
		private const string IsDefaultKeyName = "IsDefault";
        private const string VersionNumberKeyName = "VersionNumber";
        private const string ReviewApproveStateKeyName = "ReviewApproveState";

        public IProcessingMethodInfo FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var processingMethodInfo = DomainFactory.Create<IProcessingMethodInfo>();
			processingMethodInfo.Guid = (Guid)jObject[GuidKeyName];
			processingMethodInfo.Name = (string)jObject[NameKeyName];
            processingMethodInfo.VersionNumber = (int?)jObject[VersionNumberKeyName];
            processingMethodInfo.ReviewApproveState = JsonConvert.DeserializeObject<ReviewApproveState>((string)jObject[ReviewApproveStateKeyName]);
            processingMethodInfo.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
			processingMethodInfo.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
			processingMethodInfo.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
			processingMethodInfo.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
			processingMethodInfo.Description = (string)jObject[DescriptionKeyName];
			processingMethodInfo.IsDefault = (bool)jObject[IsDefaultKeyName];

			return processingMethodInfo;
		}

		public JObject ToJson(IProcessingMethodInfo instance)
		{
            if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{GuidKeyName, new JValue(instance.Guid)},
				{NameKeyName, new JValue(instance.Name)},
                {VersionNumberKeyName, new JValue(instance.VersionNumber) },
                {ReviewApproveStateKeyName, new JValue(JsonConvert.SerializeObject(instance.ReviewApproveState, new StringEnumConverter()))},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
				{CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
				{ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
				{ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
				{DescriptionKeyName, new JValue(instance.Description)},
				{IsDefaultKeyName, new JValue(instance.IsDefault)},
			};

			return jObject;
		}
	}
}
