using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
	internal class DeviceChannelIdentifierJsonConverter : IJsonConverter<IDeviceChannelIdentifier>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DeviceIdentifierKeyName = "DeviceIdentifier";
		private const string ChannelIdentifierKeyName = "ChannelIdentifier";

		public JObject ToJson(IDeviceChannelIdentifier instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{DeviceIdentifierKeyName, JsonConverterRegistry.GetConverter<IDeviceIdentifier>().ToJson(instance.DeviceIdentifier)}, 
				{ChannelIdentifierKeyName, JsonConverterRegistry.GetConverter<IChannelIdentifier1>().ToJson(instance.ChannelIdentifier)}
			};
		}

		public IDeviceChannelIdentifier FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var deviceChannelIdentifier = DomainFactory.Create<IDeviceChannelIdentifier>();
			deviceChannelIdentifier.DeviceIdentifier = JsonConverterRegistry.GetConverter<IDeviceIdentifier>()
				.FromJson((JObject)jObject[DeviceIdentifierKeyName]);

			deviceChannelIdentifier.ChannelIdentifier = JsonConverterRegistry.GetConverter<IChannelIdentifier1>()
				.FromJson((JObject)jObject[ChannelIdentifierKeyName]);

			return deviceChannelIdentifier;
		}
	}
}