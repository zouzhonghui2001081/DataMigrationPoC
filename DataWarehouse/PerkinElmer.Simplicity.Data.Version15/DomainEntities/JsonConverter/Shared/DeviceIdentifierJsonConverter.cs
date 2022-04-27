using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Shared
{
	internal class DeviceIdentifierJsonConverter : IJsonConverter<IDeviceIdentifier>
	{
		private const int CurrentVersion = 1;
		private const string VersionKeyName = "Version";
		private const string DeviceClassKeyName = "DeviceClass";
		private const string DeviceIndexKeyName = "DeviceIndex";

		public JObject ToJson(IDeviceIdentifier instance)
		{
            return instance == null ? null : new JObject
            {
				{VersionKeyName, new JValue(CurrentVersion)},
				{DeviceClassKeyName, new JValue(instance.DeviceClass)}, 
				{DeviceIndexKeyName, new JValue(instance.DeviceIndex)}
			};
		}

		public IDeviceIdentifier FromJson(JObject jObject)
		{
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int) jObject[VersionKeyName];
			if (version > CurrentVersion)
				throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

			var deviceIdentifier = DomainFactory.Create<IDeviceIdentifier>();
			deviceIdentifier.DeviceClass = (string) jObject[DeviceClassKeyName];
			deviceIdentifier.DeviceIndex = (int) jObject[DeviceIndexKeyName];
			return deviceIdentifier;
		}
	}
}