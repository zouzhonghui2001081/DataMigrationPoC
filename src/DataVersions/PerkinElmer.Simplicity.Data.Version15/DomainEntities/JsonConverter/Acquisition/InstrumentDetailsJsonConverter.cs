using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Implementation.Shared;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Acquisition;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Acquisition
{
    internal class InstrumentDetailsJsonConverter : IJsonConverter<IInstrumentDetails>
    {
        private const int CurrentVersion = 2;
        private const int Version1 = 1;
        private const string VersionKeyName = "Version";
        private const string RegulatedKeyName = "Regulated";
        private const string OnlineKeyName = "Online";
        private const string InstrumentLockStateKeyName = "InstrumentLockState";
        private const string LockedUserKeyName = "LockedUser";
        private const string DeviceModulesKeyName = "DeviceModules";
        private const string DeviceDriverItemsKeyName = "DeviceDriverItems";
        private const string IDKeyName = "Id";
        private const string NameKeyName = "Name";

        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";
        private InstrumentCompleteIdJsonConverter _instrumentCompleteIdJsonConverter = new InstrumentCompleteIdJsonConverter();
        private DeviceDriverItemDetailsJsonConverter _deviceDriverItemDetailsJsonConverter = new DeviceDriverItemDetailsJsonConverter();

        public JObject ToJson(IInstrumentDetails instance)
        {
            if (instance == null)
                return null;
            var jObject = new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {RegulatedKeyName, new JValue(instance.Regulated) },
                {OnlineKeyName, new JValue(instance.Online) },
                {InstrumentLockStateKeyName, new JValue(JsonConvert.SerializeObject(instance.InstrumentLockState, new StringEnumConverter())) },
                {LockedUserKeyName, new JValue(instance.LockedUser) },
                {IDKeyName, _instrumentCompleteIdJsonConverter.ToJson(instance.Id) },
                {NameKeyName, new JValue(instance.Name) },

                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedBy)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedBy)},
            };
            JsonConverterHelper.SetListPropertyToJObject<IDeviceModuleDetails>(jObject, instance.DeviceModules, DeviceModulesKeyName);
            JsonConverterHelper.SetListPropertyToJObject<IDeviceDriverItemDetails>(jObject, instance.DeviceDriverItems, DeviceDriverItemsKeyName);
            return jObject;
        }

        public IInstrumentDetails FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];

            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var instance = DomainFactory.Create<IInstrumentDetailsModifiable>();
            instance.SetRegulated((bool)jObject[RegulatedKeyName]);
            instance.SetOnline((bool)jObject[OnlineKeyName]);
            instance.SetId(_instrumentCompleteIdJsonConverter.FromJson((JObject)jObject[IDKeyName]));
            instance.SetName((string)jObject[NameKeyName]);
            var user = jObject[LockedUserKeyName].ToString();
            if (user.ToLower() == "null")
                instance.Unlock();
            else
            {
                InstrumentLockState? state = JsonConvert.DeserializeObject<InstrumentLockState>((string)jObject[InstrumentLockStateKeyName]);
                if (state == InstrumentLockState.Locked)
                    instance.Lock(false, user);
                else if (state == InstrumentLockState.LockedForConfiguring)
                    instance.Lock(true, user);
                else
                    instance.Unlock();
            }

            if (version == Version1)
            {
                IUserInfo userInfo = new UserInfo();
                userInfo.UserId = Guid.Empty.ToString();
                userInfo.UserFullName = "SimplicityChrom";
                instance.SetCreatedUserInfo(userInfo);
                instance.SetCreatedDateTime(DateTime.MinValue);
                instance.SetModifiedUserInfo(userInfo);
                instance.SetModifiedDateTime(DateTime.MinValue);
            }
            else
            {
                instance.SetCreatedUserInfo((IUserInfo)JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]));
                instance.SetCreatedDateTime((DateTime?)jObject[CreatedDateUtcKeyName]);
                instance.SetModifiedUserInfo((IUserInfo)JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]));
                instance.SetModifiedDateTime((DateTime?)jObject[ModifiedDateUtcKeyName]);
            }


            var deviceModuleDetailsList = instance.DeviceModuleDetailsModifiableList;
            var deviceModuleDetailsListFromJson = JsonConverterHelper.GetListPropertyFromJson<IDeviceModuleDetails>(jObject, DeviceModulesKeyName);
            foreach (var deviceModuleDetails in deviceModuleDetailsListFromJson)
            {
                var deviceModuleDetailsModifiable = DomainFactory.Create<IDeviceModuleDetailsModifiable>();
                deviceModuleDetailsModifiable.Set(deviceModuleDetails);
                deviceModuleDetailsModifiable.SetSimulation(deviceModuleDetails.Simulation);

                var deviceInformation = (DeviceInformation)DomainFactory.Create<IDeviceInformation>();
                deviceInformation.FirmwareVersion = deviceModuleDetails.DeviceInformation?.FirmwareVersion ?? "-";
                deviceInformation.SerialNumber = deviceModuleDetails.DeviceInformation?.SerialNumber ?? "-";
                deviceInformation.ModelName = deviceModuleDetails.DeviceInformation?.ModelName ?? "-";
                deviceInformation.UniqueIdentifier = deviceModuleDetails.DeviceInformation?.UniqueIdentifier ?? "-";
                deviceInformation.InterfaceAddress = deviceModuleDetails.DeviceInformation?.InterfaceAddress ?? "-";

                deviceModuleDetailsModifiable.SetCommunicationTestedSuccessfully(deviceModuleDetails.CommunicationTestedSuccessfully, deviceInformation);
                deviceModuleDetailsModifiable.SetSettingsUserInterfaceSupported(deviceModuleDetails.SettingsUserInterfaceSupported);
                deviceModuleDetailsList.Add(deviceModuleDetailsModifiable);
            }

            var deviceDriverItemDetailsList = instance.DeviceDriverItemDetailsModifiableList;
            var deviceDriverItemDetailsListFromJson = JsonConverterHelper.GetListPropertyFromJson<IDeviceDriverItemDetails>(jObject, DeviceDriverItemsKeyName);
            foreach (var deviceDriverItemDetails in deviceDriverItemDetailsListFromJson)
            {
                var deviceDriverItemDetailsModifiable = DomainFactory.Create<IDeviceDriverItemDetailsModifiable>();
                deviceDriverItemDetailsModifiable.Set(deviceDriverItemDetails.Id, deviceDriverItemDetails.Name);
                deviceDriverItemDetailsModifiable.SetConfiguration(deviceDriverItemDetails.Configuration);
                deviceDriverItemDetailsModifiable.SetDeviceType(deviceDriverItemDetails.DeviceType, deviceDriverItemDetails.IsDisplayDriver);
                deviceDriverItemDetailsList.Add(deviceDriverItemDetailsModifiable);
            }

            return instance;
        }
    }
}
