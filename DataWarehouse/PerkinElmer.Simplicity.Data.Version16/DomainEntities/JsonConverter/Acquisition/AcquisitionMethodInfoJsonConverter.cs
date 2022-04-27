using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.ReviewApprove;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Acquisition
{
	internal class AcquisitionMethodInfoJsonConverter : IJsonConverter<IAcquisitionMethodInfo>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string GuidKeyName = "Guid";
		private const string NameKeyName = "Name";
		private const string CreatedDateUtcKeyName = "CreatedDateUtc";
		private const string CreatedByUserKeyName = "CreatedByUser";
		private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
		private const string ModifiedByUserKeyName = "ModifiedByUser";
		private const string DevicesKeyName = "Devices";
        private const string VersionNumberKeyName = "VersionNumber";
        private const string ReviewApproveStateKeyName = "ReviewApproveState";

        public IAcquisitionMethodInfo FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var acquisitionMethodInfo = DomainFactory.Create<IAcquisitionMethodInfo>();
			acquisitionMethodInfo.Guid = (Guid)jObject[GuidKeyName];
			acquisitionMethodInfo.Name = (string)jObject[NameKeyName];
            acquisitionMethodInfo.VersionNumber = (int)jObject[VersionNumberKeyName];
            acquisitionMethodInfo.ReviewApproveState = JsonConvert.DeserializeObject<ReviewApproveState>((string)jObject[ReviewApproveStateKeyName]);
            acquisitionMethodInfo.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            acquisitionMethodInfo.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
			acquisitionMethodInfo.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            acquisitionMethodInfo.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
			acquisitionMethodInfo.Devices = JsonConverterHelper.GetArrayPropertyFromJson<string>(jObject, DevicesKeyName);

			return acquisitionMethodInfo;
		}

		public JObject ToJson(IAcquisitionMethodInfo instance)
		{
            if (instance == null)
                return null;
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
			};

			JsonConverterHelper.SetCollectionPropertyToJObject<string>(jObject, instance.Devices, DevicesKeyName);

			return jObject;
		}
	}
}
