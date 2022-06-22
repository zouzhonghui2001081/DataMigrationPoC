using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
	internal class SpectrumMethodJsonConverter : IJsonConverter<ISpectrumMethod>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string GuidKeyName = "Guid";
		private const string StartRetentionTimeKeyName = "StartRetentionTime";
		private const string EndRetentionTimeKeyName = "EndRetentionTime";
		private const string BaselineStartRetentionTimeKeyName = "BaselineStartRetentionTime";
		private const string BaselineEndRetentionTimeKeyName = "BaselineEndRetentionTime";
		private const string BaselineCorrectionTypeKeyName = "BaselineCorrectionType";

		public ISpectrumMethod FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var spectrumMethod = DomainFactory.Create<ISpectrumMethod>();
			spectrumMethod.Guid = (Guid)jObject[GuidKeyName];
			spectrumMethod.StartRetentionTime = (double)jObject[StartRetentionTimeKeyName];
			spectrumMethod.EndRetentionTime = (double)jObject[EndRetentionTimeKeyName];
			spectrumMethod.BaselineStartRetentionTime = (double)jObject[BaselineStartRetentionTimeKeyName];
			spectrumMethod.BaselineEndRetentionTime = (double)jObject[BaselineEndRetentionTimeKeyName];
			spectrumMethod.BaselineCorrectionType = (BaselineCorrectionType)Enum.Parse(typeof(BaselineCorrectionType), (string)jObject[BaselineCorrectionTypeKeyName], true);

			return spectrumMethod;
		}

		public JObject ToJson(ISpectrumMethod instance)
        {
            if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{GuidKeyName, new JValue(instance.Guid)},
				{StartRetentionTimeKeyName, new JValue(instance.StartRetentionTime)},
				{BaselineStartRetentionTimeKeyName, new JValue(instance.BaselineStartRetentionTime)},
				{EndRetentionTimeKeyName, new JValue(instance.EndRetentionTime)},
				{BaselineEndRetentionTimeKeyName, new JValue(instance.BaselineEndRetentionTime)},
				{BaselineCorrectionTypeKeyName,new JValue(instance.BaselineCorrectionType.ToString())},
			};

			return jObject;
		}
	}
}
