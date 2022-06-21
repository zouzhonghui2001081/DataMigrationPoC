using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CalibrationPointResponseJsonConverter : IJsonConverter<ICalibrationPointResponse>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string QuantifyUsingAreaKeyName = "QuantifyUsingArea";
        private const string UseInternalStandardKeyName = "UseInternalStandard";
        private const string AreaKeyName = "Area";
        private const string AreaRatioKeyName = "AreaRatio";
        private const string HeightKeyName = "Height";
        private const string HeightRatioKeyName = "HeightRatio";
        private const string ExcludedKeyName = "Excluded";
        private const string PeakNotFoundErrorKeyName = "PeakNotFoundError";
        private const string InternalStandardPeakNotFoundErrorKeyName = "InternalStandardPeakNotFoundError";
        private const string BatchRunGuidKeyName = "BatchRunGuid";
        private const string ExternalKeyName = "External";
        private const string PeakAreaPercentageKeyName = "PeakAreaPercentage";
        private const string LevelKeyName = "Level";
        private const string StandardAmountAdjustmentCoeffKeyName = "StandardAmountAdjustmentCoeff";
        private const string InternalStandardAmountAdjustmentCoeffKeyName = "InternalStandardAmountAdjustmentCoeff";
        private const string PointCalibrationFactorKeyName = "PointCalibrationFactor";
        private const string InvalidAmountErrorKeyName = "InvalidAmountError";
        private const string OutlierTestFailedKeyName = "OutlierTestFailed";
        private const string OutlierTestResultKeyName = "OutlierTestResult";

        public JObject ToJson(ICalibrationPointResponse instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {QuantifyUsingAreaKeyName, new JValue(instance.QuantifyUsingArea)},
                {UseInternalStandardKeyName, new JValue(instance.UseInternalStandard)},
                {AreaKeyName, new JValue(instance.Area)},
                {AreaRatioKeyName, new JValue(instance.AreaRatio)},
                {HeightKeyName, new JValue(instance.Height)},
                {HeightRatioKeyName, new JValue(instance.HeightRatio)},
                {ExcludedKeyName, new JValue(instance.Excluded)},
                {PeakNotFoundErrorKeyName, new JValue(instance.PeakNotFoundError)},
                {InternalStandardPeakNotFoundErrorKeyName, new JValue(instance.InternalStandardPeakNotFoundError)},
                {BatchRunGuidKeyName, new JValue(instance.BatchRunGuid)},
                {ExternalKeyName, new JValue(instance.External)},
                {PeakAreaPercentageKeyName, new JValue(instance.PeakAreaPercentage)},
                {LevelKeyName, new JValue(instance.Level)},
                {StandardAmountAdjustmentCoeffKeyName, new JValue(instance.StandardAmountAdjustmentCoeff)},
                {InternalStandardAmountAdjustmentCoeffKeyName, new JValue(instance.InternalStandardAmountAdjustmentCoeff)},
                {PointCalibrationFactorKeyName, new JValue(instance.PointCalibrationFactor)},
                {InvalidAmountErrorKeyName, new JValue(instance.InvalidAmountError)},
                {OutlierTestFailedKeyName, new JValue(instance.OutlierTestFailed)},
                {OutlierTestResultKeyName, new JValue(instance.OutlierTestResult)}
            };
        }

        public ICalibrationPointResponse FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            if (jObject.ContainsKey(VersionKeyName))
            {
                var version = (int)jObject[VersionKeyName];
                if (version > CurrentVersion)
                    throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            }

            var calibrationPointResponse = DomainFactory.Create<ICalibrationPointResponse>();
            calibrationPointResponse.QuantifyUsingArea = (bool)jObject[QuantifyUsingAreaKeyName];
            calibrationPointResponse.UseInternalStandard = (bool)jObject[UseInternalStandardKeyName];
            calibrationPointResponse.Area = (double)jObject[AreaKeyName];
            calibrationPointResponse.AreaRatio = (double?)jObject[AreaRatioKeyName];
            calibrationPointResponse.Height = (double)jObject[HeightKeyName];
            calibrationPointResponse.HeightRatio = (double?)jObject[HeightRatioKeyName];
            calibrationPointResponse.Excluded = (bool)jObject[ExcludedKeyName];
            calibrationPointResponse.PeakNotFoundError = (bool)jObject[PeakNotFoundErrorKeyName];
            calibrationPointResponse.InternalStandardPeakNotFoundError = (bool)jObject[InternalStandardPeakNotFoundErrorKeyName];
            calibrationPointResponse.BatchRunGuid = (Guid)jObject[BatchRunGuidKeyName];
            calibrationPointResponse.External = (bool)jObject[ExternalKeyName];
            calibrationPointResponse.PeakAreaPercentage = (double)jObject[PeakAreaPercentageKeyName];
            calibrationPointResponse.Level = (int)jObject[LevelKeyName];
            calibrationPointResponse.StandardAmountAdjustmentCoeff = (double)jObject[StandardAmountAdjustmentCoeffKeyName];
            calibrationPointResponse.InternalStandardAmountAdjustmentCoeff = (double)jObject[InternalStandardAmountAdjustmentCoeffKeyName];
            calibrationPointResponse.PointCalibrationFactor = (double?)jObject[PointCalibrationFactorKeyName];
            calibrationPointResponse.InvalidAmountError = (bool)jObject[InvalidAmountErrorKeyName];
            calibrationPointResponse.OutlierTestFailed = (bool)jObject[OutlierTestFailedKeyName];
            calibrationPointResponse.OutlierTestResult = (double)jObject[OutlierTestResultKeyName];
            return calibrationPointResponse;
        }
    }
}
