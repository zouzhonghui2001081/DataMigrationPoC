using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Shared
{
	internal class UserInfoJsonConverter : IJsonConverter<IUserInfo>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string UserIdKeyName = "UserId";
        private const string UserNameKeyName = "UserName";

        public IUserInfo FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var userInfo = DomainFactory.Create<IUserInfo>();
            userInfo.UserId = (string)jObject[UserIdKeyName];
            userInfo.UserFullName = (string)jObject[UserNameKeyName];
            
            return userInfo;
        }

        public JObject ToJson(IUserInfo instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {UserIdKeyName, new JValue(instance.UserId)},
                {UserNameKeyName, new JValue(instance.UserFullName)},
            };
        }
    }
}
