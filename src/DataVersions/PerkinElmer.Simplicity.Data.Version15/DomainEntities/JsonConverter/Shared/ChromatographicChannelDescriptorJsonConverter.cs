using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class ChromatographicChannelDescriptorJsonConverter : IJsonConverter<IChromatographicChannelDescriptor>
	{
		private const int CurrentVersion = 2;
		private const string VersionKeyName = "Version";
		private const string DeviceChannelDescriptorKeyName = "DeviceChannelDescriptor";
		private const string ExtractedKeyName = "ExtractionType";
		private const string ExtractionMetaDataKeyName = "ExtractionMetaData";
		private const string PostProcessingChannelTypeKeyName = "PostProcessingChannelType";
		private const string PostProcessingMetaDataKeyName = "PostProcessingMetaData";

		public JObject ToJson(IChromatographicChannelDescriptor instance)
		{
            if (instance == null) return null;
			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
				{DeviceChannelDescriptorKeyName, JsonConverterRegistry.GetConverter<IDeviceChannelDescriptor>().ToJson(instance.DeviceChannelDescriptor)},
				{ExtractedKeyName, new JValue((int)instance.ExtractionType)},
			};

			if (instance.ExtractionType != ExtractionType.None)
			{
				var extrMetaDataJObject = SerializeExtractedChannelMetaData(instance);
				jObject.Add(ExtractionMetaDataKeyName, extrMetaDataJObject);
			}

			jObject.Add(PostProcessingChannelTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.PostProcessingType, new StringEnumConverter())));

			JObject postProcessingMetaData;
			if (instance.PostProcessingType.ToString() == PostProcessingChannelType.BlankSubtracted.ToString())
			{
				postProcessingMetaData = JsonConverterRegistry.GetConverter<IPostProcessingMetaData>().ToJson((IPostProcessingMetaData)instance.PostProcessingMetaData);
				jObject.Add(PostProcessingMetaDataKeyName, postProcessingMetaData);
			}

			return jObject;
		}

		public IChromatographicChannelDescriptor FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var chromatographicChannelDescriptor = DomainFactory.Create<IChromatographicChannelDescriptor>();

			// DeviceChannelDescriptor
			chromatographicChannelDescriptor.DeviceChannelDescriptor = jObject[DeviceChannelDescriptorKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IDeviceChannelDescriptor>().FromJson((JObject)jObject[DeviceChannelDescriptorKeyName]);

			// ExtractionType
			ExtractionType extractedType;
			if (version < 2)
			{
				extractedType = ConvertV1ExtractionTypeStringToEnum((string)jObject[ExtractedKeyName]);
			}
			else
			{
				extractedType = (ExtractionType)(int)jObject[ExtractedKeyName];
			}
			chromatographicChannelDescriptor.ExtractionType = extractedType;

			// ExtractionMetaData
			if (extractedType != ExtractionType.None)
			{
				chromatographicChannelDescriptor.ExtractionMetaData = jObject[ExtractionMetaDataKeyName].Type == JTokenType.Null
					? null
					: DeserializeExtractedChannelMetaData(jObject, extractedType);
			}

			// PostProcessingType
			chromatographicChannelDescriptor.PostProcessingType = JsonConvert.DeserializeObject<PostProcessingChannelType>((string)jObject[PostProcessingChannelTypeKeyName]);

			// PostProcessingMetaData
			if (chromatographicChannelDescriptor.PostProcessingType == PostProcessingChannelType.BlankSubtracted)
			{
				chromatographicChannelDescriptor.PostProcessingMetaData = (jObject[PostProcessingMetaDataKeyName].Type == JTokenType.Null) ? null:
                    JsonConverterRegistry.GetConverter<IPostProcessingMetaData>()
					.FromJson((JObject)jObject[PostProcessingMetaDataKeyName]);
			}

			return chromatographicChannelDescriptor;
		}

		private ExtractionType ConvertV1ExtractionTypeStringToEnum(string v1ExtractionTypeString)
		{
			var cleanedV1ExtractionTypeString = v1ExtractionTypeString.Replace("\"", "");
			switch (cleanedV1ExtractionTypeString)
			{
				case "None":
					return ExtractionType.None;
				case "Extracted":
					return ExtractionType.PdaSimple;
				case "MultipleProgrammed":
					return ExtractionType.PdaProgrammed;
				case "ApexOptimized":
					return ExtractionType.PdaApexOptimized;
				default:
					throw new ArgumentException("Unknown value for ExtractionType");
			}
		}

		private static JObject SerializeExtractedChannelMetaData(IChromatographicChannelDescriptor instance)
		{
			JObject extrMetaDataJObject;
			var extrMetadata = instance.ExtractionMetaData;
			switch (instance.ExtractionType)
			{
				case ExtractionType.PdaSimple:
					extrMetaDataJObject = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataSimple>()
						.ToJson((IPdaExtractedChannelMetaDataSimple) extrMetadata);
					break;
				case ExtractionType.PdaProgrammed:
					extrMetaDataJObject = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataProgrammed>()
						.ToJson((IPdaExtractedChannelMetaDataProgrammed) extrMetadata);
					break;
				case ExtractionType.PdaApexOptimized:
					// TODO: At the same time when IsApexOptimized member is removed from IPdaExtractedChannelMetaDataSimple, serialize as IPdaExtractedChannelMetaDataApexOptimized
					extrMetaDataJObject = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataSimple>()
						.ToJson((IPdaExtractedChannelMetaDataSimple)extrMetadata);
					break;
				case ExtractionType.PdaMic:
					extrMetaDataJObject = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataMic>().ToJson((IPdaExtractedChannelMetaDataMic) extrMetadata);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return extrMetaDataJObject;
		}

		private static IExtractedChannelMetaData DeserializeExtractedChannelMetaData(JObject jObject, ExtractionType extractedType)
		{
			IExtractedChannelMetaData extrMetadata;
			var extrMetadataJObject = (JObject) jObject[ExtractionMetaDataKeyName];
			switch (extractedType)
			{
				case ExtractionType.PdaSimple:
					extrMetadata = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataSimple>().FromJson(extrMetadataJObject);
					break;
				case ExtractionType.PdaProgrammed:
					extrMetadata = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataProgrammed>().FromJson(extrMetadataJObject);
					break;
				case ExtractionType.PdaApexOptimized:
					// TODO: At the same time when IsApexOptimized member is removed from IPdaExtractedChannelMetaDataSimple, deserialize as IPdaExtractedChannelMetaDataApexOptimized
					extrMetadata = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataSimple>().FromJson(extrMetadataJObject);
					break;
				case ExtractionType.PdaMic:
					extrMetadata = JsonConverterRegistry.GetConverter<IPdaExtractedChannelMetaDataMic>().FromJson(extrMetadataJObject);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return extrMetadata;
		}
	}
}