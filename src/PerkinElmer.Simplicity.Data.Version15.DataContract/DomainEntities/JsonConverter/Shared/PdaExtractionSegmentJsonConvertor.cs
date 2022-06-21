using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Shared
{
	internal class PdaExtractionSegmentJsonConvertor : IJsonConverter<IPdaExtractionSegment>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";

		private const string StartTimeKeyName = "StartTime";
		private const string WavelengthKeyName = "Wavelength";
		private const string BandwidthKeyName = "Bandwidth";
		private const string UseReferenceKeyName = "UseReference";
		private const string ReferenceWavelengthKeyName = "ReferenceWavelength";
		private const string ReferenceBandwidthKeyName = "ReferenceBandwidth";
		private const string AutoZeroKeyName = "AutoZero";

        public JObject ToJson(IPdaExtractionSegment instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{StartTimeKeyName, new JValue(instance.StartTime)},
				{WavelengthKeyName, new JValue(instance.Wavelength)},
				{BandwidthKeyName, new JValue(instance.Bandwidth)},
				{UseReferenceKeyName, new JValue(instance.UseReference)},
				{ReferenceWavelengthKeyName, new JValue(instance.ReferenceWavelength)},
				{ReferenceBandwidthKeyName, new JValue(instance.ReferenceBandwidth)},
				{AutoZeroKeyName, new JValue(instance.AutoZero)},
			};
		}

		public IPdaExtractionSegment FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
           
			var version = (int) jObject[VersionKeyName];
			if (version != CurrentVersion)
				throw new Exception("Unsupported serialized object version!");

			var instance = DomainFactory.Create<IPdaExtractionSegment>();
			instance.StartTime = (double)jObject[StartTimeKeyName];
			instance.Wavelength = (double)jObject[WavelengthKeyName];
			instance.Bandwidth = (double)jObject[BandwidthKeyName];
			instance.UseReference = (bool)jObject[UseReferenceKeyName];
			instance.ReferenceWavelength = (double)jObject[ReferenceWavelengthKeyName];
			instance.ReferenceBandwidth = (double)jObject[ReferenceBandwidthKeyName];
			instance.AutoZero = (bool)jObject[AutoZeroKeyName];

            return instance;
		}
	}
}