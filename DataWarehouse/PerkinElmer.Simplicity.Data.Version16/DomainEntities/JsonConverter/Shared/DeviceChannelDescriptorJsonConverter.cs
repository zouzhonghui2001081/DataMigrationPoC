using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Implementation.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Shared
{
    internal class DeviceChannelDescriptorJsonConverter : IJsonConverter<IDeviceChannelDescriptor>
    {
        private const int CurrentVersion = 3;
        private const int Version1 = 1;
        private const int Version2 = 2;
        private const string VersionKeyName = "Version";
        private const string DeviceChannelTypeKeyName = "DeviceChannelType";
        private const string DeviceChannelIdentifierKeyName = "DeviceChannelIdentifier";
        private const string MetaDataKeyName = "MetaData";
        public JObject ToJson(IDeviceChannelDescriptor instance)
        {
            if (instance == null)
                return null;

            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {DeviceChannelTypeKeyName, new JValue((int)instance.DeviceChannelType)},
                {
                    DeviceChannelIdentifierKeyName,
                    JsonConverterRegistry.GetConverter<IDeviceChannelIdentifier>()
                        .ToJson(instance.DeviceChannelIdentifier)
                }
            };

            JObject metaDataJObject;

            switch (instance.DeviceChannelType)
            {
                case DeviceChannelType.AToD:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IAToDChannelMetaData>()
                        .ToJson((IAToDChannelMetaData)instance.MetaData);
                    break;
                case DeviceChannelType.GC:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IGCChannelMetaData>()
                        .ToJson((IGCChannelMetaData)instance.MetaData);
                    break;
                case DeviceChannelType.UV:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IUVChannelMetaData>()
                        .ToJson((IUVChannelMetaData)instance.MetaData);
                    break;
                case DeviceChannelType.MultiUV:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IMultiUVChannelMetaData>()
                        .ToJson((IMultiUVChannelMetaData)instance.MetaData);
                    break;
                case DeviceChannelType.PDA:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IPdaChannelMetaData>()
                        .ToJson((IPdaChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.FL:
                    if (instance.MetaData is FLChannelMetaData flChannelMetaData)
                        metaDataJObject = JsonConverterRegistry.GetConverter<IFLChannelMetaData>()
                            .ToJson(flChannelMetaData);
                    else
                        metaDataJObject = JsonConverterRegistry.GetConverter<IFluorescenceSpectrumChannelMetaData>()
                            .ToJson((IFluorescenceSpectrumChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.Temperature:
                    metaDataJObject = JsonConverterRegistry.GetConverter<ITemperatureChannelMetaData>()
                        .ToJson((ITemperatureChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.PumpFlow:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IPumpFlowChannelMetaData>()
                        .ToJson((IPumpFlowChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.SolventProportion:
                    metaDataJObject = JsonConverterRegistry.GetConverter<ISolventProportionChannelMetaData>()
                        .ToJson((ISolventProportionChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.RI:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IRIChannelMetaData>()
                        .ToJson((IRIChannelMetaData)instance.MetaData);
                    break;

                case DeviceChannelType.Pressure:
                    {
                        if (instance.MetaData is PressureChannelMetaData pressureInBarChannelMetaData)
                        {
                            metaDataJObject = JsonConverterRegistry.GetConverter<IPressureChannelMetaData>()
                                .ToJson(pressureInBarChannelMetaData);
                        }
                        else
                        {
                            throw new NotSupportedException($"{instance.MetaData.GetType()} is not supported");
                        }
                        break;
                    }

                case DeviceChannelType.AuxChannel:
                    metaDataJObject = JsonConverterRegistry.GetConverter<IAuxChannelMetaData>()
                        .ToJson((IAuxChannelMetaData)instance.MetaData);
                    break;

                default:
                    throw new Exception("Unsupported device class!");
            }

            jObject.Add(MetaDataKeyName, metaDataJObject);

            return jObject;
        }

        public IDeviceChannelDescriptor FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var deviceChannelDescriptor = DomainFactory.Create<IDeviceChannelDescriptor>();
            deviceChannelDescriptor.DeviceChannelIdentifier = JsonConverterRegistry
                .GetConverter<IDeviceChannelIdentifier>()
                .FromJson((JObject)jObject[DeviceChannelIdentifierKeyName]);

            if (version == Version1)
                deviceChannelDescriptor.DeviceChannelType = GetDeviceChannelTypeForV1(deviceChannelDescriptor.DeviceChannelIdentifier);
            else
                deviceChannelDescriptor.DeviceChannelType = (DeviceChannelType)(int)jObject[DeviceChannelTypeKeyName];

            IChannelMetaData channelMetaData;

            switch (deviceChannelDescriptor.DeviceChannelType)
            {
                case DeviceChannelType.AToD:
                    channelMetaData = JsonConverterRegistry.GetConverter<IAToDChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;
                case DeviceChannelType.GC:
                    channelMetaData = JsonConverterRegistry.GetConverter<IGCChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;
                case DeviceChannelType.UV:
                    channelMetaData = JsonConverterRegistry.GetConverter<IUVChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;
                case DeviceChannelType.MultiUV:
                    channelMetaData = JsonConverterRegistry.GetConverter<IMultiUVChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;
                case DeviceChannelType.PDA:
                    channelMetaData = JsonConverterRegistry.GetConverter<IPdaChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                case DeviceChannelType.FL:
                    channelMetaData = JsonConverterRegistry.GetConverter<IFLChannelMetaData>()
                    .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                case DeviceChannelType.Temperature:
                    channelMetaData = JsonConverterRegistry.GetConverter<ITemperatureChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                case DeviceChannelType.PumpFlow:
                    channelMetaData = JsonConverterRegistry.GetConverter<IPumpFlowChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                case DeviceChannelType.SolventProportion:
                    channelMetaData = JsonConverterRegistry.GetConverter<ISolventProportionChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;


                case DeviceChannelType.RI:
                    channelMetaData = JsonConverterRegistry.GetConverter<IRIChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                case DeviceChannelType.Pressure:
                    channelMetaData = JsonConverterRegistry.GetConverter<IPressureChannelMetaData>()
                        .FromJson((JObject)jObject[MetaDataKeyName]);
                    break;

                default:
                    throw new Exception("Unsupported device class!");
            }
            if (version != Version2)
            {
                switch (deviceChannelDescriptor.DeviceChannelType)
                {
                    case DeviceChannelType.AuxChannel:
                        channelMetaData = JsonConverterRegistry.GetConverter<IAuxChannelMetaData>()
                            .FromJson((JObject)jObject[MetaDataKeyName]);
                        break;
                }
            }
            deviceChannelDescriptor.MetaData = channelMetaData;


            return deviceChannelDescriptor;
        }

        private DeviceChannelType GetDeviceChannelTypeForV1(IDeviceChannelIdentifier deviceChannelIdentifier)
        {
            var deviceClass = deviceChannelIdentifier.DeviceIdentifier.DeviceClass;
            switch (deviceClass)
            {
                case "GC":
                    return DeviceChannelType.GC;
                case "UV":
                    return DeviceChannelType.UV;
                case "PDA":
                    return DeviceChannelType.PDA;
                case "FL":
                    return DeviceChannelType.FL;
                case "RI":
                    return DeviceChannelType.RI;
                case "AtoD":
                    return DeviceChannelType.AToD;
                default:
                    throw new ArgumentException("Unknown value for DeviceClass");
            }
        }
    }
}