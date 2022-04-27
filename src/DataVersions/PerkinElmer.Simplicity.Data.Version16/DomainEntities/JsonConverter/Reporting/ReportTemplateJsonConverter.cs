using System;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Reporting;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Reporting
{
    public class ReportTemplateJsonConverter: IJsonConverter<IReportTemplate>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string IdKeyName = "Id";
        private const string CategoryKeyName = "Category";
        private const string NameKeyName = "Name";
        private const string CreatedDateKeyName = "CreatedDate";
        private const string CreatedByUserKeyName = "CreatedUserId";
        private const string ModifiedDateKeyName = "ModifiedDate";
        private const string ModifiedByUserKeyName = "ModifiedByUser";
        private const string ContentKeyName = "Content";
        private const string ConfigKeyName = "Config";
        private const string ProjectIdKeyName = "ProjectId";
        private const string IsGlobalKeyName = "IsGlobal";
        private const string IsDefaultKeyName = "IsDefault";       

        public JObject ToJson(IReportTemplate instance)
        {
            if (instance == null) return null;

            var content = string.Empty;
            var config = string.Empty;
            if (instance.Content != null)
            {
                instance.Content.Position = 0;
                using (var contentReader = new StreamReader(instance.Content))
                {
                    content = contentReader.ReadToEnd();
                }
            }
            if (instance.Config != null)
            {
                instance.Config.Position = 0;
                using (var configReader = new StreamReader(instance.Config))
                {
                    config = configReader.ReadToEnd();
                }
            }
            
            return new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {IdKeyName, new JValue(instance.Id)},
                {CategoryKeyName, new JValue(instance.Category.GetDescription())},
                {NameKeyName, new JValue(instance.Name)},
                {CreatedDateKeyName, new JValue(instance.CreatedDate)},
                {ModifiedDateKeyName, new JValue(instance.ModifiedDate)},
                {ContentKeyName, new JValue(content)},
                {ConfigKeyName, new JValue(config) },
                {ProjectIdKeyName, new JValue(instance.ProjectId)},
                {IsGlobalKeyName, new JValue(instance.IsGlobal)},
                {IsDefaultKeyName, new JValue(instance.IsDefault)},
                {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
            };
        }

        public IReportTemplate FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var instance = DomainFactory.Create<IReportTemplate>();
            instance.Id = (Guid)jObject[IdKeyName];
            instance.Category = ((string)jObject[CategoryKeyName]).ToReportTemplateType();
            instance.Name = (string)jObject[NameKeyName];
            instance.CreatedDate = (DateTime)jObject[CreatedDateKeyName];
            instance.ModifiedDate = (DateTime)jObject[ModifiedDateKeyName];
            byte[] contentByteArray = Encoding.ASCII.GetBytes((string)jObject[ContentKeyName]);
            byte[] configByteArray = Encoding.ASCII.GetBytes((string)jObject[ConfigKeyName]);
            if (contentByteArray != null && contentByteArray.Length > 0)
            {
                instance.Content = new MemoryStream(contentByteArray);
            }
            if (configByteArray != null && configByteArray.Length > 0)
            {
                instance.Config = new MemoryStream(configByteArray);
            }
            instance.ProjectId = (long?)jObject[ProjectIdKeyName];
            instance.IsGlobal = (bool)jObject[IsGlobalKeyName];
            instance.IsDefault = (bool)jObject[IsDefaultKeyName];
            instance.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            instance.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ? null :
                JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);

            return instance;
        }
    }
}
