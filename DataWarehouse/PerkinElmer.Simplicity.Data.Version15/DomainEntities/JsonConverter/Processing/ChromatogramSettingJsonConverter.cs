using System;
using Newtonsoft.Json.Linq;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Factory;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Processing;
using PerkinElmer.Simplicity.Data.Version15.DomainEntities.Interface.Shared;

namespace PerkinElmer.Simplicity.Data.Version15.DomainEntities.JsonConverter.Processing
{
    internal class ChromatogramSettingJsonConverter : IJsonConverter<IChromatogramSetting>
    {
        private const int CurrentVersion = 1;
        private const string VersionKeyName = "Version";
        private const string ConfigurePeakLabelsKeyName = "ConfigurePeakLabels";
        private const string IsOrientationVerticalKeyName = "IsOrientationVertical";
        private const string IsSignalUnitInUvKeyName = "IsSignalUnitInUv";
        private const string IsTimeUnitInMinuteKeyName = "IsTimeUnitInMinute";
        private const string IsRescalePlotSignalToFullKeyName = "IsRescalePlotSignalToFull";
        private const string IsRescalePlotSignalToMaxYKeyName = "IsRescalePlotSignalToMaxY";
        private const string IsRescalePlotSignalToCustomKeyName = "IsRescalePlotSignalToCustom";
        private const string IsRescalePlottimeFullKeyName = "IsRescalePlottimeFull";
        private const string RescalePlotSignalFromKeyName = "RescalePlotSignalFrom";
        private const string RescalePlotSignalToKeyName = "RescalePlotSignalTo";
        private const string RescalePlotTimeFromKeyName = "RescalePlotTimeFrom";
        private const string RescalePlotTimeToKeyName = "RescalePlotTimeTo";
        private const string PeakPropertiesListKeyName = "PeakPropertiesList";
        private const string NameKeyName = "Name";
        private const string GuidKeyName = "Guid";
        private const string CreatedDateUtcKeyName = "CreatedDateUtc";
        private const string CreatedByUserKeyName = "CreatedByUser";
        private const string ModifiedDateUtcKeyName = "ModifiedDateUtc";
        private const string ModifiedByUserKeyName = "ModifiedByUser";

        public JObject ToJson(IChromatogramSetting instance)
        {
            return instance == null ? null : new JObject
                {
                    {VersionKeyName, new JValue(CurrentVersion)},
                    {ConfigurePeakLabelsKeyName, new JValue(instance.ConfigurePeakLabels)},
                    {IsOrientationVerticalKeyName, new JValue(instance.IsOrientationVertical)},
                    {IsSignalUnitInUvKeyName, new JValue(instance.IsSignalUnitInUv)},
                    {IsTimeUnitInMinuteKeyName, new JValue(instance.IsTimeUnitInMinute)},
                    {IsRescalePlotSignalToFullKeyName, new JValue(instance.IsRescalePlotSignalToFull)},
                    {IsRescalePlotSignalToMaxYKeyName, new JValue(instance.IsRescalePlotSignalToMaxY)},
                    {IsRescalePlotSignalToCustomKeyName, new JValue(instance.IsRescalePlotSignalToCustom)},
                    {IsRescalePlottimeFullKeyName, new JValue(instance.IsRescalePlottimeFull)},
                    {RescalePlotSignalFromKeyName, new JValue(instance.RescalePlotSignalFrom)},
                    {RescalePlotSignalToKeyName, new JValue(instance.RescalePlotSignalTo)},
                    {RescalePlotTimeFromKeyName, new JValue(instance.RescalePlotTimeFrom)},
                    {RescalePlotTimeToKeyName, new JValue(instance.RescalePlotTimeTo)},
                    {PeakPropertiesListKeyName, instance.PeakPropertiesList == null ? null : JArray.FromObject(instance.PeakPropertiesList)},
                    {GuidKeyName, new JValue(instance.Guid)},
                    {NameKeyName, new JValue(instance.Name)},
                    {CreatedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.CreatedByUser)},
                    {CreatedDateUtcKeyName, new JValue(instance.CreatedDateUtc)},
                    {ModifiedDateUtcKeyName, new JValue(instance.ModifiedDateUtc)},
                    {ModifiedByUserKeyName, JsonConverterRegistry.GetConverter<IUserInfo>().ToJson(instance.ModifiedByUser)},
                };

        }

        public IChromatogramSetting FromJson(JObject jObject)
        {
            if (jObject == null || jObject.Type == JTokenType.Null) return null;

            var version = (int)jObject[VersionKeyName];
            if (version > CurrentVersion)
                throw new Exception(JsonConverterErrorMessage.UnsupportedSerializedObjectVersion);

            var chromatogramSetting = DomainFactory.Create<IChromatogramSetting>();
            chromatogramSetting.ConfigurePeakLabels = (string)jObject[ConfigurePeakLabelsKeyName];
            chromatogramSetting.IsOrientationVertical = (bool)jObject[IsOrientationVerticalKeyName];
            chromatogramSetting.IsSignalUnitInUv = (bool)jObject[IsSignalUnitInUvKeyName];
            chromatogramSetting.IsTimeUnitInMinute = (bool)jObject[IsTimeUnitInMinuteKeyName];
            chromatogramSetting.IsRescalePlotSignalToFull = (bool)jObject[IsRescalePlotSignalToFullKeyName];
            chromatogramSetting.IsRescalePlotSignalToMaxY = (bool)jObject[IsRescalePlotSignalToMaxYKeyName];
            chromatogramSetting.IsRescalePlotSignalToCustom = (bool)jObject[IsRescalePlotSignalToCustomKeyName];
            chromatogramSetting.IsRescalePlottimeFull = (bool)jObject[IsRescalePlottimeFullKeyName];
            chromatogramSetting.RescalePlotSignalFrom = (long)jObject[RescalePlotSignalFromKeyName];
            chromatogramSetting.RescalePlotSignalTo = (long)jObject[RescalePlotSignalToKeyName];
            chromatogramSetting.RescalePlotTimeFrom = (double)jObject[RescalePlotTimeFromKeyName];
            chromatogramSetting.RescalePlotTimeTo = (double)jObject[RescalePlotTimeToKeyName];
            chromatogramSetting.PeakPropertiesList = JsonConverterHelper.GetListPropertyFromJson<PeakProperties>(jObject, PeakPropertiesListKeyName);
            chromatogramSetting.Guid = (Guid)jObject[GuidKeyName];
            chromatogramSetting.Name = (string)jObject[NameKeyName];
            chromatogramSetting.CreatedByUser = jObject[CreatedByUserKeyName].Type == JTokenType.Null ? 
                null : JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[CreatedByUserKeyName]);
            chromatogramSetting.CreatedDateUtc = (DateTime)jObject[CreatedDateUtcKeyName];
            chromatogramSetting.ModifiedByUser = jObject[ModifiedByUserKeyName].Type == JTokenType.Null ?
                null : JsonConverterRegistry.GetConverter<IUserInfo>().FromJson((JObject)jObject[ModifiedByUserKeyName]);
            chromatogramSetting.ModifiedDateUtc = (DateTime)jObject[ModifiedDateUtcKeyName];
            return chromatogramSetting;
        }
    }
}
