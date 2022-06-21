using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.LabManagement
{
    public class ESignaturePointInfoJsonConverter: IJsonConverter<IESignaturePointInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";

        private const string ModuleNameKeyName = "ModuleName";
        private const string DisplayOrderKeyName = "DisplayOrder";
        private const string IsUseAuthKeyName = "IsUseAuth";
        private const string IsCustomReasonKeyName = "IsCustomReason";
        private const string IsPredefinedReasonKeyName = "IsPredefinedReason";
        private const string PredefineReasonsKeyName = "PredefineReasons";

        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public JObject ToJson(IESignaturePointInfo instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ModuleNameKeyName, new JValue(instance.ModuleName)},
                {DisplayOrderKeyName, new JValue(instance.DisplayOrder)},
                {IsUseAuthKeyName, new JValue(instance.IsUseAuth)},
                {IsCustomReasonKeyName, new JValue(instance.IsCustomReason)},
                {IsPredefinedReasonKeyName, new JValue(instance.IsPredefinedReason)},
                {PredefineReasonsKeyName, instance.PredefineReasons ==null?
                    null: JArray.FromObject(instance.PredefineReasons)},

                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
            };
        }

        public IESignaturePointInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IESignaturePointInfo>();
            instance.ModuleName = (string)jObject[ModuleNameKeyName];
            instance.DisplayOrder = (int)jObject[DisplayOrderKeyName];
            instance.IsUseAuth = (bool)jObject[IsUseAuthKeyName];
            instance.IsCustomReason = (bool)jObject[IsCustomReasonKeyName];
            instance.IsPredefinedReason = (bool)jObject[IsPredefinedReasonKeyName];
            instance.PredefineReasons =
                JsonConverterHelper.GetListPropertyFromJson<string>(jObject, PredefineReasonsKeyName);

            instance.Guid = (Guid)jObject[GuidKeyName];
            instance.Name = (string)jObject[NameKeyName];
            instance.CreatedByUser = JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            instance.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
            instance.ModifiedByUser = JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            instance.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];

            return instance;
        }
    }
}
