using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class ChannelMethodJsonConverter : IJsonConverter<IChannelMethod>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ChannelIndexKeyName = "ChannelIndex";
        private const string ChannelDescriptorKeyName = "ChannelDescriptor";
        private const string ChannelGuidKeyName = "ChannelGuid";
        private const string SmoothParamsKeyName = "SmoothParams";
        private const string IsPdaMethodKeyName = "IsPdaMethod";
        private const string PdaParametersKeyName = "PdaParameters";
        private const string WidthRatioKeyName = "WidthRatio";
        private const string ValleyToPeakRatioKeyName = "ValleyToPeakRatio";
        private const string TimeAdjustmentKeyName = "TimeAdjustment";
        private const string TangentSkimWidthKeyName = "TangentSkimWidth";
        private const string PeakHeightRatioKeyName = "PeakHeightRatio";
        private const string AdjustedHeightRatioKeyName = "AdjustedHeightRatio";
        private const string ValleyHeightRatioKeyName = "ValleyHeightRatio";
        private const string VoidTimeKeyName = "VoidTime";
        private const string VoidTimeTypeKeyName = "VoidTimeType";
        private const string RrtReferenceCompoundKeyName = "RrtReferenceCompound";
        private const string RrtReferenceTypeKeyName = "RrtReferenceType";
        private const string CalibrationFactorKeyName = "CalibrationFactor";
        private const string UnidentifiedPeakCalibrationTypeKeyName = "UnidentifiedPeakCalibrationType";
        private const string AmountUnitKeyName = "AmountUnit";
        private const string TimedIntegrationParametersKeyName = "TimedIntegrationParameters";
        private const string BunchingFactorKeyName = "BunchingFactor";
        private const string NoiseThresholdKeyName = "NoiseThreshold";
        private const string AreaThresholdKeyName = "AreaThreshold";

        public JObject ToJson(IChannelMethod instance)
        {
            if (instance == null) return null;
            var jObject =  new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ChannelIndexKeyName, new JValue(instance.ChannelIndex)},
                {ChannelDescriptorKeyName, JsonConverterRegistry.GetConverter<IChromatographicChannelDescriptor>().ToJson(instance.ChannelDescriptor)},
                {ChannelGuidKeyName, new JValue(instance.ChannelGuid)},
                {SmoothParamsKeyName, JsonConverterRegistry.GetConverter<ISmoothParameters>().ToJson(instance.SmoothParams)},
                {IsPdaMethodKeyName,  new JValue(instance.IsPdaMethod)},
                {PdaParametersKeyName, JsonConverterRegistry.GetConverter<IPdaParameters>().ToJson(instance.PdaParameters) },
                {WidthRatioKeyName, new JValue(instance.WidthRatio) },
                {ValleyToPeakRatioKeyName, new JValue(instance.ValleyToPeakRatio) },
                {TimeAdjustmentKeyName, new JValue(instance.TimeAdjustment) },
                {TangentSkimWidthKeyName, new JValue(instance.TangentSkimWidth) },
                {PeakHeightRatioKeyName, new JValue(instance.PeakHeightRatio) },
                {AdjustedHeightRatioKeyName, new JValue(instance.AdjustedHeightRatio) },
                {ValleyHeightRatioKeyName, new JValue(instance.ValleyHeightRatio) },
                {VoidTimeKeyName, new JValue(instance.VoidTime) },
                {VoidTimeTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.VoidTimeType, new StringEnumConverter()))},
                {RrtReferenceCompoundKeyName, new JValue(instance.RrtReferenceCompound) },
                {RrtReferenceTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.RrtReferenceType, new StringEnumConverter()))},
                {CalibrationFactorKeyName, new JValue(instance.CalibrationFactor) },
                {UnidentifiedPeakCalibrationTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.UnidentifiedPeakCalibrationType, new StringEnumConverter())) },
                {AmountUnitKeyName, new JValue(instance.AmountUnit) },
                {BunchingFactorKeyName, new JValue(instance.BunchingFactor) },
                {NoiseThresholdKeyName, new JValue(instance.NoiseThreshold) },
                {AreaThresholdKeyName, new JValue(instance.AreaThreshold) },
            };
            JsonConverterHelper.SetCollectionPropertyToJObject<IIntegrationEvent>(jObject, instance.TimedIntegrationParameters, TimedIntegrationParametersKeyName);
            return jObject;
        }

        public IChannelMethod FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var channelMethod = DomainFactory.Create<IChannelMethod>();
            channelMethod.ChannelIndex = (int) jObject[ChannelIndexKeyName];
            channelMethod.ChannelDescriptor = jObject[ChannelDescriptorKeyName].Type== JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IChromatographicChannelDescriptor>().FromJson((JObject) jObject[ChannelDescriptorKeyName]);
            channelMethod.ChannelGuid = (Guid)jObject[ChannelGuidKeyName];
            channelMethod.SmoothParams = jObject[SmoothParamsKeyName].Type == JTokenType.Null ? 
                null : JsonConverterRegistry.GetConverter<ISmoothParameters>().FromJson((JObject)jObject[SmoothParamsKeyName]);
            channelMethod.IsPdaMethod = (bool)jObject[IsPdaMethodKeyName];
            channelMethod.PdaParameters = jObject[PdaParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaParameters>().FromJson((JObject)jObject[PdaParametersKeyName]);
            channelMethod.WidthRatio = (double)jObject[WidthRatioKeyName];
            channelMethod.ValleyToPeakRatio = (double)jObject[ValleyToPeakRatioKeyName];
            channelMethod.TimeAdjustment = (double)jObject[TimeAdjustmentKeyName];
            channelMethod.TangentSkimWidth = (double)jObject[TangentSkimWidthKeyName];
            channelMethod.PeakHeightRatio = (double)jObject[PeakHeightRatioKeyName];
            channelMethod.AdjustedHeightRatio = (double)jObject[AdjustedHeightRatioKeyName];
            channelMethod.ValleyHeightRatio = (double)jObject[ValleyHeightRatioKeyName];
            channelMethod.VoidTime = (double)jObject[VoidTimeKeyName];
            channelMethod.VoidTimeType = JsonConvert.DeserializeObject<VoidTimeType>((string)jObject[VoidTimeTypeKeyName]); 
            channelMethod.RrtReferenceCompound = (Guid)jObject[RrtReferenceCompoundKeyName];
            channelMethod.RrtReferenceType = JsonConvert.DeserializeObject<RrtReferenceType>((string)jObject[RrtReferenceTypeKeyName]);  
            channelMethod.CalibrationFactor = (double)jObject[CalibrationFactorKeyName];
            channelMethod.UnidentifiedPeakCalibrationType = JsonConvert.DeserializeObject<UnidentifiedPeakCalibrationType>((string)jObject[UnidentifiedPeakCalibrationTypeKeyName]);  
            channelMethod.AmountUnit = (string)jObject[AmountUnitKeyName];
            channelMethod.TimedIntegrationParameters = JsonConverterHelper.GetListPropertyFromJson<IIntegrationEvent>(jObject, TimedIntegrationParametersKeyName); 
            channelMethod.BunchingFactor = (int?)jObject[BunchingFactorKeyName];
            channelMethod.NoiseThreshold = (double?)jObject[NoiseThresholdKeyName];
            channelMethod.AreaThreshold = (double?)jObject[AreaThresholdKeyName];
            return channelMethod;
        }
    }
}
