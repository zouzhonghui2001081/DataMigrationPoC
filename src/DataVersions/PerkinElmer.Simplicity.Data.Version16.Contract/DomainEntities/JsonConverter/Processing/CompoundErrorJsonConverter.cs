using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CompoundErrorJsonConverter : IJsonConverter<ICompoundError>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ErrorCodeKeyName = "ErrorCode";
        private const string CompoundNameKeyName = "CompoundName";
        private const string ExpectedRetentionTimeKeyName = "ExpectedRetentionTime";

        public JObject ToJson(ICompoundError instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ErrorCodeKeyName, new JValue(JsonConvert.SerializeObject(instance.ErrorCode, new StringEnumConverter()))},
                {CompoundNameKeyName, new JValue(instance.CompoundName)},
                {ExpectedRetentionTimeKeyName, new JValue(instance.ExpectedRetentionTime)}
            };
        }

        public ICompoundError FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var compoundError = DomainFactory.Create<ICompoundError>();
            compoundError.ErrorCode= JsonConvert.DeserializeObject<ErrorCodes>((string)jObject[ErrorCodeKeyName]);
            compoundError.CompoundName = (string)jObject[CompoundNameKeyName];
            compoundError.ExpectedRetentionTime = (double)jObject[ExpectedRetentionTimeKeyName];
            return compoundError;
        }
    }
}
