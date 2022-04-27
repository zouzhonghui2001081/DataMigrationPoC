using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class ProcessingMethodDataChannelDescriptorJsonConverter : IJsonConverter<ProcessingMethodDataChannelDescriptor>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DataChannelMetaDataKeyName = "DataChannelMetaData";
		private const string DataChannelTypeKeyName = "DataChannelType";
		public JObject ToJson(ProcessingMethodDataChannelDescriptor instance)
		{
			if (instance == null)
				return null;

			var jObject = new JObject
			{
				{VersionKeyName, new JValue(CurrentVersion)},
                //{DataChannelMetaDataKeyName, new JValue(instance.DataChannelMetaData)},
                {DataChannelTypeKeyName, new JValue(instance.DataChannelType)},
			};

			JObject metaDataJObject = null;
			switch (instance.DataChannelType)
			{
				case ProcessingMethodDataChannelType.AToD:
					metaDataJObject = JsonConverterRegistry.GetConverter<AToDProcessingMethodDataChannelMetaData>()
						.ToJson((AToDProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.PdaMic:
					metaDataJObject = JsonConverterRegistry.GetConverter<PdaMicProcessingMethodDataChannelMetaData>()
						.ToJson((PdaMicProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.GC:
					metaDataJObject = JsonConverterRegistry.GetConverter<GCProcessingMethodDataChannelMetaData>()
						.ToJson((GCProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.FL:
					metaDataJObject = JsonConverterRegistry.GetConverter<FLProcessingMethodDataChannelMetaData>()
						.ToJson((FLProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.UV:
					metaDataJObject = JsonConverterRegistry.GetConverter<UVProcessingMethodDataChannelMetaData>()
						.ToJson((UVProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.RI:
					metaDataJObject = JsonConverterRegistry.GetConverter<RiProcessingMethodDataChannelMetaData>()
						.ToJson((RiProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.PdaApexOptimized:
					metaDataJObject = JsonConverterRegistry.GetConverter<PdaApexOptimizedProcessingMethodDataChannelMetaData>()
						.ToJson((PdaApexOptimizedProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.PdaExtracted:
					metaDataJObject = JsonConverterRegistry.GetConverter<PdaExtractedProcessingMethodDataChannelMetaData>()
						.ToJson((PdaExtractedProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
				case ProcessingMethodDataChannelType.MultiUV:
					metaDataJObject = JsonConverterRegistry.GetConverter<MultiUvProcessingMethodDataChannelMetaData>()
						.ToJson((MultiUvProcessingMethodDataChannelMetaData)instance.DataChannelMetaData);
					break;
			}
			jObject.Add(DataChannelMetaDataKeyName, metaDataJObject);

			return jObject;
		}

		public ProcessingMethodDataChannelDescriptor FromJson(JObject jObject)
		{
			if (jObject == null || jObject.Type == JTokenType.Null) return null;
			var version = (int)jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			ProcessingMethodDataChannelType channelType = JsonConvert.DeserializeObject<ProcessingMethodDataChannelType>((string)jObject[DataChannelTypeKeyName]);


			IProcessingMethodDataChannelMetaData channelMetaData = null;
			switch (channelType)
			{
				case ProcessingMethodDataChannelType.AToD:
					channelMetaData = JsonConverterRegistry.GetConverter<AToDProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.PdaMic:
					channelMetaData = JsonConverterRegistry.GetConverter<PdaMicProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.GC:
					channelMetaData = JsonConverterRegistry.GetConverter<GCProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.FL:
					channelMetaData = JsonConverterRegistry.GetConverter<FLProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.UV:
					channelMetaData = JsonConverterRegistry.GetConverter<UVProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.RI:
					channelMetaData = JsonConverterRegistry.GetConverter<RiProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.PdaApexOptimized:
					channelMetaData = JsonConverterRegistry.GetConverter<PdaApexOptimizedProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.PdaExtracted:
					channelMetaData = JsonConverterRegistry.GetConverter<PdaExtractedProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;
				case ProcessingMethodDataChannelType.MultiUV:
					channelMetaData = JsonConverterRegistry.GetConverter<MultiUvProcessingMethodDataChannelMetaData>()
						.FromJson((JObject)jObject[DataChannelMetaDataKeyName]);
					break;

			}
			return new ProcessingMethodDataChannelDescriptor(channelType, channelMetaData);
		}


	}
}