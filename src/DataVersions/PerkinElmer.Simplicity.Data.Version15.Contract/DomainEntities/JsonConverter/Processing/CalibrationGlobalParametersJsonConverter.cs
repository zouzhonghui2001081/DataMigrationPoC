using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.Contract.DomainEntities.JsonConverter.Processing
{
    internal class CalibrationGlobalParametersJsonConverter : IJsonConverter<ICalibrationGlobalParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string NumberOfLevelsKeyName = "NumberOfLevels";
        private const string AmountUnitsKeyName = "AmountUnits";
        private const string UnidentifiedPeakCalibrationTypeKeyName = "UnidentifiedPeakCalibrationType";
        private const string UnidentifiedPeakCalibrationFactorKeyName = "UnidentifiedPeakCalibrationFactor";
        private const string UnidentifiedPeakReferenceCompoundGuidKeyName = "UnidentifiedPeakReferenceCompoundGuid";

        public JObject ToJson(ICalibrationGlobalParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {NumberOfLevelsKeyName, new JValue(instance.NumberOfLevels)},
                {AmountUnitsKeyName, new JValue(instance.AmountUnits)},
                {UnidentifiedPeakCalibrationTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.UnidentifiedPeakCalibrationType, new StringEnumConverter()))},
                {UnidentifiedPeakCalibrationFactorKeyName, new JValue(instance.UnidentifiedPeakCalibrationFactor)},
                {UnidentifiedPeakReferenceCompoundGuidKeyName, new JValue(instance.UnidentifiedPeakReferenceCompoundGuid) }
            };
        }

        public ICalibrationGlobalParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);
            var calibrationGlobalParameters = DomainFactory.Create<ICalibrationGlobalParameters>();
            calibrationGlobalParameters.NumberOfLevels = (int)jObject[NumberOfLevelsKeyName];
            calibrationGlobalParameters.AmountUnits = (string) jObject[AmountUnitsKeyName];
            calibrationGlobalParameters.UnidentifiedPeakCalibrationType =
                JsonConvert.DeserializeObject<UnidentifiedPeakCalibrationType>(
                    (string) jObject[UnidentifiedPeakCalibrationTypeKeyName]);
            calibrationGlobalParameters.UnidentifiedPeakCalibrationFactor =
                (double) jObject[UnidentifiedPeakCalibrationFactorKeyName];
            calibrationGlobalParameters.UnidentifiedPeakReferenceCompoundGuid =
                (Guid) jObject[UnidentifiedPeakReferenceCompoundGuidKeyName];
            return calibrationGlobalParameters;
        }
    }
}
