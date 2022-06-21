using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.LabManagement;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.LabManagement
{
    public class ProjectInfoJsonConverter: IJsonConverter<IProjectInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";

        private const string DescriptionKeyName = "Description";
        private const string IsEnabledKeyName = "IsEnabled";
        private const string IsSecurityOnKeyName = "IsSecurityOn";
        private const string IsESignatureOnKeyName = "IsESignatureOn";
        private const string IsReviewApprovalOnKeyName = "IsReviewApprovalOn";
        private const string StartDateUtcKeyName = "StartDateUtc";
        private const string EndDateUtcKeyName = "EndDateUtc";

        private const string GuidKeyName = "Guid";
        private const string NameKeyName = "Name";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public JObject ToJson(IProjectInfo instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {DescriptionKeyName, new JValue(instance.Description)},
                {IsEnabledKeyName, new JValue(instance.IsEnabled)},
                {IsSecurityOnKeyName, new JValue(instance.IsSecurityOn)},
                {IsESignatureOnKeyName, new JValue(instance.IsESignatureOn)},
                {IsReviewApprovalOnKeyName, new JValue(instance.IsReviewApprovalOn)},
                {StartDateUtcKeyName, new JValue(instance.StartDateUtc)},
                {EndDateUtcKeyName, new JValue(instance.EndDateUtc)},

                {GuidKeyName, new JValue(instance.Guid)},
                {NameKeyName, new JValue(instance.Name)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
            };
        }

        public IProjectInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IProjectInfo>();
            instance.Description = (string)jObject[DescriptionKeyName];
            instance.IsEnabled = (bool)jObject[IsEnabledKeyName];
            instance.IsSecurityOn = (bool)jObject[IsSecurityOnKeyName];
            instance.IsESignatureOn = (bool)jObject[IsESignatureOnKeyName];
            instance.IsReviewApprovalOn = (bool)jObject[IsReviewApprovalOnKeyName];
            instance.StartDateUtc = (DateTime?)jObject[StartDateUtcKeyName];
            instance.EndDateUtc = (DateTime?)jObject[EndDateUtcKeyName];

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
