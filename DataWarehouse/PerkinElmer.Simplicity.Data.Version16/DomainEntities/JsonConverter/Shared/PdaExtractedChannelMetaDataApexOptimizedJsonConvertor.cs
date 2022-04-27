using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
	internal class PdaExtractedChannelMetaDataApexOptimizedJsonConvertor : IJsonConverter<IPdaExtractedChannelMetaDataApexOptimized>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		
		private const string ResponseUnitKeyName = "ResponseUnit";
		private const string DefaultMinYScaleKeyName = "DefaultMinYScale";
		private const string DefaultMaxYScaleKeyName = "DefaultMaxYScale";
		private const string MinValidYValueKeyName = "MinValidYValue";
		private const string MaxValidYValueKeyName = "MaxValidYValue";
		private const string SamplingRateInMilliseconds = "SamplingRateInMilliseconds";

		private const string BaseBrChannelIdentifierKeyName = "BaseBrChannelIdentifier";
		private const string WavelengthBandwidthKeyName = "WavelengthBandwidth";
		private const string UseReferenceKeyName = "UseReference";
		private const string ReferenceWavelengthKeyName = "ReferenceWavelength";
		private const string ReferenceWavelengthBandwidthKeyName = "ReferenceWavelengthBandwidth";
        
		public JObject ToJson(IPdaExtractedChannelMetaDataApexOptimized instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{ResponseUnitKeyName, new JValue(instance.ResponseUnit)},
				{DefaultMinYScaleKeyName, new JValue(instance.DefaultMinYScale)},
				{DefaultMaxYScaleKeyName, new JValue(instance.DefaultMaxYScale)},
				{MinValidYValueKeyName, new JValue(instance.MinValidYValue)},
				{MaxValidYValueKeyName, new JValue(instance.MaxValidYValue)},
				{SamplingRateInMilliseconds, new JValue(instance.SamplingRateInMilliseconds)},
				{BaseBrChannelIdentifierKeyName, new JValue(instance.BaseBrChannelGuid)},
				{WavelengthBandwidthKeyName, new JValue(instance.WavelengthBandwidth)},
				{UseReferenceKeyName, new JValue(instance.UseReference)},
				{ReferenceWavelengthKeyName, new JValue(instance.ReferenceWavelength)},
				{ReferenceWavelengthBandwidthKeyName, new JValue(instance.ReferenceWavelengthBandwidth)},
			};
		}

		public IPdaExtractedChannelMetaDataApexOptimized FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int) jObject[VersionKeyName];
			if (version != CurrentVersion)
				throw new Exception("Unsupported serialized object version!");

			var instance = DomainFactory.Create<IPdaExtractedChannelMetaDataApexOptimized>();
			instance.ResponseUnit = (string) jObject[ResponseUnitKeyName];
			instance.DefaultMinYScale = (double)jObject[DefaultMinYScaleKeyName];
			instance.DefaultMaxYScale = (double)jObject[DefaultMaxYScaleKeyName];
			instance.MinValidYValue = (double)jObject[MinValidYValueKeyName];
			instance.MaxValidYValue = (double)jObject[MaxValidYValueKeyName];
			instance.SamplingRateInMilliseconds = (double)jObject[SamplingRateInMilliseconds];
			instance.BaseBrChannelGuid = (Guid)jObject[BaseBrChannelIdentifierKeyName];
			instance.WavelengthBandwidth = (double) jObject[WavelengthBandwidthKeyName];
			instance.UseReference = (bool) jObject[UseReferenceKeyName];
			instance.ReferenceWavelength = (double)jObject[ReferenceWavelengthKeyName];
			instance.ReferenceWavelengthBandwidth = (double)jObject[ReferenceWavelengthBandwidthKeyName];
            return instance;
		}
	}
}