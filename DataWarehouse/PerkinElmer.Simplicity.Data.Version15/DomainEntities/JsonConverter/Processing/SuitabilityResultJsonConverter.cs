using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class SuitabilityResultJsonConverter : IJsonConverter<ISuitabilityResult>
    {
        private const int CurrentVersion = 2;
        private const int Version1 = 1;
        private const string VersionKeyName = "Version";
        private const string AreaKeyName = "Area";
        private const string HeightKeyName = "Height";
        private const string KPrimeKeyName = "KPrime";
        private const string PlatesDorseyFoleyKeyName = "PlatesDorseyFoley";
        private const string PlatesTangentialKeyName = "PlatesTangential";
        private const string TailingFactorKeyName = "TailingFactor";
        private const string ResolutionKeyName = "Resolution";
        private const string PeakWidthKeyName = "PeakWidth";
        private const string PeakWidthAt5PercentHeightKeyName = "PeakWidthAt5PercentHeight";
        private const string PeakWidthAt10PercentHeightKeyName = "PeakWidthAt10PercentHeight";
        private const string PeakWidthAtHalfHeightKeyName = "PeakWidthAtHalfHeight";
        private const string RelativeRetTimeSuitKeyName = "RelativeRetTimeSuit";
        private const string RelativeRetentionTimeKeyName = "RelativeRetentionTime";
        private const string SignalKeyName = "Signal";


        public JObject ToJson(ISuitabilityResult instance)
        {
            if (instance == null) return null;
            var jObject= new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {AreaKeyName,  instance.Area== null ? null: JObject.FromObject(instance.Area)},
                {HeightKeyName,  instance.Height == null ? null : JObject.FromObject(instance.Height)},
                {KPrimeKeyName,  instance.CapacityFactorKPrime== null ? null : JObject.FromObject(instance.CapacityFactorKPrime)},
                {PlatesDorseyFoleyKeyName,  instance.TheoreticalPlatesN== null ? null : JObject.FromObject(instance.TheoreticalPlatesN)},
                {PlatesTangentialKeyName,  instance.TheoreticalPlatesNTan== null ? null : JObject.FromObject(instance.TheoreticalPlatesNTan)},
                {TailingFactorKeyName,  instance.TailingFactorSymmetry== null ? null : JObject.FromObject(instance.TailingFactorSymmetry)},
                {ResolutionKeyName,  instance.Resolution== null ? null : JObject.FromObject(instance.Resolution)},
                {PeakWidthKeyName,  instance.PeakWidthAtBase== null ? null : JObject.FromObject(instance.PeakWidthAtBase)},
                {PeakWidthAt5PercentHeightKeyName,  instance.PeakWidthAt5Pct== null ? null : JObject.FromObject(instance.PeakWidthAt5Pct)},
                {PeakWidthAt10PercentHeightKeyName,  instance.PeakWidthAt10Pct== null ? null : JObject.FromObject(instance.PeakWidthAt10Pct)},
                {PeakWidthAtHalfHeightKeyName,  instance.PeakWidthAt50Pct== null ? null : JObject.FromObject(instance.PeakWidthAt50Pct)},
                {RelativeRetTimeSuitKeyName,  instance.RelativeRetention== null ? null : JObject.FromObject(instance.RelativeRetention)},
                {SignalKeyName,  instance.SignalToNoise == null ? null : JObject.FromObject(instance.SignalToNoise)}
            };

            if (CurrentVersion > Version1)
            {
                jObject.Add(RelativeRetentionTimeKeyName, new JValue(instance.RelativeRetentionTime));
            }
            return jObject;
        }

        public ISuitabilityResult FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var suitabilityResult = DomainFactory.Create<ISuitabilityResult>();
            suitabilityResult.Area = jObject[AreaKeyName].Type == JTokenType.Null? null : jObject[AreaKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.Height = jObject[HeightKeyName].Type == JTokenType.Null ? null : jObject[HeightKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.CapacityFactorKPrime = jObject[KPrimeKeyName].Type == JTokenType.Null ? null : jObject[KPrimeKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.TheoreticalPlatesN = jObject[PlatesDorseyFoleyKeyName].Type == JTokenType.Null ? null : jObject[PlatesDorseyFoleyKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.TheoreticalPlatesNTan = jObject[PlatesTangentialKeyName].Type == JTokenType.Null ? null : jObject[PlatesTangentialKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.TailingFactorSymmetry = jObject[TailingFactorKeyName].Type == JTokenType.Null ? null : jObject[TailingFactorKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.Resolution = jObject[ResolutionKeyName].Type == JTokenType.Null ? null : jObject[ResolutionKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.PeakWidthAtBase = jObject[PeakWidthKeyName].Type == JTokenType.Null ? null : jObject[PeakWidthKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.PeakWidthAt5Pct = jObject[PeakWidthAt5PercentHeightKeyName].Type == JTokenType.Null ? null : jObject[PeakWidthAt5PercentHeightKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.PeakWidthAt10Pct = jObject[PeakWidthAt10PercentHeightKeyName].Type == JTokenType.Null ? null : jObject[PeakWidthAt10PercentHeightKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.PeakWidthAt50Pct = jObject[PeakWidthAtHalfHeightKeyName].Type == JTokenType.Null ? null : jObject[PeakWidthAtHalfHeightKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.RelativeRetention = jObject[RelativeRetTimeSuitKeyName].Type == JTokenType.Null ? null : jObject[RelativeRetTimeSuitKeyName].ToObject<SuitabilityParameterResult>();
            suitabilityResult.SignalToNoise = jObject[SignalKeyName].Type == JTokenType.Null ? null : jObject[SignalKeyName].ToObject<SuitabilityParameterResult>();
            if (version > Version1)
            {
                suitabilityResult.RelativeRetentionTime = jObject[RelativeRetentionTimeKeyName].Type == JTokenType.Null ? null : jObject[RelativeRetentionTimeKeyName].ToObject<SuitabilityParameterResult>();
            }
            return suitabilityResult;
        }
    }
}
