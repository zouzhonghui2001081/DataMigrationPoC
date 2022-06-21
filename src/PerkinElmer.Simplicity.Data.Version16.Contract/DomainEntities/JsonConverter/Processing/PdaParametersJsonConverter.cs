using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.Interface.Processing;

namespace PerkinElmer.Simplicity.Data.Version16.Contract.DomainEntities.JsonConverter.Processing
{
    internal class PdaParametersJsonConverter : IJsonConverter<IPdaParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string WavelengthParametersKeyName = "WavelengthParameters";
        private const string PeakPurityParametersKeyName = "PeakPurityParameters";
        private const string StandardConfirmationParametersKeyName = "StandardConfirmationParameters";
        private const string AbsorbanceRatioParametersKeyName = "AbsorbanceRatioParameters";
        private const string LibraryConfirmationParametersKeyName = "LibraryConfirmationParameters";
        private const string PeakLibrarySearchParametersKeyName = "PeakLibrarySearchParameters";
        private const string BaselineCorrectionParametersKeyName = "BaselineCorrectionParameters";
        private const string ApexOptimizedParametersKeyName = "ApexOptimizedParameters";

        public JObject ToJson(IPdaParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {WavelengthParametersKeyName, JsonConverterRegistry.GetConverter<IPdaWavelengthMaxParameters>().ToJson(instance.WavelengthParameters)},
                {PeakPurityParametersKeyName, JsonConverterRegistry.GetConverter<IPdaPeakPurityParameters>().ToJson(instance.PeakPurityParameters)},
                {StandardConfirmationParametersKeyName, JsonConverterRegistry.GetConverter<IPdaStandardConfirmationParameters>().ToJson(instance.StandardConfirmationParameters)},
                {AbsorbanceRatioParametersKeyName, JsonConverterRegistry.GetConverter<IPdaAbsorbanceRatioParameters>().ToJson(instance.AbsorbanceRatioParameters)},
                {LibraryConfirmationParametersKeyName, JsonConverterRegistry.GetConverter<IPdaLibraryConfirmationParameters>().ToJson(instance.LibraryConfirmationParameters)},
                {PeakLibrarySearchParametersKeyName, JsonConverterRegistry.GetConverter<IPdaLibrarySearchParameters>().ToJson(instance.PeakLibrarySearchParameters)},
                {BaselineCorrectionParametersKeyName, JsonConverterRegistry.GetConverter<IPdaBaselineCorrectionParameters>().ToJson(instance.BaselineCorrectionParameters)}
            };
        }

        public IPdaParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaParameters = DomainFactory.Create<IPdaParameters>();

            pdaParameters.WavelengthParameters = jObject[WavelengthParametersKeyName].Type == JTokenType.Null ? 
                null: JsonConverterRegistry.GetConverter<IPdaWavelengthMaxParameters>().FromJson((JObject)jObject[WavelengthParametersKeyName]);
            pdaParameters.PeakPurityParameters = jObject[PeakPurityParametersKeyName].Type == JTokenType.Null ? 
                null : JsonConverterRegistry.GetConverter<IPdaPeakPurityParameters>().FromJson((JObject)jObject[PeakPurityParametersKeyName]);
            pdaParameters.StandardConfirmationParameters = jObject[StandardConfirmationParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaStandardConfirmationParameters>().FromJson((JObject)jObject[StandardConfirmationParametersKeyName]);
            pdaParameters.AbsorbanceRatioParameters = jObject[AbsorbanceRatioParametersKeyName].Type == JTokenType.Null ? 
                null : JsonConverterRegistry.GetConverter<IPdaAbsorbanceRatioParameters>().FromJson((JObject)jObject[AbsorbanceRatioParametersKeyName]);
            pdaParameters.LibraryConfirmationParameters = jObject[LibraryConfirmationParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaLibraryConfirmationParameters>().FromJson((JObject)jObject[LibraryConfirmationParametersKeyName]);
            pdaParameters.PeakLibrarySearchParameters = jObject[PeakLibrarySearchParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaLibrarySearchParameters>().FromJson((JObject)jObject[PeakLibrarySearchParametersKeyName]);
            pdaParameters.BaselineCorrectionParameters = jObject[BaselineCorrectionParametersKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IPdaBaselineCorrectionParameters>().FromJson((JObject)jObject[BaselineCorrectionParametersKeyName]);
            return pdaParameters;
        }
    }

    public class PdaWavelengthMaxParametersJsonConverter : IJsonConverter<IPdaWavelengthMaxParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MinWavelengthKeyName = "MinWavelength";
        private const string MaxWavelengthKeyName = "MaxWavelength";
        private const string ApplyBaselineCorrectionKeyName = "ApplyBaselineCorrection";
        private const string UseAutoAbsorbanceThresholdKeyName = "UseAutoAbsorbanceThreshold";
        private const string ManualAbsorbanceThresholdKeyName = "ManualAbsorbanceThreshold";

        public JObject ToJson(IPdaWavelengthMaxParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinWavelengthKeyName, new JValue(instance.MinWavelength)},
                {MaxWavelengthKeyName, new JValue(instance.MaxWavelength)},
                {ApplyBaselineCorrectionKeyName, new JValue(instance.ApplyBaselineCorrection)},
                {UseAutoAbsorbanceThresholdKeyName, new JValue(instance.UseAutoAbsorbanceThreshold)},
                {ManualAbsorbanceThresholdKeyName, new JValue(instance.ManualAbsorbanceThreshold)}
            };
        }

        public IPdaWavelengthMaxParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaWavelengthMaxParameters = DomainFactory.Create<IPdaWavelengthMaxParameters>();

            pdaWavelengthMaxParameters.MinWavelength = (double)jObject[MinWavelengthKeyName];
            pdaWavelengthMaxParameters.MaxWavelength = (double)jObject[MaxWavelengthKeyName];
            pdaWavelengthMaxParameters.ApplyBaselineCorrection = (bool)jObject[ApplyBaselineCorrectionKeyName];
            pdaWavelengthMaxParameters.UseAutoAbsorbanceThreshold = (bool)jObject[UseAutoAbsorbanceThresholdKeyName];
            pdaWavelengthMaxParameters.ManualAbsorbanceThreshold = (double)jObject[ManualAbsorbanceThresholdKeyName];
            return pdaWavelengthMaxParameters;
        }
    }

    public class PdaPeakPurityParametersJsonConverter : IJsonConverter<IPdaPeakPurityParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string MinWavelengthKeyName = "MinWavelength";
        private const string MaxWavelengthKeyName = "MaxWavelength";
        private const string MinimumDataPointsKeyName = "MinimumDataPoints";
        private const string ApplyBaselineCorrectionKeyName = "ApplyBaselineCorrection";
        private const string PurityLimitKeyName = "PurityLimit";
        private const string PercentOfPeakHeightForSpectraKeyName = "PercentOfPeakHeightForSpectra";
        private const string UseAutoAbsorbanceThresholdKeyName = "UseAutoAbsorbanceThreshold";
        private const string ManualAbsorbanceThresholdKeyName = "ManualAbsorbanceThreshold";

        public JObject ToJson(IPdaPeakPurityParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinWavelengthKeyName, new JValue(instance.MinWavelength)},
                {MaxWavelengthKeyName, new JValue(instance.MaxWavelength)},
                {MinimumDataPointsKeyName, new JValue(instance.MinimumDataPoints)},
                {ApplyBaselineCorrectionKeyName, new JValue(instance.ApplyBaselineCorrection)},
                {PurityLimitKeyName, new JValue(instance.PurityLimit)},
                {PercentOfPeakHeightForSpectraKeyName, new JValue(instance.PercentOfPeakHeightForSpectra)},
                {UseAutoAbsorbanceThresholdKeyName, new JValue(instance.UseAutoAbsorbanceThreshold)},
                {ManualAbsorbanceThresholdKeyName, new JValue(instance.ManualAbsorbanceThreshold)}
            };
        }

        public IPdaPeakPurityParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaPeakPurityParameters = DomainFactory.Create<IPdaPeakPurityParameters>();

            pdaPeakPurityParameters.MinWavelength = (double)jObject[MinWavelengthKeyName];
            pdaPeakPurityParameters.MaxWavelength = (double)jObject[MaxWavelengthKeyName];
            pdaPeakPurityParameters.MinimumDataPoints = (int)jObject[MinimumDataPointsKeyName];
            pdaPeakPurityParameters.ApplyBaselineCorrection = (bool)jObject[ApplyBaselineCorrectionKeyName];
            pdaPeakPurityParameters.PurityLimit = (double)jObject[PurityLimitKeyName];
            pdaPeakPurityParameters.PercentOfPeakHeightForSpectra = (double)jObject[PercentOfPeakHeightForSpectraKeyName];
            pdaPeakPurityParameters.UseAutoAbsorbanceThreshold = (bool)jObject[UseAutoAbsorbanceThresholdKeyName];
            pdaPeakPurityParameters.ManualAbsorbanceThreshold = (double)jObject[ManualAbsorbanceThresholdKeyName];
            return pdaPeakPurityParameters;
        }
    }

    public class PdaLibrarySearchParametersJsonConverter : IJsonConverter<IPdaLibrarySearchParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";

        private const string MinimumWavelengthKeyName = "MinimumWavelength";
        private const string MaximumWavelengthKeyName = "MaximumWavelength";
        private const string MatchRetentionTimeWindowKeyName = "MatchRetentionTimeWindow";
        private const string IsMatchRetentionTimeWindowEnabledKeyName = "IsMatchRetentionTimeWindowEnabled";
        private const string IsBaselineCorrectionEnabledKeyName = "IsBaselineCorrectionEnabled";
        private const string HitDistanceThresholdKeyName = "HitDistanceThreshold";
        private const string IsPeakLibrarySearchKeyName = "IsPeakLibrarySearch";
        private const string SelectedLibrariesKeyName = "SelectedLibraries";
        private const string UseWavelengthLimitsKeyName = "UseWavelengthLimits";
        private const string MaxNumberOfResultsKeyName = "MaxNumberOfResults";



        public JObject ToJson(IPdaLibrarySearchParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinimumWavelengthKeyName, new JValue(instance.MinimumWavelength)},
                {MaximumWavelengthKeyName, new JValue(instance.MaximumWavelength)},
                {MatchRetentionTimeWindowKeyName, new JValue(instance.MatchRetentionTimeWindow)},
                {IsMatchRetentionTimeWindowEnabledKeyName, new JValue(instance.IsMatchRetentionTimeWindowEnabled)},
                {IsBaselineCorrectionEnabledKeyName, new JValue(instance.IsBaselineCorrectionEnabled)},
                {HitDistanceThresholdKeyName, new JValue(instance.HitDistanceThreshold)},
                {IsPeakLibrarySearchKeyName, new JValue(instance.IsPeakLibrarySearch)},
                {SelectedLibrariesKeyName, instance.SelectedLibraries ==null? null: JArray.FromObject(instance.SelectedLibraries)},
                {UseWavelengthLimitsKeyName, new JValue(instance.UseWavelengthLimits)},
                {MaxNumberOfResultsKeyName, new JValue(instance.MaxNumberOfResults)}
            };
        }

        public IPdaLibrarySearchParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaLibrarySearchParameters = DomainFactory.Create<IPdaLibrarySearchParameters>();

            pdaLibrarySearchParameters.MinimumWavelength = (double)jObject[MinimumWavelengthKeyName];
            pdaLibrarySearchParameters.MaximumWavelength = (double)jObject[MaximumWavelengthKeyName];
            pdaLibrarySearchParameters.MatchRetentionTimeWindow = (double)jObject[MatchRetentionTimeWindowKeyName];
            pdaLibrarySearchParameters.IsMatchRetentionTimeWindowEnabled = (bool)jObject[IsMatchRetentionTimeWindowEnabledKeyName];
            pdaLibrarySearchParameters.IsBaselineCorrectionEnabled = (bool)jObject[IsBaselineCorrectionEnabledKeyName];
            pdaLibrarySearchParameters.HitDistanceThreshold = (double)jObject[HitDistanceThresholdKeyName];
            pdaLibrarySearchParameters.IsPeakLibrarySearch = (bool)jObject[IsPeakLibrarySearchKeyName];
            pdaLibrarySearchParameters.SelectedLibraries = JsonConverterHelper.GetListPropertyFromJson<string>(jObject, SelectedLibrariesKeyName);
            pdaLibrarySearchParameters.UseWavelengthLimits = (bool)jObject[UseWavelengthLimitsKeyName];
            pdaLibrarySearchParameters.MaxNumberOfResults = (int)jObject[MaxNumberOfResultsKeyName];

            return pdaLibrarySearchParameters;
        }
    }

    public class PdaLibraryConfirmationParametersJsonConverter : IJsonConverter<IPdaLibraryConfirmationParameters>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";

        private const string MinimumWavelengthKeyName = "MinimumWavelength";
        private const string MaximumWavelengthKeyName = "MaximumWavelength";
        private const string IsBaselineCorrectionEnabledKeyName = "IsBaselineCorrectionEnabled";
        private const string HitDistanceThresholdKeyName = "HitDistanceThreshold";
        private const string SelectedLibrariesKeyName = "SelectedLibraries";

        public JObject ToJson(IPdaLibraryConfirmationParameters instance)
        {
            return instance == null ? null : new JObject
            {
                {VersionKeyName, new JValue(CurrentVersion)},
                {MinimumWavelengthKeyName, new JValue(instance.MinimumWavelength)},
                {MaximumWavelengthKeyName, new JValue(instance.MaximumWavelength)},
                {IsBaselineCorrectionEnabledKeyName, new JValue(instance.IsBaselineCorrectionEnabled)},
                {HitDistanceThresholdKeyName, new JValue(instance.HitDistanceThreshold)},
                {SelectedLibrariesKeyName, instance.SelectedLibraries == null? null: JArray.FromObject(instance.SelectedLibraries)},
            };
        }

        public IPdaLibraryConfirmationParameters FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;
            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var pdaLibraryConfirmationParameters = DomainFactory.Create<IPdaLibraryConfirmationParameters>();

            pdaLibraryConfirmationParameters.MinimumWavelength = (double)jObject[MinimumWavelengthKeyName];
            pdaLibraryConfirmationParameters.MaximumWavelength = (double)jObject[MaximumWavelengthKeyName];
            pdaLibraryConfirmationParameters.IsBaselineCorrectionEnabled = (bool)jObject[IsBaselineCorrectionEnabledKeyName];
            pdaLibraryConfirmationParameters.HitDistanceThreshold = (double)jObject[HitDistanceThresholdKeyName];
            pdaLibraryConfirmationParameters.SelectedLibraries = JsonConverterHelper.GetListPropertyFromJson<string>(jObject, SelectedLibrariesKeyName);

            return pdaLibraryConfirmationParameters;
        }
    }


}
