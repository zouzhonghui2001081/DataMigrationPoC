using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.DomainEntities.JsonConverter.Processing
{
    internal class CalibrationParametersJsonConverter : IJsonConverter<ICalibrationParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string InternalStandardKeyName = "InternalStandard";
        private const string ReferenceInternalStandardGuidKeyName = "ReferenceInternalStandardGuid";
        private const string InternalStandardAmountKeyName = "InternalStandardAmount";
        private const string PurityKeyName = "Purity";
        private const string QuantifyUsingAreaKeyName = "QuantifyUsingArea";
        private const string CalibrationTypeKeyName = "CalibrationType";
        private const string WeightingTypeKeyName = "WeightingType";
        private const string ScalingKeyName = "Scaling";
        private const string OriginTreatmentKeyName = "OriginTreatment";
        private const string CalibrationFactorKeyName = "CalibrationFactor";
        private const string ReferenceCompoundGuidKeyName = "ReferenceCompoundGuid";
        private const string LevelAmountsKeyName = "LevelAmounts";

        public JObject ToJson(ICalibrationParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {InternalStandardKeyName, new JValue(instance.InternalStandard)},
                {ReferenceInternalStandardGuidKeyName, new JValue(instance.ReferenceInternalStandardGuid)},
                {InternalStandardAmountKeyName, new JValue(instance.InternalStandardAmount)},
                {PurityKeyName, new JValue(instance.Purity)},
                {QuantifyUsingAreaKeyName, new JValue(instance.QuantifyUsingArea)},
                {CalibrationTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.CalibrationType, new StringEnumConverter()))},
                {WeightingTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.WeightingType, new StringEnumConverter()))},
                {ScalingKeyName, new JValue(JsonConvert.SerializeObject(instance.Scaling, new StringEnumConverter()))},
                {OriginTreatmentKeyName, new JValue(JsonConvert.SerializeObject(instance.OriginTreatment, new StringEnumConverter()))},
                {CalibrationFactorKeyName, new JValue(instance.CalibrationFactor)},
                {ReferenceCompoundGuidKeyName, new JValue(instance.ReferenceCompoundGuid)},
                {LevelAmountsKeyName, JsonConvert.SerializeObject(instance.LevelAmounts)}
            };
        }

        public ICalibrationParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var calibrationParameters = DomainFactory.Create<ICalibrationParameters>();
            calibrationParameters.InternalStandard = (bool) jObject[InternalStandardKeyName];
            calibrationParameters.ReferenceInternalStandardGuid = (Guid) jObject[ReferenceInternalStandardGuidKeyName];
            calibrationParameters.InternalStandardAmount = (double?) jObject[InternalStandardAmountKeyName];
            calibrationParameters.Purity = (double) jObject[PurityKeyName];
            calibrationParameters.QuantifyUsingArea = (bool) jObject[QuantifyUsingAreaKeyName];
            calibrationParameters.CalibrationType =
                JsonConvert.DeserializeObject<CompoundCalibrationType>((string) jObject[CalibrationTypeKeyName]);
            calibrationParameters.WeightingType =
                JsonConvert.DeserializeObject<CalibrationWeightingType>((string)jObject[WeightingTypeKeyName]);
            calibrationParameters.Scaling =
                JsonConvert.DeserializeObject<CalibrationScaling>((string)jObject[ScalingKeyName]);
            calibrationParameters.OriginTreatment =
                JsonConvert.DeserializeObject<OriginTreatment>((string)jObject[OriginTreatmentKeyName]);
            calibrationParameters.CalibrationFactor = (double) jObject[CalibrationFactorKeyName];
            calibrationParameters.ReferenceCompoundGuid = (Guid) jObject[ReferenceCompoundGuidKeyName];
            calibrationParameters.LevelAmounts =
                JsonConvert.DeserializeObject<Dictionary<int, double?>>((string) jObject[LevelAmountsKeyName]);
            return calibrationParameters;
        }
    }
}
