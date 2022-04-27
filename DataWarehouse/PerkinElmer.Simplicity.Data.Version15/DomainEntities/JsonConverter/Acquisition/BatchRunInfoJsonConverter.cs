using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Acquisition
{
	internal class BatchRunInfoJsonConverter : IJsonConverter<IBatchRunInfo>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string GuidKeyName = "Guid";
		private const string NameKeyName = "Name";
		private const string CreatedDateUtcKeyName = "CreatedDateUtc";
		private const string CreatedByUserKeyName = "CreatedByUser";
		private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
		private const string ModifiedByUserKeyName = "ModifiedByUser";
		private const string AcquisitionCompletionStateKeyName = "AcquisitionCompletionState";
		private const string RepeatIndexKeyName = "RepeatIndex";
		private const string AcquisitionTimeKeyName = "AcquisitionTime";
		private const string SequenceSampleInfoKeyName = "SequenceSampleInfo";

		public IBatchRunInfo FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

			var batchRunInfo = DomainFactory.Create<IBatchRunInfo>();
			batchRunInfo.Guid = (Guid)jObject[GuidKeyName];
			batchRunInfo.Name = (string)jObject[NameKeyName];
			batchRunInfo.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            batchRunInfo.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
			batchRunInfo.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            batchRunInfo.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            batchRunInfo.AcquisitionRunInfo = DomainFactory.Create<IAcquisitionRunInfo>();
            batchRunInfo.AcquisitionRunInfo.AcquisitionCompletionState = (AcquisitionCompletionState)Enum.Parse(typeof(AcquisitionCompletionState), (string)jObject[AcquisitionCompletionStateKeyName], true);
			batchRunInfo.RepeatIndex = (int)jObject[RepeatIndexKeyName];
			batchRunInfo.AcquisitionRunInfo.AcquisitionTime = (DateTime)jObject[AcquisitionTimeKeyName];
			batchRunInfo.SequenceSampleInfo = JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().FromJson((JObject)jObject[SequenceSampleInfoKeyName]);

			return batchRunInfo;
		}

		public JObject ToJson(IBatchRunInfo instance)
		{
            if (instance == null)
                return null;
            var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{GuidKeyName, new JValue(instance.Guid)},
				{NameKeyName, new JValue(instance.Name)},
				{CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
				{CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
				{ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
				{ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
				{AcquisitionCompletionStateKeyName, new JValue(instance.AcquisitionRunInfo.AcquisitionCompletionState.ToString())},
				{RepeatIndexKeyName, new JValue(instance.RepeatIndex)},
				{AcquisitionTimeKeyName, new JValue(instance.AcquisitionRunInfo.AcquisitionTime)},
				{SequenceSampleInfoKeyName, JsonConverterRegistry.GetConverter<ISequenceSampleInfo>().ToJson(instance.SequenceSampleInfo)},
			};

			return jObject;
		}
	}
}
