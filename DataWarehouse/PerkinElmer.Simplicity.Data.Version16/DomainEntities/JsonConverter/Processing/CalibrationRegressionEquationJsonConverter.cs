using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class CalibrationRegressionEquationJsonConverter : IJsonConverter<ICalibrationRegressionEquation>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string RegressionTypeKeyName = "RegressionType";
        private const string CoefficientsKeyName = "Coefficients";
        private const string RSquareKeyName = "RSquare";
        private const string RelativeStandardErrorValueKeyName = "RelativeStandardErrorValue";
        private const string RelativeStandardDeviationPercentKeyName = "RelativeStandardDeviationPercent";
        private const string CorrelationCoefficientKeyName = "CorrelationCoefficient";

        public JObject ToJson(ICalibrationRegressionEquation instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {RegressionTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.RegressionType, new StringEnumConverter()))},
                {CoefficientsKeyName, instance.Coefficients == null ?
                    null : JArray.FromObject(instance.Coefficients)},
                {RSquareKeyName, new JValue(instance.RSquare)},
                {RelativeStandardErrorValueKeyName, new JValue(instance.RelativeStandardErrorValue)},
                {RelativeStandardDeviationPercentKeyName,  new JValue(instance.RelativeStandardDeviationPercent)},
                {CorrelationCoefficientKeyName, new JValue(instance.CorrelationCoefficient) }
            };
        }

        public ICalibrationRegressionEquation FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var calibrationRegressionEquation = DomainFactory.Create<ICalibrationRegressionEquation>();
            calibrationRegressionEquation.RegressionType = JsonConvert.DeserializeObject<RegressionType>((string)jObject[RegressionTypeKeyName]);
            calibrationRegressionEquation.Coefficients = JsonConverterHelper.GetArrayPropertyFromJson<double>(jObject, CoefficientsKeyName);
            calibrationRegressionEquation.RSquare = (double)jObject[RSquareKeyName];
            calibrationRegressionEquation.RelativeStandardErrorValue = (double)jObject[RelativeStandardErrorValueKeyName];
            calibrationRegressionEquation.RelativeStandardDeviationPercent = (double)jObject[RelativeStandardDeviationPercentKeyName];
            calibrationRegressionEquation.CorrelationCoefficient = (double)jObject[CorrelationCoefficientKeyName];
            return calibrationRegressionEquation;
        }
    }
}
