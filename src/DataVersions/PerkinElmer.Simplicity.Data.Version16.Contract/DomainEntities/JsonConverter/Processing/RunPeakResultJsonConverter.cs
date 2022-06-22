using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing.Spectral;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class RunPeakResultJsonConverter : IJsonConverter<IRunPeakResult>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string PeakGuidKeyName = "PeakGuid";
        private const string BatchRunGuidKeyName = "BatchRunGuid";
        private const string BatchRunChannelGuidKeyName = "BatchRunChannelGuid";
        private const string CompoundGuidKeyName = "CompoundGuid";
        private const string CompoundTypeKeyName = "CompoundType";
        private const string PeakNumberKeyName = "PeakNumber";
        private const string PeakNameKeyName = "PeakName";
        private const string AreaKeyName = "Area";
        private const string HeightKeyName = "Height";
        private const string InternalStandardAreaRatioKeyName = "InternalStandardAreaRatio";
        private const string InternalStandardHeightRatioKeyName = "InternalStandardHeightRatio";
        private const string InternalStandardAmountKeyName = "InternalStandardAmount";
        private const string InternalStandardAmountRatioKeyName = "InternalStandardAmountRatio";
        private const string AreaPercentKeyName = "AreaPercent";
        private const string RetentionTimeKeyName = "RetentionTime";
        private const string StartPeakTimeKeyName = "StartPeakTime";
        private const string EndPeakTimeKeyName = "EndPeakTime";
        private const string OverlappedKeyName = "Overlapped";
        private const string IsBaselineExpoKeyName = "IsBaselineExpo";
        private const string BaselineSlopeKeyName = "BaselineSlope";
        private const string BaselineInterceptKeyName = "BaselineIntercept";
        private const string ExpoAKeyName = "ExpoA";
        private const string ExpoBKeyName = "ExpoB";
        private const string ExpoCorrectionKeyName = "ExpoCorrection";
        private const string ExpoDecayKeyName = "ExpoDecay";
        private const string ExpoHeightKeyName = "ExpoHeight";
        private const string SignalToNoiseRatioKeyName = "SignalToNoiseRatio";
        private const string AmountKeyName = "Amount";
        private const string AmountErrorKeyName = "AmountError";
        private const string ReferenceInternalStandardPeakGuidKeyName = "ReferenceInternalStandardPeakGuid";
        private const string ReferenceInternalStandardCompoundGuidKeyName = "ReferenceInternalStandardCompoundGuid";
        private const string AreaToAmountRatioKeyName = "AreaToAmountRatio";
        private const string AreaToHeightRatioKeyName = "AreaToHeightRatio";
        private const string BaselineCodeKeyName = "BaselineCode";
        private const string CalibrationInRangeKeyName = "CalibrationInRange";
        private const string NormalizedAmountKeyName = "NormalizedAmount";
        private const string RelativeRetentionTimeKeyName = "RelativeRetentionTime";
        private const string RawAmountKeyName = "RawAmount";
        private const string RetTimeReferenceGuidKeyName = "RetTimeReferenceGuid";
        private const string RrtReferenceGuidKeyName = "RrtReferenceGuid";
        private const string SuitabilityResultKeyName = "SuitabilityResult";
        private const string MidIndexKeyName = "MidIndex";
        private const string StartIndexKeyName = "StartIndex";
        private const string StopIndexKeyName = "StopIndex";
        private const string WavelengthMaxKeyName = "WavelengthMax";
        private const string AbsorbanceAtWavelengthMaxKeyName = "AbsorbanceAtWavelengthMax";
        private const string WavelengthMaxErrorKeyName = "WavelengthMaxError";
        private const string PeakPurityKeyName = "PeakPurity";
        private const string PeakPurityPassedKeyName = "PeakPurityPassed";
        private const string PeakPurityErrorKeyName = "PeakPurityError";
        private const string StandardConfirmationIndexKeyName = "StandardConfirmationIndex";
        private const string StandardConfirmationPassedKeyName = "StandardConfirmationPassed";
        private const string StandardConfirmationErrorKeyName = "StandardConfirmationError";
        private const string AbsorbanceRatioKeyName = "AbsorbanceRatio";
        private const string ModifiedByManualEventKeyName = "ModifiedByManualEvent";

        public JObject ToJson(IRunPeakResult instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {PeakGuidKeyName, new JValue(instance.PeakGuid)},
                {BatchRunGuidKeyName, new JValue(instance.BatchRunGuid)},
                {BatchRunChannelGuidKeyName, new JValue(instance.BatchRunChannelGuid)},
                
                {CompoundGuidKeyName, new JValue(instance.CompoundGuid)},
                {CompoundTypeKeyName, new JValue(JsonConvert.SerializeObject(instance.CompoundType, new StringEnumConverter()))},
                {PeakNumberKeyName, new JValue(instance.PeakNumber)},
                {PeakNameKeyName, new JValue(instance.PeakName)},
                {AreaKeyName, new JValue(instance.Area)},
                {HeightKeyName, new JValue(instance.Height)},
                {InternalStandardAreaRatioKeyName, new JValue(instance.InternalStandardAreaRatio)},
                {InternalStandardHeightRatioKeyName, new JValue(instance.InternalStandardHeightRatio)},
                {InternalStandardAmountKeyName, new JValue(instance.InternalStandardAmount)},
                {InternalStandardAmountRatioKeyName, new JValue(instance.InternalStandardAmountRatio)},
                {AreaPercentKeyName, new JValue(instance.AreaPercent)},
                {RetentionTimeKeyName, new JValue(instance.RetentionTime)},
                {StartPeakTimeKeyName, new JValue(instance.StartPeakTime)},
                {EndPeakTimeKeyName, new JValue(instance.EndPeakTime)},
                {OverlappedKeyName, new JValue(instance.Overlapped)},
                {IsBaselineExpoKeyName, new JValue(instance.IsBaselineExpo)},
                {BaselineSlopeKeyName, new JValue(instance.BaselineSlope)},
                {BaselineInterceptKeyName, new JValue(instance.BaselineIntercept)},
                {ExpoAKeyName, new JValue(instance.ExpoA)},
                {ExpoBKeyName, new JValue(instance.ExpoB)},
                {ExpoCorrectionKeyName, new JValue(instance.ExpoCorrection)},
                {ExpoDecayKeyName, new JValue(instance.ExpoDecay)},
                {ExpoHeightKeyName, new JValue(instance.ExpoHeight)},
                {SignalToNoiseRatioKeyName, new JValue(instance.SignalToNoiseRatio)},
                {AmountKeyName, new JValue(instance.Amount)},
                {AmountErrorKeyName, new JValue(JsonConvert.SerializeObject(instance.AmountError, new StringEnumConverter()))},
                {ReferenceInternalStandardPeakGuidKeyName, new JValue(instance.ReferenceInternalStandardPeakGuid)},
                {ReferenceInternalStandardCompoundGuidKeyName, new JValue(instance.ReferenceInternalStandardCompoundGuid)},
                {AreaToAmountRatioKeyName, new JValue(instance.AreaToAmountRatio)},
                {AreaToHeightRatioKeyName, new JValue(instance.AreaToHeightRatio)},
                {BaselineCodeKeyName, new JValue(JsonConvert.SerializeObject(instance.BaselineCode, new StringEnumConverter()))},
                {CalibrationInRangeKeyName, new JValue(JsonConvert.SerializeObject(instance.CalibrationInRange, new StringEnumConverter()))},
                {NormalizedAmountKeyName, new JValue(instance.NormalizedAmount)},
                {RelativeRetentionTimeKeyName, new JValue(instance.RelativeRetentionTime)},
                {RawAmountKeyName, new JValue(instance.RawAmount)},
                {RetTimeReferenceGuidKeyName, new JValue(instance.RetTimeReferenceGuid)},
                {RrtReferenceGuidKeyName, new JValue(instance.RrtReferenceGuid)},
                //{SuitabilityResultKeyName, JsonConverterRegistry.GetConverter<ISuitabilityResult>().ToJson(instance.SuitabilityResult)},
                {MidIndexKeyName, new JValue(instance.MidIndex)},
                {StartIndexKeyName, new JValue(instance.StartIndex)},
                {StopIndexKeyName, new JValue(instance.StopIndex)},
                {WavelengthMaxKeyName, new JValue(instance.WavelengthMax)},
                {AbsorbanceAtWavelengthMaxKeyName, new JValue(instance.AbsorbanceAtWavelengthMax)},
                {WavelengthMaxErrorKeyName, new JValue(JsonConvert.SerializeObject(instance.WavelengthMaxError, new StringEnumConverter()))},
                {PeakPurityKeyName, new JValue(instance.PeakPurity)},
                {PeakPurityPassedKeyName, new JValue(instance.PeakPurityPassed)},
                {PeakPurityErrorKeyName, new JValue(JsonConvert.SerializeObject(instance.PeakPurityError, new StringEnumConverter()))},
                {StandardConfirmationIndexKeyName, new JValue(instance.StandardConfirmationIndex)},
                {StandardConfirmationPassedKeyName, new JValue(instance.StandardConfirmationPassed)},
                {StandardConfirmationErrorKeyName, new JValue(JsonConvert.SerializeObject(instance.StandardConfirmationError, new StringEnumConverter()))},
                {AbsorbanceRatioKeyName, new JValue(instance.AbsorbanceRatio)},
                {ModifiedByManualEventKeyName, new JValue(instance.ModifiedByManualEvent)}
            };
        }

        public IRunPeakResult FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var runPeakResult = DomainFactory.Create<IRunPeakResult>();

            runPeakResult.PeakGuid = (Guid)jObject[PeakGuidKeyName];
            runPeakResult.BatchRunGuid = (Guid)jObject[BatchRunGuidKeyName];
            runPeakResult.BatchRunChannelGuid = (Guid)jObject[BatchRunChannelGuidKeyName];
            
            runPeakResult.CompoundGuid = (Guid)jObject[CompoundGuidKeyName];
            runPeakResult.CompoundType = JsonConvert.DeserializeObject<CompoundType>((string)jObject[CompoundTypeKeyName]);
            runPeakResult.PeakNumber = (int)jObject[PeakNumberKeyName];
            runPeakResult.PeakName = (string)jObject[PeakNameKeyName];
            runPeakResult.Area = (double)jObject[AreaKeyName];
            runPeakResult.Height = (double)jObject[HeightKeyName];
            runPeakResult.InternalStandardAreaRatio = (double?)jObject[InternalStandardAreaRatioKeyName];
            runPeakResult.InternalStandardHeightRatio = (double?)jObject[InternalStandardHeightRatioKeyName];
            runPeakResult.InternalStandardAmount = (double?)jObject[InternalStandardAmountKeyName];
            runPeakResult.InternalStandardAmountRatio = (double?)jObject[InternalStandardAmountRatioKeyName];
            runPeakResult.AreaPercent = (double)jObject[AreaPercentKeyName];
            runPeakResult.RetentionTime = (double)jObject[RetentionTimeKeyName];
            runPeakResult.StartPeakTime = (double)jObject[StartPeakTimeKeyName];
            runPeakResult.EndPeakTime = (double)jObject[EndPeakTimeKeyName];
            runPeakResult.Overlapped = (bool)jObject[OverlappedKeyName];
            runPeakResult.IsBaselineExpo = (bool)jObject[IsBaselineExpoKeyName];
            runPeakResult.BaselineSlope = (double)jObject[BaselineSlopeKeyName];
            runPeakResult.BaselineIntercept = (double)jObject[BaselineInterceptKeyName];
            runPeakResult.ExpoA = (double)jObject[ExpoAKeyName];
            runPeakResult.ExpoB = (double)jObject[ExpoBKeyName];
            runPeakResult.ExpoCorrection = (double)jObject[ExpoCorrectionKeyName];
            runPeakResult.ExpoDecay = (double)jObject[ExpoDecayKeyName];
            runPeakResult.ExpoHeight = (double)jObject[ExpoHeightKeyName];
            runPeakResult.SignalToNoiseRatio = (double)jObject[SignalToNoiseRatioKeyName];
            runPeakResult.Amount = (double?)jObject[AmountKeyName];
            runPeakResult.AmountError = JsonConvert.DeserializeObject<AmountResultError>((string)jObject[AmountErrorKeyName]);
            runPeakResult.ReferenceInternalStandardPeakGuid = (Guid)jObject[ReferenceInternalStandardPeakGuidKeyName];
            runPeakResult.ReferenceInternalStandardCompoundGuid = (Guid)jObject[ReferenceInternalStandardCompoundGuidKeyName];
            runPeakResult.AreaToAmountRatio = (double?)jObject[AreaToAmountRatioKeyName];
            runPeakResult.AreaToHeightRatio = (double?)jObject[AreaToHeightRatioKeyName];
            runPeakResult.BaselineCode = JsonConvert.DeserializeObject<BaselineCode>((string)jObject[BaselineCodeKeyName]);
            //TODO: need verify the null convert condition.
            runPeakResult.CalibrationInRange = jObject[CalibrationInRangeKeyName].Type == JTokenType.Null ?
                null : JsonConvert.DeserializeObject<HittingCalibrationRange?>((string)jObject[CalibrationInRangeKeyName]);
            runPeakResult.NormalizedAmount = (double?)jObject[NormalizedAmountKeyName];
            runPeakResult.RelativeRetentionTime = (double?)jObject[RelativeRetentionTimeKeyName];
            runPeakResult.RawAmount = (double?)jObject[RawAmountKeyName];
            runPeakResult.RetTimeReferenceGuid = (Guid)jObject[RetTimeReferenceGuidKeyName];
            runPeakResult.RrtReferenceGuid = (Guid)jObject[RrtReferenceGuidKeyName];
            // runPeakResult.SuitabilityResult = jObject[SuitabilityResultKeyName].Type == JTokenType.Null ? null :
            //     JsonConverterRegistry.GetConverter<ISuitabilityResult>().FromJson((JObject)jObject[SuitabilityResultKeyName]);
            runPeakResult.MidIndex = (int)jObject[MidIndexKeyName];
            runPeakResult.StartIndex = (int)jObject[StartIndexKeyName];
            runPeakResult.StopIndex = (int)jObject[StopIndexKeyName];
            runPeakResult.WavelengthMax = (double?)jObject[WavelengthMaxKeyName];
            runPeakResult.AbsorbanceAtWavelengthMax = (double?)jObject[AbsorbanceAtWavelengthMaxKeyName];
            runPeakResult.WavelengthMaxError = JsonConvert.DeserializeObject<WavelengthMaxError>((string)jObject[WavelengthMaxErrorKeyName]);
            runPeakResult.PeakPurity = (double?)jObject[PeakPurityKeyName];
            runPeakResult.PeakPurityPassed = (bool)jObject[PeakPurityPassedKeyName];
            runPeakResult.PeakPurityError = JsonConvert.DeserializeObject<PeakPurityError>((string)jObject[PeakPurityErrorKeyName]);
            runPeakResult.StandardConfirmationIndex = (double?)jObject[StandardConfirmationIndexKeyName];
            runPeakResult.StandardConfirmationPassed = (bool)jObject[StandardConfirmationPassedKeyName];
            runPeakResult.StandardConfirmationError = JsonConvert.DeserializeObject<StandardConfirmationError>((string)jObject[StandardConfirmationErrorKeyName]);
            runPeakResult.AbsorbanceRatio = (double?)jObject[AbsorbanceRatioKeyName];
            runPeakResult.ModifiedByManualEvent = (bool)jObject[ModifiedByManualEventKeyName];
            return runPeakResult;
        }
    }
}
