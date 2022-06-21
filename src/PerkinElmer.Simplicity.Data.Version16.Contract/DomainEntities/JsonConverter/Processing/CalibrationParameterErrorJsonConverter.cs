using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CalibrationParameterErrorJsonConverter : IJsonConverter<ICalibrationParameterError>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ParameterNameKeyName = "ParameterName";
        private const string ErrorCodeKeyName = "ErrorCode";

        public JObject ToJson(ICalibrationParameterError instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {ParameterNameKeyName, new JValue(instance.ParameterName)},
                {ErrorCodeKeyName, new JValue(instance.ErrorCode)}
            };
        }

        public ICalibrationParameterError FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var calibrationParameterError = DomainFactory.Create<ICalibrationParameterError>();
            calibrationParameterError.ParameterName = (string)jObject[ParameterNameKeyName];
            calibrationParameterError.ErrorCode = (string)jObject[ErrorCodeKeyName];
            return calibrationParameterError;
        }
    }
}
