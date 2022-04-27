using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PerkinElmer.Domain.Contracts.Processing;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class CompoundCalibrationResultsJsonConverter : IJsonConverter<ICompoundCalibrationResults>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string CompoundGuidKeyName = "CompoundGuid";
        private const string LevelResponsesKeyName = "LevelResponses";
        private const string NotEnoughLevelsFoundErrorKeyName = "NotEnoughLevelsFoundError";
        private const string InvalidAmountErrorKeyName = "InvalidAmountError";
        private const string InvalidAmountsKeyName = "InvalidAmounts";
        private const string ConfLimitTestResultKeyName = "ConfLimitTestResult";
        private const string RegressionEquationKeyName = "RegressionEquation";

        public JObject ToJson(ICompoundCalibrationResults instance)
        {
            if (instance == null) return null;
            var jObject =  new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {CompoundGuidKeyName, new JValue(instance.CompoundGuid)},
                {LevelResponsesKeyName, JObject.FromObject(instance.LevelResponses)},
                {NotEnoughLevelsFoundErrorKeyName, new JValue(instance.NotEnoughLevelsFoundError)},
                {InvalidAmountErrorKeyName, new JValue(instance.InvalidAmountError)},
                {InvalidAmountsKeyName,  instance.InvalidAmounts == null ?
                    null : JArray.FromObject(instance.InvalidAmounts)},
                {ConfLimitTestResultKeyName, new JValue(instance.ConfLimitTestResult)},
                {RegressionEquationKeyName, JsonConverterRegistry.GetConverter<ICalibrationRegressionEquation>().ToJson(instance.RegressionEquation)},
            };
            return jObject;
        }

        public ICompoundCalibrationResults FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var chromatogramSetting = DomainFactory.Create<ICompoundCalibrationResults>();
            chromatogramSetting.CompoundGuid = (Guid)jObject[CompoundGuidKeyName];
            chromatogramSetting.LevelResponses =
                JsonConverterHelper.GetDictionaryPropertyFromJson<int, List<ICalibrationPointResponse>, ICalibrationPointResponse>(jObject, LevelResponsesKeyName, int.Parse);
            chromatogramSetting.NotEnoughLevelsFoundError = (bool)jObject[NotEnoughLevelsFoundErrorKeyName];
            chromatogramSetting.InvalidAmountError = (bool)jObject[InvalidAmountErrorKeyName];
            chromatogramSetting.InvalidAmounts =
                JsonConverterHelper.GetListPropertyFromJson<double>(jObject, InvalidAmountsKeyName);
            chromatogramSetting.ConfLimitTestResult = (int)jObject[ConfLimitTestResultKeyName];
            chromatogramSetting.RegressionEquation = jObject[RegressionEquationKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<ICalibrationRegressionEquation>().FromJson((JObject) jObject[RegressionEquationKeyName]);
            return chromatogramSetting;
        }

       
    }
}
